using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    [Table("UserGroups")]
    public class UserGroups 
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int GroupId { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        [ForeignKey("GroupId")]
        public virtual Groups Groups { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }
    }
}
