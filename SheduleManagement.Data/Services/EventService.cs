using Microsoft.EntityFrameworkCore.Storage;
using SheduleManagement.Data.EF;
using SheduleManagement.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheduleManagement.Data.Services
{
    public class EventService
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public EventService(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public (string, List<Events>) GetByMonth(int userId, int groupId, int month, int year)
        {
            try
            {
                if (month <= 0 || month > 12 || year < 1900 || year > 9999)
                    return ("Thời gian không hợp lệ", null);
                var events = _dbContext.EventUsers
                    .Where(x => (groupId == 0 || x.Events.GroupId == groupId) 
                        && (userId == 0 || x.UserID == userId) 
                        && x.Events.StartTime.Date < new DateTime(year, month, 1).AddMonths(1) 
                        && x.Events.EndTime.Date >= new DateTime(year, month, 1))
                    .Select(x => x.Events)
                    .ToList();
                return (String.Empty, events);
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
        public (string, int) Update(int eventId, string title, string description, DateTime startTime, DateTime endTime, int recurrenceType, List<int> participants)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    Events ev = eventId == 0 ? new Events() : _dbContext.Events.Find(eventId);
                    if (ev == null) return ("Không tìm thấy sự kiện tương ứng", 0);
                    ev.Title = title;
                    ev.Description = description;
                    ev.StartTime = startTime;
                    ev.EndTime = endTime;
                    ev.RecurrenceID = recurrenceType;
                    if (eventId == 0) _dbContext.Events.Add(ev);
                    _dbContext.SaveChanges();
                    var eventUserService = new EventUserService(_dbContext);
                    string msg = eventUserService.UpdateForEvent(eventId, participants);
                    if (msg.Length > 0) return (msg, 0);
                    transaction.Commit();
                    return (String.Empty, ev.Id);
                }

            }
            catch (Exception ex)
            {
                return (ex.Message, 0);
            }
        }
    }
}
