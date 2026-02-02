using System.ComponentModel.DataAnnotations;

namespace ProductivIO.Backend.DTOs.Tasks
{
    public class UpdateTaskDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public string Priority { get; set; } = "Medium";

        public string Status { get; set; } = "Pending";
    }
}
