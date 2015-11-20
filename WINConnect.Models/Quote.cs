using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("Quotes")]
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        public int RfqId { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ValidUntilDate { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }

        [ForeignKey("StatusId")]
        public virtual ListValues Status { get; set; }
    }
}