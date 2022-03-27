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
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
}
