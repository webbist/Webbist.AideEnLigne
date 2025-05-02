using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssistClub.Infrastructure.Persistence;

public partial class AssistClubDbContext : DbContext
{
    public AssistClubDbContext()
    {
    }

    public AssistClubDbContext(DbContextOptions<AssistClubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<AnswerVote> AnswerVotes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<NotificationPreference> NotificationPreferences { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answers__3214EC0758CA3685");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AttachmentName).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Answers_Questions");

            entity.HasOne(d => d.User).WithMany(p => p.Answers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Users");
        });

        modelBuilder.Entity<AnswerVote>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AnswerId }).HasName("UQ_AnswerVotes_User_Answer");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.AnswerVotes)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_AnswerVotes_Answers");

            entity.HasOne(d => d.User).WithMany(p => p.AnswerVotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnswerVotes_Users");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07B4A6C2CB");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F627F886BB").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<NotificationPreference>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Notifica__1788CC4CF2D008AF");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.NotifyOnAnswerPublishedOnMyQuestion).HasDefaultValue(true);
            entity.Property(e => e.NotifyOnAnswerToMyQuestionMarkedOfficial).HasDefaultValue(true);
            entity.Property(e => e.NotifyOnAnyOfficialAnswerInQuestionIrelated)
                .HasDefaultValue(true)
                .HasColumnName("NotifyOnAnyOfficialAnswerInQuestionIRelated");
            entity.Property(e => e.NotifyOnMyQuestionOrAnswerModifiedByAdmin).HasDefaultValue(true);
            entity.Property(e => e.NotifyOnNewAnswerInQuestionIrelated)
                .HasDefaultValue(true)
                .HasColumnName("NotifyOnNewAnswerInQuestionIRelated");
            entity.Property(e => e.NotifyOnNewClubQuestion).HasDefaultValue(true);
            entity.Property(e => e.NotifyOnQuestionIrelatedModifiedByAuthor)
                .HasDefaultValue(true)
                .HasColumnName("NotifyOnQuestionIRelatedModifiedByAuthor");

            entity.HasOne(d => d.User).WithOne(p => p.NotificationPreference)
                .HasForeignKey<NotificationPreference>(d => d.UserId)
                .HasConstraintName("FK_NotificationPreferences_Users");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC0787B01AC0");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AttachmentName).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Visibility).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Questions_Users");

            entity.HasMany(d => d.Categories).WithMany(p => p.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "QuestionCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_QuestionCategories_Categories"),
                    l => l.HasOne<Question>().WithMany()
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("FK_QuestionCategories_Questions"),
                    j =>
                    {
                        j.HasKey("QuestionId", "CategoryId");
                        j.ToTable("QuestionCategories");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D3B156ED");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341BD98AD9").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Club).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Firstname).HasMaxLength(100);
            entity.Property(e => e.Lastname).HasMaxLength(100);
            entity.Property(e => e.Microsite)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
