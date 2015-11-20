using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("StatusEvent")]
    public class SeaBookingStatusEvent
    {
        public int BookingId { get; set; }
        [Key]
        public int StatusEventId { get; set; }

        public string ServiceType { get; set; }
        public string MovementType { get; set; }

        public DateTime UpdatedOn { get; set; }

        [ForeignKey("StatusEventId")]
        public virtual ICollection<StatusEvent_Instruction> Comments { get; set; }

        [ForeignKey("StatusEventId")]
        public virtual ICollection<StatusEvent_Party> Parties { get; set; }

        [ForeignKey("StatusEventId")]
        public virtual ICollection<StatusEvent_Reference> References { get; set; }

        [ForeignKey("StatusEventId")]
        public virtual ICollection<StatusEvent_Equipment> Equipments { get; set; }
    }

    [Table("StatusEvent_Instruction")]
    public class StatusEvent_Instruction
    {
        [Key]
        [Column("InstructionId")]
        public int Id { get; set; }

        public int StatusEventId { get; set; }

        [Column("CommentType")]
        public string Key { get; set; }
        [Column("StatusEventComments")]
        public string Value { get; set; }
    }

    [Table("StatusEvent_Party")]
    public class StatusEvent_Party
    {
        [Key]
        public int PartyId { get; set; }

        public int StatusEventId { get; set; }

        [Column("PartnerRole")]
        public string Key { get; set; }
        [Column("PartnerIdentifier")]
        public string Value { get; set; }
    }

    [Table("StatusEvent_Reference")]
    public class StatusEvent_Reference
    {
        [Key]
        public int ReferenceId { get; set; }

        public int StatusEventId { get; set; }

        [Column("ReferenceType")]
        public string Key { get; set; }
        [Column("ReferenceInformation")]
        public string Value { get; set; }
    }

    [Table("StatusEvent_Equipment")]
    public class StatusEvent_Equipment
    {
        [Key]
        public int ContainerId { get; set; }

        public int StatusEventId { get; set; }

        public string EquipmentLoadType { get; set; }
        public string EquipmentTypeCode { get; set; }
        public string EquipmentIdentifier { get; set; }

        /* Child */
        [ForeignKey("ContainerID")]
        public virtual ICollection<StatusEvent_EquipmentEvent> Events { get; set; }
    }

    [Table("StatusEvent_EquipmentEvent")]
    public class StatusEvent_EquipmentEvent
    {
        [Key]
        public int EventID { get; set; }

        public int ContainerID { get; set; }

        public string EventCode { get; set; }
        public string EventDateType { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime ReceivedDateTime { get; set; }

        public string LocationType { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationCountry { get; set; }

        /* Child */
        [ForeignKey("EventID")]
        public virtual ICollection<StatusEvent_EquipmentLocation> Locations { get; set; }

        [ForeignKey("EventID")]
        public virtual ICollection<StatusEvent_EquipmentTransport> Transport { get; set; }
    }

    [Table("StatusEvent_EquipmentLocation")]
    public class StatusEvent_EquipmentLocation
    {
        [Key]
        public int LocationID { get; set; }

        public int EventID { get; set; }

        public string LocationType { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationCountry { get; set; }
        public string LocationCodeAgency { get; set; }

        public DateTime? DepartureActual { get; set; }
        public DateTime? DepartureEstimated { get; set; }
        public DateTime? ArrivalActual { get; set; }
        public DateTime? ArrivalEstimated { get; set; }
    }

    [Table("StatusEvent_EquipmentTransport")]
    public class StatusEvent_EquipmentTransport
    {
        [Key]
        public int TransportID { get; set; }

        public int EventID { get; set; }

        public string TransportStage { get; set; }
        public string TransportMode { get; set; }
        public string ConveyanceName { get; set; }
        public string VoyageTripNumber { get; set; }
        public string CarrierSCAC { get; set; }
        public string TransportIdentification { get; set; }
        public string TransportIdentificationType { get; set; }
    }
}