using ProductivIO.Backend.DTOs.Notes;
using ProductivIO.Backend.Repositories.Interfaces;
using ProductivIO.Backend.Services.Interfaces;

namespace ProductivIO.Backend.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IEnumerable<NoteDto>> GetAll(Guid userId)
        {
            return await _noteRepository.GetAllNotesAsync(userId);
        }

        public async Task<NoteDto?> Get(Guid id, Guid userId)
        {
            return await _noteRepository.GetNoteAsync(id, userId);
        }

        public async Task<NoteDto?> Create(CreateNoteDto note, Guid userId)
        {
            return await _noteRepository.AddNoteAsync(note, userId);
        }

        public async Task<bool> Update(Guid id, UpdateNoteDto note, Guid userId)
        {
            var updated = await _noteRepository.UpdateNoteAsync(id, note, userId);
            return updated != null;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            return await _noteRepository.DeleteNoteAsync(id, userId);
        }
    }
}