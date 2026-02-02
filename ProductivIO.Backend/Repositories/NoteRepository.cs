using Microsoft.EntityFrameworkCore;
using ProductivIO.Backend.Data;
using ProductivIO.Backend.DTOs.Notes;
using ProductivIO.Backend.Models;
using ProductivIO.Backend.Repositories.Interfaces;

namespace ProductivIO.Backend.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _db;

        public NoteRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<NoteDto>> GetAllNotesAsync(Guid userId)
        {
            return await _db.Notes
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Title = n.Title,
                    Content = n.Content,
                    CreatedAt = n.CreatedAt,
                    UpdatedAt = n.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<NoteDto?> GetNoteAsync(Guid id, Guid userId)
        {
            var note = await _db.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return null;

            return new NoteDto
            {
                Id = note.Id,
                UserId = note.UserId,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            };
        }

        public async Task<NoteDto?> AddNoteAsync(CreateNoteDto dto, Guid userId)
        {
            var entity = new Notes
            {
                UserId = userId,
                Title = dto.Title,
                Content = dto.Content
            };

            _db.Notes.Add(entity);
            await _db.SaveChangesAsync();

            return new NoteDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Title = entity.Title,
                Content = entity.Content,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<NoteDto?> UpdateNoteAsync(Guid id, UpdateNoteDto dto, Guid userId)
        {
            var existing = await _db.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Content = dto.Content;
            // UpdatedAt is handled by database default if configured in DbContext

            await _db.SaveChangesAsync();

            return new NoteDto
            {
                Id = existing.Id,
                UserId = existing.UserId,
                Title = existing.Title,
                Content = existing.Content,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
        }

        public async Task<bool> DeleteNoteAsync(Guid id, Guid userId)
        {
            var note = await _db.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return false;

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
