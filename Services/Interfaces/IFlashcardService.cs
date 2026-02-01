using ProductivIOBackend.DTOs.Flashcards;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface IFlashcardService
    {
        Task<List<FlashcardsDto>> GetAllFlashcardsAsync(Guid userId);
        Task<FlashcardsDto?> GetFlashcardAsync(Guid id, Guid userId);
        Task<FlashcardsDto?> AddFlashcardAsync(FlashcardsDto flashcard);
        Task<FlashcardsDto?> UpdateFlashcardAsync(FlashcardsDto flashcard);
        Task<bool> DeleteFlashcardAsync(Guid id, Guid  userId);

        Task<FlashcardQuestionDto?> AddQuestionAsync(Guid flashcardId, FlashcardQuestionDto question);
        Task<FlashcardQuestionDto?> UpdateQuestionAsync(FlashcardQuestionDto question);
        Task<bool> DeleteQuestionAsync(Guid questionId);

        Task<FlashcardAnswerDto?> AddAnswerAsync(Guid questionId, FlashcardAnswerDto answer);
        Task<FlashcardAnswerDto?> UpdateAnswerAsync(FlashcardAnswerDto answer);
        Task<bool> DeleteAnswerAsync(Guid answerId);
    }
}
