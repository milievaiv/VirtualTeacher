using System.ComponentModel.DataAnnotations.Schema;

namespace ReactExample.Models
{
    public class CourseTopic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public string Topic { get; set; }
        //public bool IsDeleted { get; set; } 
    }
}
