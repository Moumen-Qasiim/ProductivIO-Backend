using ProductivIO.Backend.DTOs.Flashcards;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface IFlashcardService
    {
        Task<List<FlashcardsDto>> GetAllFlashcardsAsync(Guid userId);
        Task<FlashcardsDto?> GetFlashcardAsync(Guid id, Guid userId);
        Task<FlashcardsDto?> AddFlashcardAsync(CreateFlashcardDto flashcard, Guid userId);
        Task<FlashcardsDto?> UpdateFlashcardAsync(Guid id, UpdateFlashcardDto flashcard, Guid userId);
        Task<bool> DeleteFlashcardAsync(Guid id, Guid userId);

        Task<FlashcardQuestionDto?> AddQuestionAsync(Guid flashcardId, CreateFlashcardQuestionDto question, Guid userId);
        Task<FlashcardQuestionDto?> UpdateQuestionAsync(Guid questionId, UpdateFlashcardQuestionDto question, Guid userId);
        Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId);

        Task<FlashcardAnswerDto?> AddAnswerAsync(Guid questionId, CreateFlashcardAnswerDto answer, Guid userId);
        Task<FlashcardAnswerDto?> UpdateAnswerAsync(Guid answerId, UpdateFlashcardAnswerDto answer, Guid userId);
        Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId);
    }
}
