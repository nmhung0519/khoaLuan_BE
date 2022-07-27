using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Models
{
    public class ReplyInvitationModel
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
