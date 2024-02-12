using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO.LectureDTO
{
    public class LectureDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string VideoURL { get; set; }
    }
}