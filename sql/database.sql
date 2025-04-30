DROP TABLE IF EXISTS AnswerVotes;
DROP TABLE IF EXISTS NotificationPreferences;
DROP TABLE IF EXISTS Answers;
DROP TABLE IF EXISTS Questions;
DROP TABLE IF EXISTS Users;

DROP DATABASE IF EXISTS AssistClubDB;

CREATE DATABASE AssistClubDB;

USE AssistClubDB;

CREATE TABLE Users (
                       Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                       Firstname NVARCHAR(100) NOT NULL,
                       Lastname NVARCHAR(100) NOT NULL,
                       Email VARCHAR(100) UNIQUE NOT NULL,
                       Photo VARCHAR(255),
                       Club NVARCHAR(100) NOT NULL,
                       Microsite VARCHAR(255) NOT NULL,
                       Role NVARCHAR(50) NOT NULL CHECK (Role IN ('admin', 'user')),
);

CREATE TABLE Questions (
                           Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                           UserId UNIQUEIDENTIFIER NOT NULL,
                           Title NVARCHAR(255) NOT NULL,
                           Content NVARCHAR(MAX) NOT NULL,
                           CreatedAt DATETIME DEFAULT GETDATE(),
                           UpdatedAt DATETIME NULL,
                           Visibility NVARCHAR(50) NOT NULL CHECK (Visibility IN ('public', 'private')),
                           Status NVARCHAR(50) NOT NULL CHECK (Status IN ('open', 'pending', 'resolved')),
                           AttachmentName NVARCHAR(255) NULL,
                           ModifiedBy UNIQUEIDENTIFIER NULL,

                           CONSTRAINT FK_Questions_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE Answers (
                         Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                         QuestionId UNIQUEIDENTIFIER NOT NULL,
                         UserId UNIQUEIDENTIFIER NOT NULL,
                         Content NVARCHAR(MAX) NOT NULL,
                         Status NVARCHAR(50) NOT NULL CHECK (Status IN ('pending', 'official', 'archived')),
                         CreatedAt DATETIME DEFAULT GETDATE(),
                         UpdatedAt DATETIME NULL,
                         AttachmentName NVARCHAR(255) NULL,
                         ModifiedBy UNIQUEIDENTIFIER NULL,

                         CONSTRAINT FK_Answers_Questions FOREIGN KEY (QuestionId) REFERENCES Questions(Id) ON DELETE CASCADE,
                         CONSTRAINT FK_Answers_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE NotificationPreferences (
    UserId UNIQUEIDENTIFIER  PRIMARY KEY NOT NULL,

    -- Responsable de club
    NotifyOnNewClubQuestion BIT DEFAULT 1,

    -- Utilisateur
    NotifyOnAnswerPublishedOnMyQuestion BIT DEFAULT 1,
    NotifyOnAnswerToMyQuestionMarkedOfficial BIT DEFAULT 1,
    NotifyOnMyQuestionOrAnswerModifiedByAdmin BIT DEFAULT 1,
    NotifyOnAnyOfficialAnswerInQuestionIRelated BIT DEFAULT 1,
    NotifyOnQuestionIRelatedModifiedByAuthor BIT DEFAULT 1,
    NotifyOnNewAnswerInQuestionIRelated BIT DEFAULT 1,

    CONSTRAINT FK_NotificationPreferences_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE AnswerVotes (
                             AnswerId UNIQUEIDENTIFIER NOT NULL,
                             UserId UNIQUEIDENTIFIER NOT NULL,
                             CreatedAt DATETIME DEFAULT GETDATE(),

                             CONSTRAINT UQ_AnswerVotes_User_Answer PRIMARY KEY (UserId, AnswerId),
                             CONSTRAINT FK_AnswerVotes_Answers FOREIGN KEY (AnswerId) REFERENCES Answers(Id) ON DELETE CASCADE,
                             CONSTRAINT FK_AnswerVotes_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE NO ACTION
);