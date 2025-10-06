using Microsoft.EntityFrameworkCore;
using ProductivIOBackend.Data;
using ProductivIOBackend.DTOs.Flashcards;
using ProductivIOBackend.Models;
using ProductivIOBackend.Repositories.Interfaces;

namespace ProductivIOBackend.Repositories
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly AppDbContext _db;

        public FlashcardRepository(AppDbContext db)
        {
            _db = db;
        }


        // FLASHCARDS 
        public async Task<List<FlashcardsDto>> GetAllFlashcardsAsync(int userId)
        {
            var flashcards = await _db.Flashcards
                .Where(f => f.UserID == userId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return flashcards.Select(f => new FlashcardsDto
            {
                Id = f.Id,
                UserID = f.UserID,
                Title = f.Title,
                Description = f.Description,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt
            }).ToList();
        }

        public async Task<FlashcardsDto?> GetFlashcardAsync(int id, int userId)
        {
            var f = await _db.Flashcards
                .FirstOrDefaultAsync(fc => fc.Id == id && fc.UserID == userId);

            if (f == null) return null;

            return new FlashcardsDto
            {
                Id = f.Id,
                UserID = f.UserID,
                Title = f.Title,
                Description = f.Description,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt
            };
        }

        public async Task<FlashcardsDto?> AddFlashcardAsync(FlashcardsDto dto)
        {
            var entity = new Flashcards
            {
                UserID = dto.UserID,
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            _db.Flashcards.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        public async Task<FlashcardsDto?> UpdateFlashcardAsync(FlashcardsDto dto)
        {
            var existing = await _db.Flashcards
                .FirstOrDefaultAsync(f => f.Id == dto.Id && f.UserID == dto.UserID);

            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            dto.UpdatedAt = existing.UpdatedAt;
            return dto;
        }

        public async Task<bool> DeleteFlashcardAsync(int id, int userId)
        {
            var flashcard = await _db.Flashcards
                .Include(f => f.FlashcardQuestions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(f => f.Id == id && f.UserID == userId);

            if (flashcard == null) return false;

            _db.Flashcards.Remove(flashcard);
            await _db.SaveChangesAsync();
            return true;
        }


        // QUESTIONS

        public async Task<FlashcardQuestionDto?> AddQuestionAsync(int flashcardId, FlashcardQuestionDto dto)
        {
            var flashcard = await _db.Flashcards.FindAsync(flashcardId);
            if (flashcard == null) return null;

            var entity = new FlashcardQuestion
            {
                FlashcardId = flashcardId,
                Question = dto.Question,
                Hint = dto.Hint,
                CreatedAt = DateTime.Now
            };

            _db.FlashcardQuestions.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.FlashcardId = flashcardId;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        public async Task<FlashcardQuestionDto?> UpdateQuestionAsync(FlashcardQuestionDto dto)
        {
            var existing = await _db.FlashcardQuestions.FindAsync(dto.Id);
            if (existing == null) return null;

            existing.Question = dto.Question;
            existing.Hint = dto.Hint;
            existing.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            dto.UpdatedAt = existing.UpdatedAt;
            return dto;
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            var question = await _db.FlashcardQuestions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null) return false;

            _db.FlashcardQuestions.Remove(question);
            await _db.SaveChangesAsync();
            return true;
        }


        //  ANSWERS

        public async Task<FlashcardAnswerDto?> AddAnswerAsync(int questionId, FlashcardAnswerDto dto)
        {
            var question = await _db.FlashcardQuestions.FindAsync(questionId);
            if (question == null) return null;

            var entity = new FlashcardAnswer
            {
                QuestionId = questionId,
                Answer = dto.Answer,
                IsCorrect = dto.IsCorrect,
                CreatedAt = DateTime.Now
            };

            _db.FlashcardAnswers.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.QuestionId = questionId;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        public async Task<FlashcardAnswerDto?> UpdateAnswerAsync(FlashcardAnswerDto dto)
        {
            var existing = await _db.FlashcardAnswers.FindAsync(dto.Id);
            if (existing == null) return null;

            existing.Answer = dto.Answer;
            existing.IsCorrect = dto.IsCorrect;
            existing.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            dto.UpdatedAt = existing.UpdatedAt ?? DateTime.Now;
            return dto;
        }

        public async Task<bool> DeleteAnswerAsync(int answerId)
        {
            var answer = await _db.FlashcardAnswers.FindAsync(answerId);
            if (answer == null) return false;

            _db.FlashcardAnswers.Remove(answer);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
