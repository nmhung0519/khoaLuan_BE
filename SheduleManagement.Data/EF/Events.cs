using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    public class Events
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public int UserCreate { get; set; }
        public int RecurrenceID { get; set; }
        [ForeignKey("RecurrenceID")]
        public virtual Recurrence Recurrence { get; set; }
    }
}
