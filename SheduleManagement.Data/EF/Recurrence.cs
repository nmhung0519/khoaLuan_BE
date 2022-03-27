using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SheduleManagement.Data.EF
{
    public class Recurrence
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartLoop { get; set; }
        [Required]
        public DateTime EndLoop { get; set; }
        [StringLength(255)]
        public string TypeRecurring { get; set; }
        public int Frequency { get; set; }
        public DateTime WeekDay { get; set; }
    }
}
