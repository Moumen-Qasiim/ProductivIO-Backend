using System.Threading.Tasks;
using ProductivIOBackend.DTOs.Notes;
using ProductivIOBackend.Models;
using ProductivIOBackend.Services.Interfaces;
using ProductivIOBackend.Repositories.Interfaces;

namespace ProductivIOBackend.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }


        public async Task<IEnumerable<NoteDto>> GetAll(Guid   UserId)
        {
            var note = await _noteRepository.GetAllNotesAsync(UserId);

            return note.Select(t => new NoteDto
            {
                Id = t.Id,
                Title = t.Title,
                Content = t.Content,
                UserId = t.UserId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            });
        }

        public async Task<NoteDto?> Get(Guid   id, Guid   UserId)
        {
            var note = await _noteRepository.GetNoteAsync(id, UserId);
            if (note == null) return null;

            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                UserId = note.UserId,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            };
        }

        public async Task<NoteDto?> Create(NoteDto note)
        {
            var created = await _noteRepository.AddNoteAsync(note);
            if (created == null) return null;

            return new NoteDto
            {
                Id = created.Id,
                Title = created.Title,
                Content = created.Content,
                UserId = created.UserId,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> Update(Guid   id, NoteDto note)
        {
            if (id != note.Id) return false;

            var updated = await _noteRepository.UpdateNoteAsync(note);
            if (updated == null) return false;

            return true;
        }

        public async Task<bool> Delete(Guid   id, Guid   UserId)
        {
            return await _noteRepository.DeleteNoteAsync(id, UserId);
        }
    }
}