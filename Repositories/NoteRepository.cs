using Microsoft.EntityFrameworkCore;
using ProductivIOBackend.Data;
using ProductivIOBackend.Models;
using ProductivIOBackend.Repositories.Interfaces;
using ProductivIOBackend.DTOs.Notes;

namespace ProductivIOBackend.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _db;

        public NoteRepository(AppDbContext db)
        {
            _db = db;
        }

        // Get all notes for a specific user
        public async Task<List<NoteDto>> GetAllNotesAsync(int userId)
        {
            return await _db.Notes
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    UserID = n.UserID,
                    Title = n.Title,
                    Content = n.Content,
                    CreatedAt = n.CreatedAt,
                    UpdatedAt = n.UpdatedAt
                })
                .ToListAsync();
        }

        // Get a single note by ID
        public async Task<NoteDto?> GetNoteAsync(int id, int userId)
        {
            var note = await _db.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserID == userId);

            if (note == null) return null;

            return new NoteDto
            {
                Id = note.Id,
                UserID = note.UserID,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            };
        }

        // Add a new note
        public async Task<NoteDto?> AddNoteAsync(NoteDto dto)
        {
            var user = await _db.Users.FindAsync(dto.UserID);
            if (user == null)
                throw new InvalidOperationException($"User with ID {dto.UserID} not found.");

            var entity = new Notes
            {
                UserID = dto.UserID,
                User = user,
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.Now
            };

            _db.Notes.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        // Update an existing note
        public async Task<NoteDto?> UpdateNoteAsync(NoteDto dto)
        {
            var existing = await _db.Notes
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == dto.Id && n.UserID == dto.UserID);

            if (existing == null) return null;

            existing.Title = dto.Title;
            existing.Content = dto.Content;
            existing.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            dto.UpdatedAt = existing.UpdatedAt;
            return dto;
        }

        // Delete a note
        public async Task<bool> DeleteNoteAsync(int id, int userId)
        {
            var note = await _db.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserID == userId);

            if (note == null) return false;

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
