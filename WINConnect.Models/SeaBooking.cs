using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("INTTRABKG")]
    public class SeaBooking
    {
        [Key]
        public int BookingId { get; set; }
        [Column("Carrier")]
        public int CarrierId { get; set; }
        [Column("BookingStatus")]
        public int BookingStatusId { get; set; }
        public int SourceId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string CarrierSCAC { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual Contact Created { get; set; }

        [ForeignKey("CarrierId")]
        public virtual AgentCarrier Carrier { get; set; }

        [ForeignKey("BookingStatusId")]
        public virtual SeaValues Status { get; set; }

        [ForeignKey("BookingId")]
        public virtual ICollection<SeaBooking_Location> Locations { get; set; }

        [ForeignKey("BookingId")]
        public virtual ICollection<SeaBooking_Container> Containers { get; set; }

        [ForeignKey("BookingId")]
        public virtual ICollection<SeaBooking_Reference> References { get; set; }
    }

    [Table("SeaValuesMaster")]
    public class SeaValues
    {
        [Column("TheId")]
        public int Id { get; set; }
        [Column("KeyValue")]
        public string Code { get; set; }
        [Column("KeyDescription")]
        public string Name { get; set; }
    }

    [Table("INTTRABKG_Location")]
    public class SeaBooking_Location
    {
        [Key]
        public long TheId { get; set; }
        public int BookingId { get; set; }
        [Column("LocationType")]
        public int TypeId { get; set; }
        [Column("LocationName")]
        public string Name { get; set; }

        [ForeignKey("TypeId")]
        public virtual SeaValues Type { get; set; }
    }

    [Table("INTTRABKG_Container")]
    public class SeaBooking_Container
    {
        [Key]
        public Int64 ContainerId { get; set; }
        public Int32 BookingId { get; set; }
        public Int64 Quantity { get; set; }
    }

    [Table("INTTRABKG_Reference")]
    public class SeaBooking_Reference
    {
        [Key]
        public long TheId { get; set; }

        public int BookingId { get; set; }
        [Column("ReferenceType")]
        public int TypeId { get; set; }
        [Column("ReferenceNo")]
        public string Number { get; set; }

        [ForeignKey("TypeId")]
        public virtual SeaValues Type { get; set; }
    }
}