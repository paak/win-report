using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("vwMAWB")]
    public class ViewMAWB
    {
        public long RowNum { get; set; }
        [Key]
        public int MawbId { get; set; }
        public string MawbStatus { get; set; }
        public string AirlinePrefix { get; set; }
        public string AwbNumber { get; set; }
        public string IATACode { get; set; }
        public string AirlineName { get; set; }
        public bool eAwb { get; set; }
        public string AgentName { get; set; }
        public string AgentCity { get; set; }
        public string AgentCountry { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int HawbCount { get; set; }
        public DateTime? FlightDate { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime MawbSentOn { get; set; }
    }

    [Table("HAWB")]
    public class HAWB
    {
        public int MawbId { get; set; }
        [Key]
        public int HawbId { get; set; }
        public string HawbNo { get; set; }
    }
}