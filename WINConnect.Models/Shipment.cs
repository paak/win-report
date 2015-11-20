using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("Shipments")]
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public string Title { get; set; }

        [Column("Status")]
        public int StatusId { get; set; }

        public int CreatedBy { get; set; }

        [Column("TransportMode")]
        public int TransportModeId { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [ForeignKey("TransportModeId")]
        public virtual ListValues TransportMode { get; set; }

        [ForeignKey("StatusId")]
        public virtual ListValues Status { get; set; }

        [ForeignKey("ShipmentId")]
        public virtual ICollection<Shipment_Location> Locations { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual Contact Exporter { get; set; }

        [ForeignKey("ShipmentId")]
        public virtual ICollection<RFQ> RFQs { get; set; }
    }

    [Table("Shipments_Locations")]
    public class Shipment_Location
    {
        [Key]
        public int LocationId { get; set; }
        public int ShipmentId { get; set; }
        public int LocationTypeId { get; set; }
        public int LocationCodeId { get; set; }
        public string LocationName { get; set; }

        [ForeignKey("LocationTypeId")]
        public virtual ListValues LocationType { get; set; }
    }

}