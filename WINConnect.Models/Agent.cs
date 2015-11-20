using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    public class Agent
    {
        public int AgentId { get; set; }

        [Column("RegistrationType")]
        public int RegistrationTypeId { get; set; }

        public string AgentName { get; set; }
        public string AccountNumber { get; set; }
        public string IATACode { get; set; }

        public string Addrs1 { get; set; }
        public string CityCode { get; set; }
        public string CountryCode { get; set; }

        public bool IsActivated { get; set; }
        public bool IsEYProgram { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [ForeignKey("CountryCode")]
        public virtual Country Country { get; set; }

        [ForeignKey("RegistrationTypeId")]
        public virtual ListValues RegistrationType { get; set; }

        [ForeignKey("AgentId")]
        public virtual ICollection<AgentRole> Roles { get; set; }

        [ForeignKey("AgentId")]
        public virtual ICollection<Contact> Contacts { get; set; }
    }

    [Table("Agent_Role")]
    public class AgentRole
    {
        [Key, Column(Order = 0)]
        public int AgentId { get; set; }

        [Key, Column(Order = 1)]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }

    [Table("Agent_Carrier")]
    public class AgentCarrier
    {
        [Key]
        public int TheId { get; set; }
        public int AgentId { get; set; }
        public int CarrierId { get; set; }

        [ForeignKey("CarrierId")]
        public virtual SeaCarrier Carrier { get; set; }
    }

}
