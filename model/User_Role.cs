using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.model
{
    public class User_Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string UserRole { get; set; }
        [Required]
        [ForeignKey("User")]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        
    }
}
