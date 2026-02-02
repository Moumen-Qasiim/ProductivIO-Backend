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

        public async Task<List<FlashcardsDto>> GetAllFlashcardsAsync(Guid userId)
        {
            return await _flashcardRepository.GetAllFlashcardsAsync(userId);
        }

        public async Task<FlashcardsDto?> GetFlashcardAsync(Guid id, Guid userId)
        {
            return await _flashcardRepository.GetFlashcardAsync(id, userId);
        }

        public async Task<FlashcardsDto?> AddFlashcardAsync(CreateFlashcardDto flashcard, Guid userId)
        {
            return await _flashcardRepository.AddFlashcardAsync(flashcard, userId);
        }

        public async Task<FlashcardsDto?> UpdateFlashcardAsync(Guid id, UpdateFlashcardDto flashcard, Guid userId)
        {
            return await _flashcardRepository.UpdateFlashcardAsync(id, flashcard, userId);
        }

        public async Task<bool> DeleteFlashcardAsync(Guid id, Guid userId)
        {
            return await _flashcardRepository.DeleteFlashcardAsync(id, userId);
        }

        public async Task<FlashcardQuestionDto?> AddQuestionAsync(Guid flashcardId, CreateFlashcardQuestionDto question, Guid userId)
        {
            return await _flashcardRepository.AddQuestionAsync(flashcardId, question, userId);
        }

        public async Task<FlashcardQuestionDto?> UpdateQuestionAsync(Guid questionId, UpdateFlashcardQuestionDto question, Guid userId)
        {
            return await _flashcardRepository.UpdateQuestionAsync(questionId, question, userId);
        }

        public async Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId)
        {
            return await _flashcardRepository.DeleteQuestionAsync(questionId, userId);
        }

        public async Task<FlashcardAnswerDto?> AddAnswerAsync(Guid questionId, CreateFlashcardAnswerDto answer, Guid userId)
        {
            return await _flashcardRepository.AddAnswerAsync(questionId, answer, userId);
        }

        public async Task<FlashcardAnswerDto?> UpdateAnswerAsync(Guid answerId, UpdateFlashcardAnswerDto answer, Guid userId)
        {
            return await _flashcardRepository.UpdateAnswerAsync(answerId, answer, userId);
        }

        public async Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId)
        {
            return await _flashcardRepository.DeleteAnswerAsync(answerId, userId);
        }
    }
}
