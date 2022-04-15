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
        public (string, int) Add(int userId, string name)
        {
            try
            {
                using (var transac = _dbContext.Database.BeginTransaction())
                {
                    var group = new Groups
                    {
                        GroupName = name,
                        CreatorId = userId
                    };
                    _dbContext.Groups.Add(group);
                    _dbContext.SaveChanges();
                    var userGroupService = new UserGroupService(_dbContext);
                    string msg = userGroupService.Add(group.Id, new List<KeyValuePair<int, int>> { new KeyValuePair<int, int>(userId, 1) });
                    if (msg.Length > 0) return (msg, 0);
                    transac.Commit();

                    return (String.Empty, group.Id);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, 0);
            }
        }
        public string Delete(int groupId)
        {
            try
            {
                var group = _dbContext.Groups.Find(groupId);
                if (group == null)
                    return "Không tìm thấy nhóm tương ứng";
                _dbContext.Groups.Remove(group);
                _dbContext.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
