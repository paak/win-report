using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("Awb_Status")]
    public class FSU
    {
        [Key]
        public int StatusId { get; set; }

        public string AirlinePrefix { get; set; }
        public string AwbNumber { get; set; }

        [ForeignKey("StatusId")]
        public virtual ICollection<FSU_Detail> Details { get; set; }

        [ForeignKey("StatusId")]
        public virtual ICollection<AwbAcknowledge> Acknowledges { get; set; }
    }

    [Table("Awb_StatusDetail")]
    public class FSU_Detail
    {
        public int StatusId { get; set; }

        [Key]
        public int StatusDetailId { get; set; }

        public string StatusCode { get; set; }
        public string CarrierCode { get; set; }
        public string FlightNumber { get; set; }
        public Nullable<DateTime> FlightDateTime { get; set; }

        public string PortOfArrival { get; set; }
        public string PortOfDeparture { get; set; }
        public string FileContent { get; set; }

        [Column("NumberOfPieces")]
        public int? Pcs { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUOM { get; set; }
        public string OSI { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime MessageOn { get; set; }

        [ForeignKey("StatusId")]
        public virtual FSU FSU { get; set; }
    }

    [Table("Awb_Acknowledge")]
    public class AwbAcknowledge
    {
        [Key]
        public int AckID { get; set; }
        [Column("AwbID")]
        public int StatusId { get; set; }
        public string AckType { get; set; }
        public string AckMessage { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime MessageOn { get; set; }
    }
}