using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanUpdate { get; set; }
        public virtual List<UserGroups> UserGroups { get; set; }
    }
}
