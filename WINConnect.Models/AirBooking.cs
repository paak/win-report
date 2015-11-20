using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("vwAirbooking")]
    public class AirBooking
    {
        [Key]
        public int BookingId { get; set; }
        public string AirlinePrefix { get; set; }
        public string AWBNumber { get; set; }

        public string Status { get; set; }
        public string StatusName { get; set; }

        public bool SpecialRates { get; set; }
        public string IATACode { get; set; }
        public string AirlineName { get; set; }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }

        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public string Channel { get; set; }

        public DateTime SentOn { get; set; }

        [ForeignKey("BookingId")]
        public virtual ICollection<AirBooking_Routing> Routings { get; set; }
    }

    [Table("vwAirbookingRouting")]
    public class AirBooking_Routing
    {
        [Key]
        public int RoutingDetailsID { get; set; }
        public int BookingId { get; set; }
        public string FromAirportCode { get; set; }
        public string ToAirportCode { get; set; }
        public string CarrierCode { get; set; }
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }

        public string StatusCode { get; set; }
        public string StatusName { get; set; }

        //[NotMapped]
        //public virtual AirValues Status { get; set; }
    }
}