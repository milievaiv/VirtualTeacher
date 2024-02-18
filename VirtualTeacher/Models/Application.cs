using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class Application
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public string VerifKey { get; set; }
        public string Email { get; set; }

        public DateTime Date { get; set; } 
    }
}
