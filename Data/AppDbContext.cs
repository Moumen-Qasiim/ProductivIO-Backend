using Microsoft.EntityFrameworkCore;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Map models to tables
    public DbSet<User> Users => Set<User>();
    public DbSet<Notes> Notes => Set<Notes>();
    public DbSet<Tasks> Tasks => Set<Tasks>();
    public DbSet<Pomodoro> Pomodoros => Set<Pomodoro>();
    public DbSet<Flashcards> Flashcards => Set<Flashcards>();
    public DbSet<FlashcardQuestion> FlashcardQuestions => Set<FlashcardQuestion>();
    public DbSet<FlashcardAnswer> FlashcardAnswers => Set<FlashcardAnswer>();
    public DbSet<Quizzes> Quizzes => Set<Quizzes>();
    public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
    public DbSet<QuizAnswer> QuizAnswers => Set<QuizAnswer>();
    public DbSet<QuizResult> QuizResults => Set<QuizResult>();
    public DbSet<QuizResultAnswer> QuizResultAnswers => Set<QuizResultAnswer>();


    // Configure relationships and cascade delete
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //  User relationships
        modelBuilder.Entity<User>()
            .HasMany(u => u.Notes)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Pomodoros)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Flashcards)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Quizzes)
            .WithOne(q => q.User)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        //  Quiz relationships
        modelBuilder.Entity<Quizzes>()
            .HasMany(q => q.QuizQuestions)
            .WithOne(qq => qq.Quiz)
            .HasForeignKey(qq => qq.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizQuestion>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.QuizQuestion)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        //  Flashcard relationships
        modelBuilder.Entity<Flashcards>()
            .HasMany(f => f.FlashcardQuestions)
            .WithOne(q => q.Flashcard)
            .HasForeignKey(q => q.FlashcardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FlashcardQuestion>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.FlashcardQuestion)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        //  Default values for timestamp
        modelBuilder.Entity<Notes>()
            .Property(n => n.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Tasks>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Pomodoro>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");


        // Quiz Result
        modelBuilder.Entity<QuizResult>()
            .HasOne(qr => qr.User)
            .WithMany()
            .HasForeignKey(qr => qr.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<QuizResult>()
            .HasOne(qr => qr.Quiz)
            .WithMany()
            .HasForeignKey(qr => qr.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizResultAnswer>()
            .HasOne(qra => qra.QuizResult)
            .WithMany(qr => qr.ResultAnswers)
            .HasForeignKey(qra => qra.QuizResultId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuizResultAnswer>()
            .HasOne(qra => qra.Question)
            .WithMany()
            .HasForeignKey(qra => qra.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<QuizResultAnswer>()
            .HasOne(qra => qra.Answer)
            .WithMany()
            .HasForeignKey(qra => qra.AnswerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configures domain entities to auto-generate IDs for SQL Server
        ConfigureEntitiesGuids(modelBuilder);
        // Configures domain entities to auto-generate CreatedAt and UpdatedAt on insert and Update operations for SQL Server
        ConfigureEntitiesTimestamps(modelBuilder);
    }

    #region Private functions
    
    /// <summary>
    /// Scans all registered domain entities and configures Guid primary keys 
    /// to be automatically generated by the database using sequential GUIDs.
    /// </summary>
    /// <remarks>
    /// This uses 'NEWSEQUENTIALID()' which is optimized for SQL Server to prevent 
    /// index fragmentation.
    /// <para>
    /// <b>Note:</b> This logic specifically looks for a property named exactly "Id". 
    /// if an entity uses a different naming convention (e.g., "UserId"), it will not be configured here.
    /// </para>
    /// </remarks>
    /// <param name="mb">The <see cref="ModelBuilder"/> instance used to configure the model schema.</param>
    private static void ConfigureEntitiesGuids(ModelBuilder mb)
    {
        foreach (var entityType in mb.Model.GetEntityTypes())
        {
            var idProperty = entityType.FindProperty("Id");
            if (idProperty != null && idProperty.ClrType == typeof(Guid))
            {
                mb.Entity(entityType.ClrType)
                .Property("Id")
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            }
        }
    }

    /// <summary>
    /// Scans all registered domain entities and configures timestamp properties 
    /// to be automatically handled by the database or the persistence layer.
    /// </summary>
    /// <remarks>
    /// This configuration applies the following rules:
    /// <list type="bullet">
    /// <item>
    /// <description><b>CreatedAt:</b> Sets a default value of 'GETUTCDATE()' for new records.</description>
    /// </item>
    /// <item>
    /// <description><b>UpdatedAt:</b> Configures the property to be refreshed on both inserts and updates.</description>
    /// </item>
    /// </list>
    /// <para>
    /// <b>Note:</b> This logic specifically targets properties named exactly "CreatedAt" and "UpdatedAt". 
    /// Properties with different names (e.g., "Timestamp" or "DateCreated") will be ignored.
    /// </para>
    /// </remarks>
    /// <param name="mb">The <see cref="ModelBuilder"/> instance used to configure the model schema.</param>
    private static void ConfigureEntitiesTimestamps(ModelBuilder mb)
    {
        foreach (var entityType in mb.Model.GetEntityTypes())
        {
            // 1. Handle CreatedAt (Insert only)
            var createdAtProperty = entityType.FindProperty("CreatedAt");
            if (createdAtProperty != null && createdAtProperty.ClrType == typeof(DateTime))
            {
                var propertyBuilder = mb.Entity(entityType.ClrType).Property("CreatedAt");

                
                    propertyBuilder
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();
            }

            // 2. Handle UpdatedAt (Insert and Update)
            var updatedAtProperty = entityType.FindProperty("UpdatedAt");
            if (updatedAtProperty != null && updatedAtProperty.ClrType == typeof(DateTime))
            {
                mb.Entity(entityType.ClrType)
                    .Property("UpdatedAt")
                    .HasDefaultValueSql("GETUTCDATE()") // Sets initial value on insert
                    .ValueGeneratedOnAddOrUpdate();   
            }
        }
    }   

    #endregion     
}

