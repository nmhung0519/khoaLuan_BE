using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    [Table("Groups")]
    public class Groups
    {
        public int  Id { get; set; }
        [Required]
        [StringLength(255)]
        public string GroupName { get; set; }
        [Required]
        [StringLength(255)]
        public string UserCreated { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
}
