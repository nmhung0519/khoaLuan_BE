using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Models
{
    public class EventInfos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RecurrenceType { get; set; }
        public int GroupId { get; set; }
        public List<UserInfos> Participants { get; set; }
    }
}
