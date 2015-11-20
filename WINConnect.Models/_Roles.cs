using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("RolesMaster")]
    public class Role
    {
        [Key]
        [Column("RoleId")]
        public int Id { get; set; }

        [Column("RoleName")]
        public string Name { get; set; }
    }
}