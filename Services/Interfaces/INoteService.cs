using ProductivIOBackend.DTOs.Notes;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAll(int userId);
        Task<NoteDto?> Get(int id, int userId);
        Task<NoteDto?> Create(NoteDto task);
        Task<bool> Update(int id, NoteDto task);
        Task<bool> Delete(int id, int userId);
    }
}