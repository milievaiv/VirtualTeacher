using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public string ?Message { get; set; }

    }
}
