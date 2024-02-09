using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactExample.Models
{
    public class Lecture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public int AssignmentId { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string VideoURL { get; set; }

        public Assignment Assignment { get; set; }
    }
}
