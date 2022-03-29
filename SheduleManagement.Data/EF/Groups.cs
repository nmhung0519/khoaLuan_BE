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
        [Key]
        public int  Id { get; set; }
        [Required]
        [StringLength(255)]
        public string GroupName { get; set; }
        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual Users Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
