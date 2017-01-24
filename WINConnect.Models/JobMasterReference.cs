using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("JobMaster_Reference")]
    public class JobMasterReference 
    {
        /*
            [ReferenceID] [int] IDENTITY(1,1) NOT NULL,
            [JobMasterID] [int] NOT NULL,
            [ReferenceType] [int] NOT NULL,
            [ReferenceNumber] [varchar](30) NOT NULL,
            [Description] [nvarchar](50) NULL,
            [FromDate] [datetime] NULL,
            [ToDate] [datetime] NULL,
         */

        [Key]
        [Required]
        public int ReferenceID { get; set; }

        [Required]
        public int JobMasterID { get; set; }

        [Required]
        [Column("ReferenceType")]
        public int ReferenceTypeID { get; set; }

        /// <summary>
        /// [20150130] Task 5851:
        /// JOB ORDER_Maximum size allowed for Type, Number and Description field under References object is not as per doc.
        /// </summary>
        [Required]
        [MaxLength(35)]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// [20150130] Task 5851:
        /// JOB ORDER_Maximum size allowed for Type, Number and Description field under References object is not as per doc.
        /// </summary>
        [MaxLength(100)]
        public string Description { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        #region Master Tables/Parents

        public virtual JobMaster JobMaster { get; set; }

        public virtual JobValues ReferenceType { get; set; }

        #endregion
    }
}
