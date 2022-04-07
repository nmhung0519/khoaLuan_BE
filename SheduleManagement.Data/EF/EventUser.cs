using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    [Table("EventUsers")]
    public class EventUser
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public string ReasonForDecline { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdate { get; set; }
        public virtual Events Events { get; set; }
        public virtual Users Users { get; set; }
    }
}
