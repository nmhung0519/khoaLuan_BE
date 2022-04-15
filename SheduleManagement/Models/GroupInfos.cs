using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Models
{
    public class GroupInfos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserInfos Creator { get; set; }
    }
}
