using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.DTOs.Flashcards;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Repositories.Interfaces;

namespace ProductivIO.Backend.Repositories
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly AppDbContext _db;

        public FlashcardRepository(AppDbContext db)
        {
            _db = db;
        }

        // Flashcards
        public async Task<List<FlashcardsDto>> GetAllFlashcardsAsync(Guid userId)
        {
            return await _db.Flashcards
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new FlashcardsDto
                {
                    Id = f.Id,
                    UserId = f.UserId,
                    Title = f.Title,
                    Description = f.Description,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<FlashcardsDto?> GetFlashcardAsync(Guid id, Guid userId)
        {
            var f = await _db.Flashcards
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (f == null) return null;

            return new FlashcardsDto
            {
                Id = f.Id,
                UserId = f.UserId,
                Title = f.Title,
                Description = f.Description,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt
            };
        }

        public async Task<FlashcardsDto?> AddFlashcardAsync(CreateFlashcardDto dto, Guid userId)
        {
            var entity = new Flashcards
            {
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description
            };

            _db.Flashcards.Add(entity);
            await _db.SaveChangesAsync();

            return new FlashcardsDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Title = entity.Title,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<FlashcardsDto?> UpdateFlashcardAsync(Guid id, UpdateFlashcardDto dto, Guid userId)
        {
            var existing = await _db.Flashcards
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Description = dto.Description;

            await _db.SaveChangesAsync();

            return new FlashcardsDto
            {
                Id = existing.Id,
                UserId = existing.UserId,
                Title = existing.Title,
                Description = existing.Description,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteFlashcardAsync(Guid id, Guid userId)
        {
            var flashcard = await _db.Flashcards
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (flashcard == null) return false;

            _db.Flashcards.Remove(flashcard);
            await _db.SaveChangesAsync();
            return true;
        }

        // Questions
        public async Task<FlashcardQuestionDto?> AddQuestionAsync(Guid flashcardId, CreateFlashcardQuestionDto dto, Guid userId)
        {
            var flashcard = await _db.Flashcards.AnyAsync(f => f.Id == flashcardId && f.UserId == userId);
            if (!flashcard) return null;

            var entity = new FlashcardQuestion
            {
                FlashcardId = flashcardId,
                Question = dto.Question,
                Hint = dto.Hint
            };

            _db.FlashcardQuestions.Add(entity);
            await _db.SaveChangesAsync();

            return new FlashcardQuestionDto
            {
                Id = entity.Id,
                FlashcardId = entity.FlashcardId,
                Question = entity.Question,
                Hint = entity.Hint,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<FlashcardQuestionDto?> UpdateQuestionAsync(Guid questionId, UpdateFlashcardQuestionDto dto, Guid userId)
        {
            var existing = await _db.FlashcardQuestions
                .Include(q => q.Flashcard)
                .FirstOrDefaultAsync(q => q.Id == questionId && q.Flashcard!.UserId == userId);

            if (existing == null) return null;

            existing.Question = dto.Question;
            existing.Hint = dto.Hint;

            await _db.SaveChangesAsync();

            return new FlashcardQuestionDto
            {
                Id = existing.Id,
                FlashcardId = existing.FlashcardId,
                Question = existing.Question,
                Hint = existing.Hint,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId)
        {
            var question = await _db.FlashcardQuestions
                .Include(q => q.Flashcard)
                .FirstOrDefaultAsync(q => q.Id == questionId && q.Flashcard!.UserId == userId);

            if (question == null) return false;

            _db.FlashcardQuestions.Remove(question);
            await _db.SaveChangesAsync();
            return true;
        }

        // Answers
        public async Task<FlashcardAnswerDto?> AddAnswerAsync(Guid questionId, CreateFlashcardAnswerDto dto, Guid userId)
        {
            var question = await _db.FlashcardQuestions
                .Include(q => q.Flashcard)
                .AnyAsync(q => q.Id == questionId && q.Flashcard!.UserId == userId);
            if (!question) return null;

            var entity = new FlashcardAnswer
            {
                QuestionId = questionId,
                Answer = dto.Answer,
                IsCorrect = dto.IsCorrect
            };

            _db.FlashcardAnswers.Add(entity);
            await _db.SaveChangesAsync();

            return new FlashcardAnswerDto
            {
                Id = entity.Id,
                QuestionId = entity.QuestionId,
                Answer = entity.Answer,
                IsCorrect = entity.IsCorrect,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<FlashcardAnswerDto?> UpdateAnswerAsync(Guid answerId, UpdateFlashcardAnswerDto dto, Guid userId)
        {
            var existing = await _db.FlashcardAnswers
                .Include(a => a.FlashcardQuestion)
                    .ThenInclude(q => q!.Flashcard)
                .FirstOrDefaultAsync(a => a.Id == answerId && a.FlashcardQuestion!.Flashcard!.UserId == userId);

            if (existing == null) return null;

            existing.Answer = dto.Answer;
            existing.IsCorrect = dto.IsCorrect;

            await _db.SaveChangesAsync();

            return new FlashcardAnswerDto
            {
                Id = existing.Id,
                QuestionId = existing.QuestionId,
                Answer = existing.Answer,
                IsCorrect = existing.IsCorrect,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId)
        {
            var answer = await _db.FlashcardAnswers
                .Include(a => a.FlashcardQuestion)
                    .ThenInclude(q => q!.Flashcard)
                .FirstOrDefaultAsync(a => a.Id == answerId && a.FlashcardQuestion!.Flashcard!.UserId == userId);

            if (answer == null) return false;

            _db.FlashcardAnswers.Remove(answer);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
