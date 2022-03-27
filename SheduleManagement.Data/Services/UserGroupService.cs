using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SheduleManagement.Data.EF;

namespace SheduleManagement.Data.Services
{
    public class GroupService
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public GroupService(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (string, List<Groups>) GetForUser(int userId)
        {
            try
            {
                return (String.Empty, _dbContext.UserGroups
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Groups)
                    .ToList());
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
    }
}
