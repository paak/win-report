using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("Organisation_MessageLogger")]
    public class FWBLog
    {
        [Key]
        public int Id { get; set; }

        public string WIN_JobReferenceNumber { get; set; }
        [Column("Message_Type")]
        public int MessageTypeId { get; set; }
        public string Archive_Path { get; set; }
        public string Archive_Name { get; set; }
        public string Message_Format { get; set; }

        [Column("Uploaded_DateTime")]
        public DateTime SentOn { get; set; }

        public virtual MessageType MessageType { get; set; }
    }
}