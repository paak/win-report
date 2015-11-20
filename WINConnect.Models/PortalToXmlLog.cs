using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("PortalToXmlLog")]
    public class PortalToXmlLog
    {
        [Key]
        public int PortalToXmlLogId { get; set; }
        public int ReferenceId { get; set; }
        public int MessageTypeId { get; set; }
        public int OrganizationId { get; set; }
        public byte SentStatus { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual MessageType MessageType { get; set; }

        public virtual Organization Organization { get; set; }
    }

    [Table("MessageTypeMaster")]
    public class MessageType
    {
        [Key]
        [Column("MessageTypeId")]
        public int Id { get; set; }
        [Column("MessageTypeName")]
        public string Name { get; set; }
    }

    [Table("OrganizationMaster")]
    public class Organization
    {
        [Key]
        [Column("OrganizationId")]
        public int Id { get; set; }
        public string Name { get; set; }
    }


}