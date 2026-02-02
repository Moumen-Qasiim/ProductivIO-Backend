using ProductivIO.Backend.DTOs.Notes;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<List<NoteDto>> GetAllNotesAsync(Guid userId);
        Task<NoteDto?> GetNoteAsync(Guid id, Guid userId);
        Task<NoteDto?> UpdateNoteAsync(Guid id, UpdateNoteDto note, Guid userId);
        Task<NoteDto?> AddNoteAsync(CreateNoteDto note, Guid userId);
        Task<bool> DeleteNoteAsync(Guid id, Guid userId);
    }
}