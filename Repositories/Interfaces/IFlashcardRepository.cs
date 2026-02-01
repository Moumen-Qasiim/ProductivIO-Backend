using ProductivIO.Backend.DTOs.Flashcards;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface IFlashcardRepository
    {
        // Flashcards
        Task<List<FlashcardsDto>> GetAllFlashcardsAsync(Guid userId);
        Task<FlashcardsDto?> GetFlashcardAsync(Guid id, Guid userId);
        Task<FlashcardsDto?> AddFlashcardAsync(FlashcardsDto dto);
        Task<FlashcardsDto?> UpdateFlashcardAsync(FlashcardsDto dto);
        Task<bool> DeleteFlashcardAsync(Guid id, Guid userId);

        // Questions
        Task<FlashcardQuestionDto?> AddQuestionAsync(Guid flashcardId, FlashcardQuestionDto dto);
        Task<FlashcardQuestionDto?> UpdateQuestionAsync(FlashcardQuestionDto dto);
        Task<bool> DeleteQuestionAsync(Guid questionId);

        // Answers
        Task<FlashcardAnswerDto?> AddAnswerAsync(Guid questionId, FlashcardAnswerDto dto);
        Task<FlashcardAnswerDto?> UpdateAnswerAsync(FlashcardAnswerDto dto);
        Task<bool> DeleteAnswerAsync(Guid answerId);
    }
}
