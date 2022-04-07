using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Models
{
    public class EventInfos
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int recurrenceType { get; set; }
        public int groupId { get; set; }
        public int creatorId { get; set; }
        public List<UserInfos> participants { get; set; }
    }
}
