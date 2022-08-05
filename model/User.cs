using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string UserPassword { get; set; }
        [Required]
        public List<User_Role> User_Roles { get; set; }
    }
}
