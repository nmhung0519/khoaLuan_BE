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
                _dbContext.EventUsers.RemoveRange(_dbContext.EventUsers.Where(x => !participants.Contains(x.UserId) && x.EventId == eventId));
                var existsParticipants = _dbContext.EventUsers.Where(x => participants.Contains(x.UserId) && x.EventId == eventId).Select(x => x.UserId).ToList();
                _dbContext.EventUsers.AddRange(participants.Where(x => !existsParticipants.Contains(x)).Select(x => new EventUser
                {
                    EventId = eventId,
                    UserId = x,
                    Status = (int)EventUserStatus.Invited,
                    LastUpdate = DateTime.Now
                }));
                _dbContext.EventUsers.Where(x => x.EventId == eventId && x.Status == (int)EventUserStatus.Declined).ToList().ForEach(x => { x.Status = (int)EventUserStatus.Invited; });
                var a = _dbContext.EventUsers.ToList();
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
                _dbContext.EventUsers.RemoveRange(_dbContext.EventUsers.Where(x => x.EventId == eventId));
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
                var eventUser = _dbContext.EventUsers.Where(x => x.UserId == userId && x.EventId == eventId).FirstOrDefault();
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
        public string ReplyInvitation(int userId, int eventId, bool isAccepted)
        {
            try
            {
                var eventUser = _dbContext.EventUsers.Where(x => x.UserId == userId && x.EventId == eventId && x.Status == 1).FirstOrDefault();
                if (eventUser == null) return "Không tìm thấy lời mời sự kiện tương ứng";

                eventUser.Status = isAccepted ? 2 : 3;
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
