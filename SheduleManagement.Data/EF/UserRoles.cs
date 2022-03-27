using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    public class UserRoles
    {
        public int Id { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
