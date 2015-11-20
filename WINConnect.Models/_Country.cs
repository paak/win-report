using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("CountryMaster")]
    public class Country
    {
        [Key]
        [Column("CountryCode")]
        public string Code { get; set; }
        
        [Column("CountryName")]
        public string Name { get; set; }
    }
}