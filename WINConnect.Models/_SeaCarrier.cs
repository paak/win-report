using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("OceanCarrierMaster")]
    public class SeaCarrier
    {
        [Key]
        [Column("OceanCarrierId")]
        public int Id { get; set; }

        [Column("OceanCarrierSCAC")]
        public string SCAC { get; set; }

        [Column("OceanCarrierName")]
        public string Name { get; set; }

        public bool IsActivated { get; set; }
    }
}
