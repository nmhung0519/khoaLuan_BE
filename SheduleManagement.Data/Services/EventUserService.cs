using SheduleManagement.Data.EF;
using SheduleManagement.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheduleManagement.Data.Services
{
    public class EventUserService
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public EventUserService(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string UpdateForEvent(int eventId, List<int> participants)
        {
            try
            {
                _dbContext.EventUsers.RemoveRange(_dbContext.EventUsers.Where(x => !participants.Contains(x.UserID)));
                var existsParticipants = _dbContext.EventUsers.Where(x => !participants.Contains(x.UserID)).Select(x => x.UserID).ToList();
                _dbContext.EventUsers.AddRange(participants.Where(x => !existsParticipants.Contains(x)).Select(x => new EventUser
                {
                    EventID = eventId,
                    UserID = x,
                    Status = (int)EventUserStatus.Invited
                }));
                _dbContext.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteForEvent(int eventId)
        {
            try
            {
                _dbContext.EventUsers.RemoveRange(_dbContext.EventUsers.Where(x => x.EventID == eventId));
                _dbContext.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Delete(int eventId, int userId)
        {
            try
            {
                var eventUser = _dbContext.EventUsers.Where(x => x.UserID == userId && x.EventID == eventId).FirstOrDefault();
                if (eventUser == null) return "Không tìm tháy lời mời tương ứng.";
                _dbContext.EventUsers.Remove(eventUser);
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
