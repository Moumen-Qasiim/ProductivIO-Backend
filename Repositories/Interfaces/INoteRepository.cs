using ProductivIOBackend.DTOs;
using ProductivIOBackend.Models;
using ProductivIOBackend.DTOs.Notes;

namespace ProductivIOBackend.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<List<NoteDto>> GetAllNotesAsync(int userId);
        Task<NoteDto?> GetNoteAsync(int Id, int userId);
        Task<NoteDto?> UpdateNoteAsync(NoteDto note);
        Task<NoteDto?> AddNoteAsync(NoteDto note);
        Task<bool> DeleteNoteAsync(int Id, int userId);
    }
}