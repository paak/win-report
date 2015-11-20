using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    public class RFQ
    {
        [Key]
        public int RfqId { get; set; }
        public int ShipmentId { get; set; }
        public int RecipientId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int StatusId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Comments { get; set; }
        public DateTime RequiredDate { get; set; }
        public bool IsRead { get; set; }
        public int UpdatedBy { get; set; }

        [ForeignKey("RecipientId")]
        public virtual Agent Recipient { get; set; }

        [ForeignKey("StatusId")]
        public virtual ListValues Status { get; set; }

        [ForeignKey("RfqId")]
        public virtual Quote Quote { get; set; }
    }
}