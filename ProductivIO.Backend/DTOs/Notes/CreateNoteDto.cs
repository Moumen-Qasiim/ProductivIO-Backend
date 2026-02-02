using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Notes
{
    public class CreateNoteDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
