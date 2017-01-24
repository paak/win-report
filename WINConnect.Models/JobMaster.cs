using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    public class JobMaster
    {
        /*
            [JobMasterID] [int] IDENTITY(1,1) NOT NULL,
            [TransportMode] [int] NOT NULL,
            [ShipmentType] [int] NULL,
            [JobStatus] [int] NOT NULL,
            [MovementType] [int] NOT NULL,
            [IncoTerms] [int] NULL,
            [IncoTermsPlace] [nvarchar](30) NULL,
            [Remarks] [nvarchar](150) NULL,
            [CreatedBy] [int] NOT NULL,
            [CreatedOn] [datetime] NOT NULL,
            [UpdatedBy] [int] NOT NULL,
            [UpdatedOn] [datetime] NOT NULL,
         */

        [Key]
        [Required]
        public int JobMasterID { get; set; }

        [Required]
        [Column("TransportMode")]
        public int TransportModeID { get; set; }

        [Column("ShipmentType")]
        public int? ShipmentTypeID { get; set; }

        [Required]
        [Column("JobStatus")]
        public int JobStatusID { get; set; }

        [Required]
        [Column("MovementType")]
        public int MovementTypeID { get; set; }

        public int? IncoTerms { get; set; }

        [MaxLength(80)]
        public string IncoTermsPlace { get; set; }

        [MaxLength(1000)]
        public string Remarks { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        [MaxLength(30)]
        public string JobMasterNo { get; set; }

        [Required]
        [MaxLength(100)]
        public string UniqueReferenceID { get; set; }

        [Required]
        public bool IsActivated { get; set; }

        [Required]
        public int WinID { get; set; }

        public DateTime? JobSharedDate { get; set; }

        #region Master Tables/Parents

        public virtual JobValues TransportMode { get; set; }

        public virtual JobValues ShipmentType { get; set; }

        public virtual JobValues JobStatus { get; set; }

        public virtual JobValues MovementType { get; set; }

        public virtual ICollection<JobMasterLocation> JobMasterLocations { get; set; }

        [ForeignKey("WinID")]
        public virtual Agent Sender { get; set; }

        public virtual ICollection<JobRecipient> JobRecipients { get; set; }

        public virtual ICollection<JobMasterReference> JobMasterReferences { get; set; }

        public virtual ICollection<JobStatusEvent> JobStatusEvents { get; set; }

        #endregion

        /*
        #region SubTables/Children
        public virtual ICollection<JobMasterAddress> JobMasterAddresses { get; set; }

        public virtual ICollection<JobMasterCharge> JobMasterCharges { get; set; }

        public virtual ICollection<JobMasterContainer> JobMasterContainers { get; set; }

        public virtual ICollection<JobMasterFlight> JobMasterFlights { get; set; }

        public virtual ICollection<JobMasterGoods> JobMasterGoodses { get; set; }

        public virtual ICollection<JobMasterNotify> JobMasterNotifies { get; set; }
        
        public virtual ICollection<JobMasterParty> JobMasterParties { get; set; }
        
        public virtual ICollection<JobMasterVoyage> JobMasterVoyages { get; set; }

        public virtual ICollection<JobHouse> JobHouses { get; set; }

        public virtual ICollection<EDockett> EDocketts { get; set; }

        #endregion
        */
    }
}