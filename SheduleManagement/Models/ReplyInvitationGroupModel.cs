using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Models
{
    public class ReplyInvitationGroupModel
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool Accept { get; set; }
    }
}
