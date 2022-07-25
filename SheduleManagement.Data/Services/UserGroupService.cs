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

        public (string, List<UserGroups>) GetForUser(int userId, bool? isAccepted = null)
        {
            try
            {
                var userGroups = _dbContext.UserGroups
                    .Where(x => x.UserId == userId && (isAccepted == null || x.IsAccepted == isAccepted))
                    .ToList();
                foreach (var userGroup in userGroups)
                {
                    _dbContext.Entry(userGroup)
                        .Reference(x => x.Users)
                        .Load();
                    _dbContext.Entry(userGroup)
                        .Reference(x => x.Groups)
                        .Load();
                    _dbContext.Entry(userGroup)
                        .Reference(x => x.Role)
                        .Load();
                }
                userGroups = userGroups.Where(x => x.Users != null && x.Groups != null && x.Role != null).ToList();
                return (String.Empty, userGroups);
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
                foreach (var item in members)
                {
                    if (_dbContext.UserGroups.Where(x => x.UserId == item.Key && x.GroupId == groupId).Count() > 0)
                        return "Đã tồn tại thành viên trong nhóm.";
                }
                _dbContext.UserGroups.AddRange(members.Select(x => new UserGroups
                {
                    UserId = x.Key,
                    GroupId = groupId,
                    RoleId = x.Value,
                    IsAccepted = false
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
                {
                    _dbContext.Entry(userGroup)
                        .Reference(x => x.Users)
                        .Load();
                    _dbContext.Entry(userGroup)
                        .Reference(x => x.Groups)
                        .Load();
                    _dbContext.Entry(userGroup)
                        .Reference(x => x.Role)
                        .Load();
                }
                return (String.Empty, userGroups);
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
        public string AcceptInvitation(int userId, int groupId, bool accept)
        {
            try
            {
                var userGroup = _dbContext.UserGroups.FirstOrDefault(x => x.UserId == userId && x.GroupId == groupId && x.IsAccepted == false);
                if (userGroup == null) return "Không tìm thấy lời mời tham gia nhóm tương ứng.";
                if (accept) userGroup.IsAccepted = true;
                else _dbContext.UserGroups.Remove(userGroup);
                _dbContext.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public (string, List<ObjectResultSearch>) Search(string prefix, List<int> exclusions)
        {
            prefix = prefix.ToLower();
            try
            {
                var a = _dbContext.Users
                    .Where(x => (!exclusions.Contains(x.Id)) && (x.UserName.ToLower() == prefix
                    || x.UserName.ToLower().Contains(prefix)
                    || x.Phone == prefix
                    || x.Phone.Contains(prefix)
                    || (x.LastName.ToLower() + " " + x.FirstName.ToLower()) == prefix
                    || (x.LastName.ToLower() + " " + x.FirstName.ToLower()).Contains(prefix)))
                    .Select(x => new ObjectResultSearch
                    {
                        shortname = x.UserName,
                        fullname = (x.LastName + " " + x.FirstName),
                        type = 1,
                        users = new List<ObjectUser> { new ObjectUser
                        {
                            id = x.Id,
                            username = x.UserName,
                            fullname = (x.LastName + " " + x.FirstName)
                        } }
                    })
                    .OrderBy(x => x.shortname.ToLower() == prefix)
                    .ThenBy(x => x.fullname == prefix)
                    .ThenBy(x => x.shortname)
                    .Take(3)
                    .ToList();
                var b = _dbContext.Groups.Where(x => x.GroupName.ToLower().Contains(prefix)).Select(x => x.Id).ToList();
                var b2 = _dbContext.UserGroups.Where(x => b.Contains(x.GroupId)).ToList();
                foreach (var item in b2)
                {
                    _dbContext.Entry(item)
                        .Reference(x => x.Users)
                        .Load();
                    _dbContext.Entry(item)
                        .Reference(x => x.Groups)
                        .Load();
                }
                var b3 = b2.GroupBy(x => x.GroupId).ToList()
                        .Select(x => new ObjectResultSearch
                        {
                            shortname = x.First().Groups.GroupName,
                            fullname = x.First().Groups.GroupName,
                            type = 2,
                            users = x.Select(y => new ObjectUser
                            {
                                id = y.Users.Id,
                                username = y.Users.UserName,
                                fullname = (y.Users.LastName + " " + y.Users.FirstName)
                            }).ToList()
                        })
                        .OrderBy(x => x.shortname.ToLower() == prefix)
                    .ThenBy(x => x.fullname == prefix)
                    .ThenBy(x => x.shortname)
                    .Take(3)
                    .ToList();
                var c = a.Union(b3).OrderBy(x => x.shortname.ToLower() == prefix)
                    .ThenBy(x => x.fullname == prefix)
                    .ThenBy(x => x.shortname)
                    .Take(3)
                    .ToList();
                return (String.Empty, c);
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
    }
    public class ObjectResultSearch
    {
        public string shortname;
        public string fullname;
        public int type;
        public List<ObjectUser> users;
    }
    public class ObjectUser
    {
        public int id;
        public string username;
        public string fullname;
    }
}
