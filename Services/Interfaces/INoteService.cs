using ProductivIOBackend.DTOs.Notes;
using ProductivIOBackend.Models;

namespace ProductivIOBackend.Services.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAll(Guid userId);
        Task<NoteDto?> Get(Guid id, Guid  userId);
        Task<NoteDto?> Create(NoteDto task);
        Task<bool> Update(Guid id, NoteDto task);
        Task<bool> Delete(Guid id, Guid  userId);
    }
}