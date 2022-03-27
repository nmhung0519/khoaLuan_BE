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
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        [ForeignKey("GroupId")]
        public virtual Groups Groups { get; set; }
    }
}
