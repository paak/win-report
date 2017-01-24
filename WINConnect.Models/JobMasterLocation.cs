using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("JobMaster_Location")]
    public class JobMasterLocation
    {
        /*
            [LocationID] [int] IDENTITY(1,1) NOT NULL,
            [JobMasterID] [int] NOT NULL,
            [LocationType] [int] NOT NULL,
            [LocationMasterID] [int] NOT NULL,
            [LocationName] [nvarchar](17) NOT NULL,
         */

        [Key]
        [Required]
        public int LocationID { get; set; }

        [Required]
        public int JobMasterID { get; set; }

        [Required]
        [Column("LocationType")]
        public int LocationTypeID { get; set; }

        public int? LocationMasterID { get; set; }

        [MaxLength(80)]
        public string LocationName { get; set; }
        
        #region MasterTables/Parents

        public virtual JobMaster JobMaster { get; set; }

        public virtual JobValues LocationType { get; set; }

        public virtual UNLocode UNLocode { get; set; }

        //public virtual AirportsMaster LocationMasterAirport { get; set; }

        #endregion 
    }
}
