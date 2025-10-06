using ProductivIOBackend.DTOs.Flashcards;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface IFlashcardService
    {
        Task<List<FlashcardsDto>> GetAllFlashcardsAsync(int userId);
        Task<FlashcardsDto?> GetFlashcardAsync(int id, int userId);
        Task<FlashcardsDto?> AddFlashcardAsync(FlashcardsDto flashcard);
        Task<FlashcardsDto?> UpdateFlashcardAsync(FlashcardsDto flashcard);
        Task<bool> DeleteFlashcardAsync(int id, int userId);

        Task<FlashcardQuestionDto?> AddQuestionAsync(int flashcardId, FlashcardQuestionDto question);
        Task<FlashcardQuestionDto?> UpdateQuestionAsync(FlashcardQuestionDto question);
        Task<bool> DeleteQuestionAsync(int questionId);

        Task<FlashcardAnswerDto?> AddAnswerAsync(int questionId, FlashcardAnswerDto answer);
        Task<FlashcardAnswerDto?> UpdateAnswerAsync(FlashcardAnswerDto answer);
        Task<bool> DeleteAnswerAsync(int answerId);
    }
}
