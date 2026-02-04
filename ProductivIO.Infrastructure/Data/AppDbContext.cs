using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductivIO.Domain.Entities;
using ProductivIO.Contracts.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProductivIO.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Note> Notes => Set<Note>();
    public DbSet<ProductivIO.Domain.Entities.Task> Tasks => Set<ProductivIO.Domain.Entities.Task>();
    public DbSet<Flashcard> Flashcards => Set<Flashcard>();
    public DbSet<FlashcardQuestion> FlashcardQuestions => Set<FlashcardQuestion>();
    public DbSet<FlashcardAnswer> FlashcardAnswers => Set<FlashcardAnswer>();
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
    public DbSet<QuizAnswer> QuizAnswers => Set<QuizAnswer>();
    public DbSet<QuizResult> QuizResults => Set<QuizResult>();
    public DbSet<QuizResultAnswer> QuizResultAnswers => Set<QuizResultAnswer>();
    public DbSet<Pomodoro> Pomodoros => Set<Pomodoro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Task Enums and Check Constraints
        modelBuilder.Entity<ProductivIO.Domain.Entities.Task>(entity =>
        {
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Task_Priority", $"[Priority] IN ({string.Join(", ", Enum.GetNames<TaskPriority>().Select(n => $"'{n}'"))})");
                t.HasCheckConstraint("CK_Task_Status", $"[Status] IN ({string.Join(", ", Enum.GetNames<ProductivIO.Contracts.Enums.TaskStatus>().Select(n => $"'{n}'"))})");
            });

            entity.Property(e => e.Priority)
                .HasConversion(new EnumToStringConverter<TaskPriority>())
                .HasMaxLength(20);
            
            entity.Property(e => e.Status)
                .HasConversion(new EnumToStringConverter<ProductivIO.Contracts.Enums.TaskStatus>())
                .HasMaxLength(20);
        });

        // Pomodoro Enums and Check Constraints
        modelBuilder.Entity<Pomodoro>(entity =>
        {
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Pomodoro_SessionType", $"[SessionType] IN ({string.Join(", ", Enum.GetNames<SessionType>().Select(n => $"'{n}'"))})");
            });

            entity.Property(e => e.SessionType)
                .HasConversion(new EnumToStringConverter<SessionType>())
                .HasMaxLength(20);
        });

        // Configure User relationships
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(u => u.Notes).WithOne(n => n.User).HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Tasks).WithOne(t => t.User).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Pomodoros).WithOne(p => p.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Flashcards).WithOne(f => f.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Quizzes).WithOne(q => q.User).HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        // Other entity specific constraints
        modelBuilder.Entity<Note>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<ProductivIO.Domain.Entities.Task>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasMany(e => e.Questions).WithOne(e => e.Flashcard).HasForeignKey(e => e.FlashcardId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FlashcardQuestion>(entity =>
        {
            entity.HasMany(e => e.Answers).WithOne(e => e.Question).HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasMany(e => e.Questions).WithOne(e => e.Quiz).HasForeignKey(e => e.QuizId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.HasMany(e => e.Answers).WithOne(e => e.Question).HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QuizResult>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnName("TakenAt");
            entity.HasOne(qr => qr.User).WithMany().HasForeignKey(qr => qr.UserId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(qr => qr.Quiz).WithMany().HasForeignKey(qr => qr.QuizId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.ResultAnswers).WithOne(e => e.QuizResult).HasForeignKey(e => e.QuizResultId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QuizResultAnswer>(entity =>
        {
            entity.HasOne(qra => qra.Question).WithMany().HasForeignKey(qra => qra.QuestionId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(qra => qra.Answer).WithMany().HasForeignKey(qra => qra.AnswerId).OnDelete(DeleteBehavior.Restrict);
        });

        // Global GUID configurations
        ConfigureEntitiesGuids(modelBuilder);
        // We will handle timestamps manually in SaveChangesAsync to ensure they work across providers
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                var createdAt = entry.Metadata.FindProperty("CreatedAt");
                if (createdAt != null)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            var updatedAt = entry.Metadata.FindProperty("UpdatedAt");
            if (updatedAt != null)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }

    private static void ConfigureEntitiesGuids(ModelBuilder mb)
    {
        foreach (var entityType in mb.Model.GetEntityTypes())
        {
            var idProperty = entityType.FindProperty("Id");
            if (idProperty != null && idProperty.ClrType == typeof(Guid))
            {
                mb.Entity(entityType.ClrType).Property("Id").HasDefaultValueSql("NEWSEQUENTIALID()");
            }
        }
    }
}
