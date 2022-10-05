using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_api.Model
{
    public class School
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Vision { get; set; }
        [Required]
        public string Mission { get; set; }
        public virtual Address Address { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Email { get; set; }
        public byte[] IconFile { get; set; }    
    }
}
