using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class CourseTopic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public string Topic { get; set; }

    }
}
