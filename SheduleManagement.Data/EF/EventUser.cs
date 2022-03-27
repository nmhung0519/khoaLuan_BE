using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    public class EventUser
    {
        public int Id { get; set; }
        public int EventID { get; set; }
        public int UserID { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        [StringLength(255)]
        public string ReasonForDecline { get; set; }
        [ForeignKey("EventID")]
        public virtual Events Events { get; set; }
        [ForeignKey("UserID")]
        public virtual Users Users { get; set; }

    }
}
