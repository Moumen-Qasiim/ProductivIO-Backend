using ProductivIO.Backend.DTOs.Notes;

namespace ProductivIO.Backend.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<List<NoteDto>> GetAllNotesAsync(Guid userId);
    
        Task<NoteDto?> GetNoteAsync(Guid id, Guid userId);
        
        Task<NoteDto?> UpdateNoteAsync(NoteDto note);
        
        Task<NoteDto?> AddNoteAsync(NoteDto note);
        
        Task<bool> DeleteNoteAsync(Guid id, Guid userId);
    }
}