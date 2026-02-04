using ProductivIO.Application.Repositories;
using ProductivIO.Application.Services.Interfaces;
using ProductivIO.Application.Mapping;
using ProductivIO.Contracts.Requests.Notes;
using ProductivIO.Contracts.Responses.Notes;
using ProductivIO.Domain.Entities;

namespace ProductivIO.Application.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<IEnumerable<NoteResponse>> GetAllAsync(Guid userId)
    {
        var notes = await _noteRepository.GetAllAsync(userId);
        return notes.Select(n => n.ToResponse());
    }

    public async Task<NoteResponse?> GetByIdAsync(Guid id, Guid userId)
    {
        var note = await _noteRepository.GetByIdAsync(id, userId);
        return note?.ToResponse();
    }

    public async Task<NoteResponse?> CreateAsync(CreateNoteRequest request, Guid userId)
    {
        var note = Note.Create(userId, request.Title, request.Content);
        var created = await _noteRepository.AddAsync(note);
        return created.ToResponse();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateNoteRequest request, Guid userId)
    {
        var note = await _noteRepository.GetByIdAsync(id, userId);
        if (note == null) return false;

        note.Update(request.Title, request.Content);
        return await _noteRepository.UpdateAsync(note);
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        return await _noteRepository.DeleteAsync(id, userId);
    }
}
