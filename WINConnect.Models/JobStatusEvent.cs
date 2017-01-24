using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    /// <summary>
    /// JobStatusEvent
    /// </summary>
    public class JobStatusEvent
    {
        /// <summary>
        /// StatusEventID
        /// </summary>
        [Key]
        public int StatusEventID { get; set; }

        /// <summary>
        /// JobMasterID
        /// </summary>
        [Required]
        public int JobMasterID { get; set; }

        /// <summary>
        /// StatusID
        /// </summary>
        [Required]
        public int StatusID { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// LocationID
        /// </summary>
        /// Task 9045:City Object: Store City Name in database and Validation.
        public int? LocationID { get; set; }

        /// <summary>
        /// EventDateTime
        /// </summary>
        [Required]
        public System.DateTime EventDateTime { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        [MaxLength(150)]
        public string Comment { get; set; }

        /// <summary>
        /// JobHouseNo
        /// </summary>
        [MaxLength(35)]
        public string JobHouseNo { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(35)]
        public string Name { get; set; }

        /// <summary>
        /// CreatedBy
        /// </summary>
        [Required]
        public int CreatedBy { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// UpdatedBy
        /// </summary>
        [Required]
        public int UpdatedBy { get; set; }

        /// <summary>
        /// UpdatedOn
        /// </summary>
        [Required]
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// To store AgentID.
        /// CurrentUser.AgentID in case of DirectAgent POST by itself
        /// RequestedAgentID in case of SoftwareCompany POST on behalf of Agent
        /// </summary>
        public int ForAgentID { get; set; }

        /// <summary>
        /// LocationName
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// SourceID
        ///     Type: Airline, OceanLiner, ExportAgent, ImportAgent, OtherParty
        /// Task 9854:UCT and JOB integration for Track and Trace.
        /// </summary>
        public int? SourceID { get; set; }

        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// StatusEventMaster
        /// </summary>
        [ForeignKey("StatusID")]
        public virtual JobValues StatusEvent { get; set; }


        /*
                /// <summary>
                /// JobStatusEventFlights
                /// </summary>
                public virtual ICollection<JobStatusEventFlight> JobStatusEventFlight { get; set; }

                /// <summary>
                /// JobStatusEventGoodsDetails
                /// </summary>
                public virtual ICollection<JobStatusEventGoodsDetail> JobStatusEventGoodsDetail { get; set; }

                /// <summary>
                /// JobStatusEventLocalTransports
                /// </summary>
                public virtual JobStatusEventLocalTransport JobStatusEventLocalTransport { get; set; }

                /// <summary>
                /// JobStatusEventVoyages
                /// </summary>
                public virtual JobStatusEventVoyage JobStatusEventVoyage { get; set; }

                /// <summary>
                /// EDocketts
                /// </summary>
                public virtual ICollection<EDockett> EDocketts { get; set; }

                /// <summary>
                /// JobStatusEventContainers
                /// </summary>
                public virtual ICollection<JobStatusEventContainer> JobStatusEventContainers { get; set; }

                /// <summary>
                /// CreatorAgentContact
                /// </summary>
                public virtual AgentContact CreatorAgentContact { get; set; }

                /// <summary>
                /// UpdatorAgentContact
                /// </summary>
                public virtual AgentContact UpdatorAgentContact { get; set; }

                /// <summary>
                /// JobMaster
                /// </summary>
                public virtual JobMaster JobMaster { get; set; }

                /// <summary>
                /// ForAgent
                /// </summary>
                public virtual Agent ForAgent { get; set; }

                /// <summary>
                /// SourceMaster
                /// </summary>
                public virtual JobValuesMaster SourceMaster { get; set; }

                /// <summary>
                /// LocationMaster
                /// </summary>
                public virtual UNLocode LocationMaster { get; set; }
                */
    }
}
