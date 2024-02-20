using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class AssignmentContent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }        
        public ContentType Type { get; set; }
        public string Content { get; set; }

    }
}
