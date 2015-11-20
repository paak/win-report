using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("_UNLocodes")]
    public class UNLocode
    {
        [Key]
        public string Code { get; set; }

        [Column("NameWoDiacritics")]
        public string Name { get; set; }

        public string CountryCode { get; set; }
    }
}