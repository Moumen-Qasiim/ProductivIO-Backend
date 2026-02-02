using ProductivIO.Backend.DTOs.Flashcards;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface IFlashcardRepository
    {
        // Flashcards
        Task<List<FlashcardsDto>> GetAllFlashcardsAsync(Guid userId);
        Task<FlashcardsDto?> GetFlashcardAsync(Guid id, Guid userId);
        Task<FlashcardsDto?> AddFlashcardAsync(CreateFlashcardDto dto, Guid userId);
        Task<FlashcardsDto?> UpdateFlashcardAsync(Guid id, UpdateFlashcardDto dto, Guid userId);
        Task<bool> DeleteFlashcardAsync(Guid id, Guid userId);

        // Questions
        Task<FlashcardQuestionDto?> AddQuestionAsync(Guid flashcardId, CreateFlashcardQuestionDto dto, Guid userId);
        Task<FlashcardQuestionDto?> UpdateQuestionAsync(Guid questionId, UpdateFlashcardQuestionDto dto, Guid userId);
        Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId);

        // Answers
        Task<FlashcardAnswerDto?> AddAnswerAsync(Guid questionId, CreateFlashcardAnswerDto dto, Guid userId);
        Task<FlashcardAnswerDto?> UpdateAnswerAsync(Guid answerId, UpdateFlashcardAnswerDto dto, Guid userId);
        Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId);
    }
}
