using ProductivIO.Backend.DTOs.Flashcards;
using ProductivIO.Backend.Repositories.Interfaces;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardRepository _flashcardRepository;

        public FlashcardService(IFlashcardRepository flashcardRepository)
        {
            _flashcardRepository = flashcardRepository;
        }

        // Flashcards 
        public async Task<List<FlashcardsDto>> GetAllFlashcardsAsync(Guid   UserId)
        {
            var flashcards = await _flashcardRepository.GetAllFlashcardsAsync(UserId);
            return flashcards.Select(f => new FlashcardsDto
            {
                Id = f.Id,
                UserId = f.UserId,
                Title = f.Title,
                Description = f.Description,
                CreatedAt = f.CreatedAt,
                UpdatedAt = f.UpdatedAt
            }).ToList();
        }

        public async Task<FlashcardsDto?> GetFlashcardAsync(Guid   id, Guid UserId)
        {
            var flashcard = await _flashcardRepository.GetFlashcardAsync(id, UserId);
            if (flashcard == null) return null;

            return new FlashcardsDto
            {
                Id = flashcard.Id,
                UserId = flashcard.UserId,
                Title = flashcard.Title,
                Description = flashcard.Description,
                CreatedAt = flashcard.CreatedAt,
                UpdatedAt = flashcard.UpdatedAt
            };
        }

        public async Task<FlashcardsDto?> AddFlashcardAsync(FlashcardsDto flashcard)
        {
            var created = await _flashcardRepository.AddFlashcardAsync(flashcard)!;
            if (created == null) return null;
            return new FlashcardsDto
            {
                Id = created.Id,
                UserId = created.UserId,
                Title = created.Title,
                Description = created.Description,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<FlashcardsDto?> UpdateFlashcardAsync(FlashcardsDto flashcard)
        {
            var updated = await _flashcardRepository.UpdateFlashcardAsync(flashcard);
            if (updated == null) return null;

            return new FlashcardsDto
            {
                Id = updated.Id,
                UserId = updated.UserId,
                Title = updated.Title,
                Description = updated.Description,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt
            };
        }

        public async Task<bool> DeleteFlashcardAsync(Guid   id, Guid UserId)
        {
            return await _flashcardRepository.DeleteFlashcardAsync(id, UserId);
        }

        // Flashcard Questions 
        public async Task<FlashcardQuestionDto?> AddQuestionAsync(Guid   flashcardId, FlashcardQuestionDto question)
        {
            var created = await _flashcardRepository.AddQuestionAsync(flashcardId, question);
            if (created == null) return null;

            return new FlashcardQuestionDto
            {
                Id = created.Id,
                FlashcardId = created.FlashcardId,
                Question = created.Question,
                Hint = created.Hint,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<FlashcardQuestionDto?> UpdateQuestionAsync(FlashcardQuestionDto question)
        {
            var updated = await _flashcardRepository.UpdateQuestionAsync(question);
            if (updated == null) return null;

            return new FlashcardQuestionDto
            {
                Id = updated.Id,
                FlashcardId = updated.FlashcardId,
                Question = updated.Question,
                Hint = updated.Hint,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt
            };
        }

        public async Task<bool> DeleteQuestionAsync(Guid   questionId)
        {
            return await _flashcardRepository.DeleteQuestionAsync(questionId);
        }

        // Flashcard Answers
        public async Task<FlashcardAnswerDto?> AddAnswerAsync(Guid   questionId, FlashcardAnswerDto answer)
        {
            var created = await _flashcardRepository.AddAnswerAsync(questionId, answer);
            if (created == null) return null;

            return new FlashcardAnswerDto
            {
                Id = created.Id,
                QuestionId = created.QuestionId,
                Answer = created.Answer,
                IsCorrect = created.IsCorrect,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<FlashcardAnswerDto?> UpdateAnswerAsync(FlashcardAnswerDto answer)
        {
            var updated = await _flashcardRepository.UpdateAnswerAsync(answer);
            if (updated == null) return null;

            return new FlashcardAnswerDto
            {
                Id = updated.Id,
                QuestionId = updated.QuestionId,
                Answer = updated.Answer,
                IsCorrect = updated.IsCorrect,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt
            };
        }

        public async Task<bool> DeleteAnswerAsync(Guid   answerId)
        {
            return await _flashcardRepository.DeleteAnswerAsync(answerId);
        }
    }
}
