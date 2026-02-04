using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Application.Mapping;
using ProductivIO.Contracts.Requests.Quiz;
using ProductivIO.Contracts.Responses.Quiz;
using ProductivIO.Domain.Entities;

namespace ProductivIO.Application.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuizResultRepository _quizResultRepository;

    public QuizService(IQuizRepository quizRepository, IQuizResultRepository quizResultRepository)
    {
        _quizRepository = quizRepository;
        _quizResultRepository = quizResultRepository;
    }

    public async Task<IEnumerable<QuizResponse>> GetAllAsync(Guid userId)
    {
        var quizzes = await _quizRepository.GetAllAsync(userId);
        return quizzes.Select(q => q.ToResponse());
    }

    public async Task<QuizResponse?> GetByIdAsync(Guid id, Guid userId)
    {
        var quiz = await _quizRepository.GetByIdAsync(id, userId);
        return quiz?.ToResponse();
    }

    public async Task<QuizResponse?> CreateAsync(CreateQuizRequest request, Guid userId)
    {
        var quiz = Quiz.Create(userId, request.Title, request.Description);
        var created = await _quizRepository.AddAsync(quiz);
        return created.ToResponse();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateQuizRequest request, Guid userId)
    {
        var quiz = await _quizRepository.GetByIdAsync(id, userId);
        if (quiz == null) return false;

        quiz.Update(request.Title, request.Description);
        return await _quizRepository.UpdateAsync(quiz);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        return await _quizRepository.DeleteAsync(id, userId);
    }

    public async Task<QuizQuestionResponse?> AddQuestionAsync(Guid quizId, CreateQuizQuestionRequest request, Guid userId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId, userId);
        if (quiz == null) return null;

        var question = QuizQuestion.Create(quizId, request.Question);
        foreach (var answerReq in request.Answers)
        {
            var answer = QuizAnswer.Create(Guid.Empty, answerReq.Answer, answerReq.IsCorrect);
            question.AddAnswer(answer);
        }

        var created = await _quizRepository.AddQuestionAsync(question);
        return created.ToResponse();
    }

    public async Task<bool> UpdateQuestionAsync(Guid questionId, UpdateQuizQuestionRequest request, Guid userId)
    {
        var question = await _quizRepository.GetQuestionByIdAsync(questionId, userId);
        if (question == null) return false;

        question.Update(request.Question);
        return await _quizRepository.UpdateQuestionAsync(question);
    }

    public async Task<bool> DeleteQuestionAsync(Guid questionId, Guid userId)
    {
        return await _quizRepository.DeleteQuestionAsync(questionId, userId);
    }

    public async Task<QuizAnswerResponse?> AddAnswerAsync(Guid questionId, CreateQuizAnswerRequest request, Guid userId)
    {
        var question = await _quizRepository.GetQuestionByIdAsync(questionId, userId);
        if (question == null) return null;

        var answer = QuizAnswer.Create(questionId, request.Answer, request.IsCorrect);
        var created = await _quizRepository.AddAnswerAsync(answer);
        return created.ToResponse();
    }

    public async Task<bool> UpdateAnswerAsync(Guid answerId, UpdateQuizAnswerRequest request, Guid userId)
    {
        var answer = await _quizRepository.GetAnswerByIdAsync(answerId, userId);
        if (answer == null) return false;

        answer.Update(request.Answer, request.IsCorrect);
        return await _quizRepository.UpdateAnswerAsync(answer);
    }

    public async Task<bool> DeleteAnswerAsync(Guid answerId, Guid userId)
    {
        return await _quizRepository.DeleteAnswerAsync(answerId, userId);
    }

    public async Task<QuizResultResponse?> SubmitResultAsync(SubmitQuizResultRequest request, Guid userId)
    {
        var quiz = await _quizRepository.GetByIdAsync(request.QuizId, userId);
        if (quiz == null) return null;

        int correctAnswers = 0;
        var resultAnswers = new List<QuizResultAnswer>();

        foreach (var answerReq in request.Answers)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == answerReq.QuestionId);
            if (question == null) continue;

            var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == answerReq.AnswerId);
            bool isCorrect = selectedAnswer?.IsCorrect ?? false;
            if (isCorrect) correctAnswers++;

            resultAnswers.Add(QuizResultAnswer.Create(Guid.Empty, answerReq.QuestionId, answerReq.AnswerId, isCorrect));
        }

        var result = QuizResult.Create(userId, request.QuizId, correctAnswers, quiz.Questions.Count, correctAnswers);
        foreach (var ra in resultAnswers) result.AddResultAnswer(ra);

        var created = await _quizResultRepository.AddAsync(result);
        return created.ToResponse();
    }
}
