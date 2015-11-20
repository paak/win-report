using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("AgentContact")]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public int AgentId { get; set; }

        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }

        [StringLength(32)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        [Compare("Username")]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        [Compare("Email")]
        public string Username { get; set; }

        [Display(Name = "Active")]
        public bool IsActivated { get; set; }

        [Required]
        [StringLength(35)]
        public string ContactType { get; set; }

        public Nullable<int> OperatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual Agent Agent { get; set; }

        [ForeignKey("ContactId")]
        public virtual Contact_Membership Memberships { get; set; }

        [ForeignKey("ContactId")]
        public virtual ICollection<Contact_Login> Logins { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual ICollection<Quote> Quotes { get; set; }

        [ForeignKey("ContactId")]
        public virtual ICollection<Contact_Role> Roles { get; set; }
    }

    [Table("AgentContact_Membership")]
    public class Contact_Membership
    {
        [Key]
        public int ContactId { get; set; }
        public bool IsConfirmed { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<DateTime> PasswordChangedDate { get; set; }
    }

    [Table("AgentContact_Login")]
    public class Contact_Login
    {
        [Key, Column(Order = 0)]
        public int ContactId { get; set; }

        [Key, Column(Order = 1)]
        public DateTime LoggedOn { get; set; }
    }

    [Table("AgentContact_Roles")]
    public class Contact_Role
    {
        [Key, Column(Order = 0)]
        public int ContactId { get; set; }

        [Key, Column(Order = 1)]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}