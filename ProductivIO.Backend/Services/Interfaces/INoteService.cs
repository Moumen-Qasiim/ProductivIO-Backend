using ProductivIO.Backend.DTOs.Notes;

namespace ProductivIO.Backend.Services.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAll(Guid userId);
        Task<NoteDto?> Get(Guid id, Guid userId);
        Task<NoteDto?> Create(CreateNoteDto note, Guid userId);
        Task<bool> Update(Guid id, UpdateNoteDto note, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);
    }
}