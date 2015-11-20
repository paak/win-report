using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    [Table("ListValuesMaster")]
    public class ListValues
    {
        [Column("TheId")]
        public int Id { get; set; }
        [Column("KeyIdentifier")]
        public string Identifier { get; set; }
        [Column("KeyValue")]
        public string Code { get; set; }
        [Column("KeyDescription")]
        public string Name { get; set; }
    }
}