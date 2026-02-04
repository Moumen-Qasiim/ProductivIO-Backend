using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Application.Mapping;
using ProductivIO.Contracts.Requests.Flashcards;
using ProductivIO.Contracts.Responses.Flashcards;
using ProductivIO.Domain.Entities;

namespace ProductivIO.Application.Services;

public class FlashcardService : IFlashcardService
{
    private readonly IFlashcardRepository _flashcardRepository;

    public FlashcardService(IFlashcardRepository flashcardRepository)
    {
        _flashcardRepository = flashcardRepository;
    }

    public async Task<IEnumerable<FlashcardResponse>> GetAllAsync(Guid userId)
    {
        var flashcards = await _flashcardRepository.GetAllAsync(userId);
        return flashcards.Select(f => f.ToResponse());
    }

    public async Task<FlashcardResponse?> GetByIdAsync(Guid id, Guid userId)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(id, userId);
        return flashcard?.ToResponse();
    }

    public async Task<FlashcardResponse?> CreateAsync(CreateFlashcardRequest request, Guid userId)
    {
        var flashcard = Flashcard.Create(userId, request.Title, request.Description);
        var created = await _flashcardRepository.AddAsync(flashcard);
        return created.ToResponse();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateFlashcardRequest request, Guid userId)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(id, userId);
        if (flashcard == null) return false;

        flashcard.Update(request.Title, request.Description);
        return await _flashcardRepository.UpdateAsync(flashcard);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        return await _flashcardRepository.DeleteAsync(id, userId);
    }

    public async Task<FlashcardQuestionResponse?> AddQuestionAsync(Guid flashcardId, CreateFlashcardQuestionRequest request, Guid userId)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(flashcardId, userId);
        if (flashcard == null) return null;

        var question = FlashcardQuestion.Create(flashcardId, request.Question, request.Hint);
        var created = await _flashcardRepository.AddQuestionAsync(question);
        return created.ToResponse();
    }

    public async Task<bool> UpdateQuestionAsync(Guid questionId, UpdateFlashcardQuestionRequest request, Guid userId)
    {
        var question = await _flashcardRepository.GetQuestionByIdAsync(questionId, userId);
        if (question == null) return false;

        question.Update(request.Question, request.Hint);
        return await _flashcardRepository.UpdateQuestionAsync(question);
    }

    public async Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId)
    {
        return await _flashcardRepository.DeleteQuestionAsync(questionId, userId);
    }

    public async Task<FlashcardAnswerResponse?> AddAnswerAsync(Guid questionId, CreateFlashcardAnswerRequest request, Guid userId)
    {
        var question = await _flashcardRepository.GetQuestionByIdAsync(questionId, userId);
        if (question == null) return null;

        var answer = FlashcardAnswer.Create(questionId, request.Answer, request.IsCorrect);
        var created = await _flashcardRepository.AddAnswerAsync(answer);
        return created.ToResponse();
    }

    public async Task<bool> UpdateAnswerAsync(Guid answerId, UpdateFlashcardAnswerRequest request, Guid userId)
    {
        var answer = await _flashcardRepository.GetAnswerByIdAsync(answerId, userId);
        if (answer == null) return false;

        answer.Update(request.Answer, request.IsCorrect);
        return await _flashcardRepository.UpdateAnswerAsync(answer);
    }

    public async Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId)
    {
        return await _flashcardRepository.DeleteAnswerAsync(answerId, userId);
    }
}
