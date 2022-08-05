using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.model
{
    public class Objects
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName="varchar(100)")]
        public string ObjectName { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ObjectType { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ObjectDeposit { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ObjectAisle { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ObjectShelf { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ObjectFloor { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ObjectStock { get; set; }



    }
}
