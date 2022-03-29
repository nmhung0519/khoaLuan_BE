using SheduleManagement.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace SheduleManagement.Data.Services
{
    public class GroupService
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public GroupService(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public (string, int) Add(string name)
        {
            try
            {
                var group = new Groups
                {
                    GroupName = name,
                    CreatorId = 1
                };
                _dbContext.Groups.Add(group);
                _dbContext.SaveChanges();
                return (String.Empty, group.Id);
            }
            catch (Exception ex)
            {
                return (ex.Message, 0);
            }
        }
    }
}
