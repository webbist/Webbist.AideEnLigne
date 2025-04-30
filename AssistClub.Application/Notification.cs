using AssistClub.Application.DTOs;
using AssistClub.Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace AssistClub.Application;

/// <summary>
/// Handles sending notification emails for AssistClub platform events (questions, answers, updates).
/// </summary>
/// <param name="configuration">Application configuration for email settings.</param>
/// <param name="userRepository">Repository for accessing user and notification preferences.</param>
public class Notification(IConfiguration configuration, IUserRepository userRepository)
{
    /// <summary>
    /// Sends a single email message asynchronously.
    /// </summary>
    /// <param name="emailRequest">The <see cref="EmailRequest"/> containing email details.</param>
    /// <returns>True if the email was sent successfully, false otherwise.</returns>
    private async Task<bool> SendEmailAsync(EmailRequest emailRequest)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = emailRequest.Body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(configuration["EmailSettings:Host"], int.Parse(configuration["EmailSettings:Port"]),
                SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(configuration["EmailSettings:Username"],
                configuration["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending email: {e.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Executes multiple email sending tasks safely and reports failures if any.
    /// </summary>
    /// <param name="tasks">The collection of email sending tasks.</param>
    private async Task SafeWhenAll(IEnumerable<Task<bool>> tasks)
    {
        var results = await Task.WhenAll(tasks);
        if (results.Any(success => !success))
        {
            Console.WriteLine("[WARNING] Some emails failed to send.");
        }
    }
    
    /// <summary>
    /// Sends an email notification when a new question is created.
    /// </summary>
    /// <remarks>
    /// Notifies admins, club members, and the question author if they opted in for this type of notification.
    /// </remarks>
    /// <param name="createdQuestion">The <see cref="Question"/> that was created.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the user associated with the question is not found.
    /// </exception>
    public async Task SendEmailOnCreate(Question createdQuestion)
    {
        var url = configuration["WebsiteSettings:QuestionUrl"];
        var websiteName = configuration["WebsiteSettings:Name"];
        var author = await userRepository.GetUserByIdAsync(createdQuestion.UserId) 
                     ?? throw new ArgumentException($"User with ID {createdQuestion.UserId} not found.");
        
        var recipients = await userRepository.GetEmailsToNotifyOnNewQuestion(createdQuestion.UserId, author.Club);
        var emailTasks = recipients
            .Select(email => SendEmailAsync(new EmailRequest
            {
                To = email,
                Subject = $"Nouvelle question sur {websiteName}",
                Body = @$"
                       <p>Bonjour,</p>
                       <p>Une nouvelle question a été publiée dans votre club sur {websiteName} :</p>
                       <p><strong>{createdQuestion.Title}</strong></p>
                       <p>Vous pouvez consulter et répondre à cette question en cliquant sur le lien suivant :</p>
                       <p><a href=""{url}/{createdQuestion.Id}"">👉 Voir ma question sur {websiteName}</a></p>
                       <p>Merci de contribuer à notre communauté !</p>
                       <p>L'équipe {websiteName}</p>"
            })).ToList();
        
        emailTasks.Add(SendEmailAsync(new EmailRequest
        {
            To = author.Email,
            Subject = $"Votre question a été enregistrée sur {websiteName}",
            Body = @$"
                   <p>Bonjour {author.Firstname},</p>
                   <p>Merci d'avoir posé votre question sur {websiteName} ! 🎉</p>
                   <p>Voici le titre de votre question :</p>
                   <p><strong>{createdQuestion.Title}</strong></p>
                   <p>Notre communauté pourra bientôt y répondre. Vous pouvez suivre l'évolution ici :</p>
                   <p><a href=""{url}/{createdQuestion.Id}"">👉 Voir ma question sur {websiteName}</a></p>
                   <p>Merci de faire confiance à {websiteName}.</p>
                   <p>L'équipe {websiteName}</p>"
        }));
        
        await SafeWhenAll(emailTasks);
    }
    
    /// <summary>
    /// Sends an email notification when a question is updated.
    /// </summary>
    /// <remarks>
    /// Notifies previous participants and the question author if modified by an admin, and they opted in for this type of notification.
    /// </remarks>
    /// <param name="updatedQuestion">The <see cref="Question"/> that was updated.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the user associated with the question is not found.
    /// </exception>
    public async Task SendEmailOnUpdateQuestion(Question updatedQuestion)
    {
        var url = configuration["WebsiteSettings:QuestionUrl"];
        var websiteName = configuration["WebsiteSettings:Name"];
        var author = await userRepository.GetUserByIdAsync(updatedQuestion.UserId) 
                     ?? throw new ArgumentException($"User with ID {updatedQuestion.UserId} not found.");
        
        var recipients = await userRepository.GetEmailsToNotifyOnUpdateQuestion(updatedQuestion.Id, updatedQuestion.UserId);
        var emailTasks = recipients
            .Select(email => SendEmailAsync(new EmailRequest
            {
                To = email,
                Subject = $"Mise à jour d'une question sur {websiteName}",
                Body = @$"
                       <p>Bonjour,</p>
                       <p>Une question que vous suivez a été mise à jour sur {websiteName} :</p>
                       <p><strong>{updatedQuestion.Title}</strong></p>
                       <p>Vous pouvez consulter la nouvelle version de la question ici :</p>
                       <p><a href=""{url}/{updatedQuestion.Id}"">👉 Voir la question mise à jour</a></p>
                       <p>Merci pour votre implication dans la communauté {websiteName} !</p>
                       <p>L'équipe {websiteName}</p>"
            }))
            .ToList();

        if (updatedQuestion.ModifiedBy != author.Id && author.NotificationPreference.NotifyOnMyQuestionOrAnswerModifiedByAdmin)
        {
            emailTasks.Add(SendEmailAsync(new EmailRequest
            {
                To = author.Email,
                Subject = "Votre question a été modifiée par un administrateur",
                Body = @$"
                       <p>Bonjour {author.Firstname},</p>
                       <p>Votre question sur AssistClub a été modifiée par un administrateur :</p>
                       <p><strong>{updatedQuestion.Title}</strong></p>
                       <p>Vous pouvez voir les modifications apportées ici :</p>
                       <p><a href=""{url}/{updatedQuestion.Id}"">👉 Voir ma question mise à jour</a></p>
                       <p>Merci de votre confiance dans {websiteName}.</p>
                       <p>L'équipe {websiteName}</p>"
            }));
        }

        await SafeWhenAll(emailTasks);
    }
    
    /// <summary>
    /// Sends an email notification when an answer is updated, particularly if marked as official.
    /// </summary>
    /// <remarks>
    /// Notifies the answer author if modified by an admin if he opted in for this type of notification. <br/>
    /// Also notifies users who follow the question if the answer marked as official is updated.
    /// </remarks>
    /// <param name="updatedAnswer">The <see cref="Answer"/> that was updated.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the user associated with the answer is not found.
    /// </exception>
    public async Task SendEmailOnUpdateAnswer(Answer updatedAnswer)
    {
        var url = configuration["WebsiteSettings:QuestionUrl"];
        var websiteName = configuration["WebsiteSettings:Name"];
        var author = await userRepository.GetUserByIdAsync(updatedAnswer.UserId) 
                     ?? throw new ArgumentException($"User with ID {updatedAnswer.UserId} not found.");
        
        var emailTasks = new List<Task<bool>>();
        if (updatedAnswer.Status == AnswerStatus.Official.ToString())
        {
            var recipients = await userRepository.GetEmailsToNotifyOnUpdateOfficialAnswer(updatedAnswer.QuestionId);
            emailTasks.AddRange(recipients.Select(email => SendEmailAsync(new EmailRequest
            {
                To = email,
                Subject = $"Mise à jour de la réponse officielle dans {websiteName}",
                Body = @$"
                       <p>Bonjour,</p>
                       <p>Une réponse officielle que vous suivez a été mise à jour sur {websiteName} :</p>
                       <p><em>« {updatedAnswer.Content} »</em></p>
                       <p>Vous pouvez consulter la réponse actualisée ici :</p>
                       <p><a href=""{url}/{updatedAnswer.QuestionId}"">👉 Voir la réponse mise à jour</a></p>
                       <p>Merci de votre participation dans la communauté {websiteName} !</p>
                       <p>L'équipe {websiteName}</p>"
            })));
        }
        
        if (updatedAnswer.ModifiedBy != author.Id && author.NotificationPreference.NotifyOnMyQuestionOrAnswerModifiedByAdmin)
        {
            emailTasks.Add(SendEmailAsync(new EmailRequest
            {
                To = author.Email,
                Subject = $"Votre réponse a été mise à jour dans {websiteName} par un administrateur",
                Body = @$"
                       <p>Bonjour {author.Firstname},</p>
                       <p>Votre réponse sur {websiteName} a été modifiée par un administrateur :</p>
                       <p><em>« {updatedAnswer.Content} »</em></p>
                       <p>Vous pouvez consulter votre réponse mise à jour ici :</p>
                       <p><a href=""{url}/{updatedAnswer.QuestionId}"">👉 Voir ma réponse mise à jour</a></p>
                       <p>Merci pour votre implication dans {websiteName}.</p>
                       <p>L'équipe {websiteName}</p>"
            }));
        }
        
        await SafeWhenAll(emailTasks);
    }
    
    /// <summary>
    /// Sends an email notification when a new answer is published.
    /// </summary>
    /// <remarks>
    /// Notifies users who follow the question and the question author if they opted in for this type of notification.
    /// </remarks>
    /// <param name="answer">The <see cref="Answer"/> that was published.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the user associated with the answer is not found.
    /// </exception>
    public async Task SendEmailOnNewAnswer(Answer answer)
    {
        var url = configuration["WebsiteSettings:QuestionUrl"];
        var websiteName = configuration["WebsiteSettings:Name"];
        var author = await userRepository.GetUserByIdAsync(answer.Question.UserId) 
                     ?? throw new ArgumentException($"User with ID {answer.Question.UserId} not found.");
        
        var recipients = await userRepository.GetEmailsToNotifyOnNewAnswer(answer.UserId, answer.Question);
        var emailTasks = recipients
            .Select(email => SendEmailAsync(new EmailRequest
            {
                To = email,
                Subject = $"Nouvelle réponse sur {websiteName}",
                Body = @$"
                        <p>Bonjour,</p>
                        <p>Une nouvelle réponse a été publiée dans une question à laquelle vous avez participé :</p>
                        <p><em>« {answer.Content} »</em></p>
                        <p>Vous pouvez lire la réponse et continuer la discussion ici :</p>
                        <p><a href=""{url}/{answer.QuestionId}"">👉 Voir la réponse</a></p>
                        <p>Merci de faire partie de la communauté {websiteName} !</p>
                        <p>L'équipe {websiteName}</p>"
            }))
            .ToList();
        
        if (author.NotificationPreference.NotifyOnAnswerPublishedOnMyQuestion && answer.UserId != author.Id)
        {
            emailTasks.Add(SendEmailAsync(new EmailRequest
            {
                To = author.Email,
                Subject = $"Votre question a reçu une nouvelle réponse sur {websiteName}",
                Body = @$"
                        <p>Bonjour {author.Firstname},</p>
                        <p>Votre question a reçu une nouvelle réponse :</p>
                        <p><em>« {answer.Content} »</em></p>
                        <p>Vous pouvez consulter la réponse et y répondre si nécessaire :</p>
                        <p><a href=""{url}/{answer.QuestionId}"">👉 Voir la réponse à ma question</a></p>
                        <p>Merci pour votre participation sur {websiteName}.</p>
                        <p>L'équipe {websiteName}</p>"
            }));
        }
        
        await SafeWhenAll(emailTasks);
    }
    
    /// <summary>
    /// Sends an email notification when an answer is marked as official.
    /// </summary>
    /// <remarks>
    /// Notifies users who follow the question, the question author and the answer author if they opted in for this type of notification.
    /// </remarks>
    /// <param name="answer">The <see cref="Answer"/> that was marked as official.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the user associated with the answer is not found.
    /// </exception>
    public async Task SendEmailOnOfficialAnswer(Answer answer)
    {
        var url = configuration["WebsiteSettings:QuestionUrl"];
        var websiteName = configuration["WebsiteSettings:Name"];
        var author = await userRepository.GetUserByIdAsync(answer.Question.UserId) 
                     ?? throw new ArgumentException($"User with ID {answer.Question.UserId} not found.");
        
        var recipients = await userRepository.GetEmailsToNotifyOnOfficialAnswer(answer.UserId, answer.Question);
        var emailTasks = recipients
            .Select(email => SendEmailAsync(new EmailRequest
            {
                To = email,
                Subject = $"Réponse officielle sur {websiteName}",
                Body = @$"
                        <p>Bonjour,</p>
                        <p>Une réponse officielle a été publiée dans une question à laquelle vous vous intéressez :</p>
                        <p><em>« {answer.Content} »</em></p>
                        <p>Découvrez la réponse officielle ici :</p>
                        <p><a href=""{url}/{answer.QuestionId}"">👉 Voir la réponse officielle</a></p>
                        <p>Merci pour votre contribution à la communauté {websiteName} !</p>
                        <p>L'équipe {websiteName}</p>"
            }))
            .ToList();
        
        if (author.NotificationPreference.NotifyOnAnswerToMyQuestionMarkedOfficial)
        {
            emailTasks.Add(SendEmailAsync(new EmailRequest
            {
                To = author.Email,
                Subject = "Réponse officielle à votre question dans AssistClub",
                Body = @$"
                        <p>Bonjour {author.Firstname},</p>
                        <p>Une réponse officielle a été publiée pour votre question :</p>
                        <p><em>« {answer.Content} »</em></p>
                        <p>Vous pouvez consulter la réponse ici :</p>
                        <p><a href=""{url}/{answer.QuestionId}"">👉 Voir la réponse officielle</a></p>
                        <p>Merci d’avoir fait confiance à {websiteName}.</p>
                        <p>L'équipe {websiteName}</p>"
            }));
        }
        
        await SafeWhenAll(emailTasks);
    }
}