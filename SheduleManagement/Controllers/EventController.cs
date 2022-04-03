using Microsoft.AspNetCore.Mvc;
using SheduleManagement.Data;
using SheduleManagement.Data.Services;
using SheduleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Controllers
{
    [Route("api/Event")]
    public class EventController : Controller
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public EventController(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetByMonth")]
        public IActionResult GetByMonth(int userId, int groupId, int month, int year)
        {
            try
            {
                var eventService = new EventService(_dbContext);
                var (msg, events) = eventService.GetByMonth(userId, groupId, month, year);
                if (msg.Length > 0) return BadRequest(msg);
                return Ok(events.Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    RecurrenceID = x.RecurrenceID,
                    GropuId = x.GroupId
                }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("InsertOrUpdate")]
        public IActionResult InsertOrUpdate(EventInfos model)
        {
            try
            {
                var eventService = new EventService(_dbContext);
                var (msg, eventId) = eventService.Update(model.Id, model.Title, model.Description, model.StartTime, model.EndTime, model.RecurrenceType, model.Participants.Select(x => x.Id).ToList());
                if (msg.Length > 0) return BadRequest(msg);
                return Ok(eventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{eventId}")]
        public IActionResult Delete(int eventId)
        {
            try
            {
                var eventService = new EventService(_dbContext);
                var msg = eventService.Delete(eventId);
                if (msg.Length > 0) return BadRequest(msg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteInvitaion/{eventId}")]
        public IActionResult DeleteInvitation(int eventId, int userId)
        {
            try
            {
                var eventUserService = new EventUserService(_dbContext);
                string msg = eventUserService.Delete(eventId, userId);
                if (msg.Length > 0) return BadRequest(msg);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
