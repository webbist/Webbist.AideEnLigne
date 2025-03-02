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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07FE407C18");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346EC785CE").IsUnique();

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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
