using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WINConnect.Models
{
    public class JobRecipient
    {
        /// <summary>
        /// TheID
        /// </summary>
        [Key]
        [Required]
        public int TheID { get; set; }

        /// <summary>
        /// JobMasterID
        /// </summary>
        [Required]
        public int JobMasterID { get; set; }

        /// <summary>
        /// WinID
        /// </summary>
        [Required]
        public int WinID { get; set; }

        /// <summary>
        /// CanEdit
        /// </summary>
        [Required]
        public bool CanEdit { get; set; }

        #region Master Tables/Parents

        /// <summary>
        /// Agent
        /// </summary>
        [ForeignKey("WinID")]
        public virtual Agent Agent { get; set; }

        #endregion
    }
}