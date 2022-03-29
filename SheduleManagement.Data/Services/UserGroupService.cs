using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SheduleManagement.Data.EF;

namespace SheduleManagement.Data.Services
{
    public class UserGroupService
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public UserGroupService(ScheduleManagementDbContext dbContext)
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
        public string Add(int groupId, List<KeyValuePair<int, int>> members)
        {
            try
            {
                if (_dbContext.Groups.Find(groupId) == null)
                    return "Không tồn tại nhóm tương ứng.";
                if (_dbContext.UserGroups
                    .Join(members,
                        x => new { UserId = x.UserId, GroupId = x.GroupId },
                        y => new { UserId = y.Key, GroupId = groupId },
                        (x, y) => 1)
                    .Count() > 0)
                    return "Đã tồn tại thành viên trong nhóm.";
                _dbContext.UserGroups.AddRange(members.Select(x => new UserGroups
                {
                    UserId = x.Key,
                    GroupId = groupId,
                    RoleId = x.Value
                }));
                _dbContext.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Delete(int groupId, int userId)
        {
            try
            {
                var userGroup = _dbContext.UserGroups.Where(x => x.GroupId == groupId && x.UserId == userId).FirstOrDefault();
                if (userGroup == null) return "Không tồn tại thành viên trong nhóm.";
                _dbContext.UserGroups.Remove(userGroup);
                _dbContext.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public (string, List<UserGroups>) GetAllMember(int groupId)
        {
            try
            {
                var group = _dbContext.Groups.Find(groupId);
                if (group == null) return ("Không tồn tại nhóm tương ứng.", null);
                var userGroups = _dbContext.UserGroups.Where(x => x.GroupId == groupId).ToList();
                foreach (var userGroup in userGroups)
                    _dbContext.Entry(userGroup)
                        .Reference(x => x.Users)
                        .Load();
                return (String.Empty, userGroups);
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
    }
}
