using ProductivIOBackend.DTOs.Flashcards;

namespace ProductivIOBackend.Repositories.Interfaces
{
    public interface IFlashcardRepository
    {
        // Flashcards
        Task<List<FlashcardsDto>> GetAllFlashcardsAsync(int userId);
        Task<FlashcardsDto?> GetFlashcardAsync(int id, int userId);
        Task<FlashcardsDto?> AddFlashcardAsync(FlashcardsDto dto);
        Task<FlashcardsDto?> UpdateFlashcardAsync(FlashcardsDto dto);
        Task<bool> DeleteFlashcardAsync(int id, int userId);

        // Questions
        Task<FlashcardQuestionDto?> AddQuestionAsync(int flashcardId, FlashcardQuestionDto dto);
        Task<FlashcardQuestionDto?> UpdateQuestionAsync(FlashcardQuestionDto dto);
        Task<bool> DeleteQuestionAsync(int questionId);

        // Answers
        Task<FlashcardAnswerDto?> AddAnswerAsync(int questionId, FlashcardAnswerDto dto);
        Task<FlashcardAnswerDto?> UpdateAnswerAsync(FlashcardAnswerDto dto);
        Task<bool> DeleteAnswerAsync(int answerId);
    }
}
