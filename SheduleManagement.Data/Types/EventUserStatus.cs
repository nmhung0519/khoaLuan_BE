using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SheduleManagement.Data.Types
{
    enum EventUserStatus
    {
        [Description("Đã gửi lời mời")]
        Invited = 1,
        [Description("Đã đồng ý")]
        Accepted = 2,
        [Description("Đã từ chối")]
        Declined = 3
    }
}
