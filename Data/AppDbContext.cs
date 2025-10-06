using Microsoft.EntityFrameworkCore;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Data
{
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
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Pomodoros)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Flashcards)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Quizzes)
                .WithOne(q => q.User)
                .HasForeignKey(q => q.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            //  Quiz relationships
            modelBuilder.Entity<Quizzes>()
                .HasMany(q => q.QuizQuestions)
                .WithOne(qq => qq.Quiz)
                .HasForeignKey(qq => qq.QuizID)
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

        }
    }
}
