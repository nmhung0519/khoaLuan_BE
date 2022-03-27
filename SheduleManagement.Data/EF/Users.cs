using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SheduleManagement.Data.EF
{
    [Table("Users")]
    public class Users
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string Phone { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string Position { get; set; }
        [StringLength(255)]
        public string Department { get; set; }
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string PassWord { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
}
