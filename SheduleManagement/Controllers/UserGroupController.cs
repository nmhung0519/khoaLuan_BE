using Microsoft.AspNetCore.Mvc;
using SheduleManagement.Data;
using SheduleManagement.Data.EF;
using SheduleManagement.Data.Services;
using SheduleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : Controller
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public UserGroupController(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet("GetForUser/{userId}")]
        public IActionResult GetForUser(int userId)
        {
            try
            {
                var userGroupService = new UserGroupService(_dbContext);
                var (msg, userGroups) = userGroupService.GetForUser(userId);
                if (msg.Length > 0) return BadRequest(msg);
                return Ok(userGroups.Select(x => new
                {
                    UserId = x.UserId,
                    GroupId = x.GroupId,
                    User = new
                    {
                        Id = x.UserId,
                        UserName = x.Users.UserName,
                        FirstName = x.Users.FirstName,
                        LastName = x.Users.LastName
                    },
                    Group = new
                    {
                        Id = x.GroupId,
                        Name = x.Groups.GroupName
                    },
                    Role = new
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name
                    },
                    IsAccepted = x.IsAccepted
                }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add/{groupId}")]
        public IActionResult Add(int groupId, [FromBody]List<KeyValuePair<int, int>> members)
        {
            try
            {
                var userGroupService = new UserGroupService(_dbContext);
                string msg = userGroupService.Add(groupId, members);
                if (msg.Length == 0) return Ok();
                return BadRequest(msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete/{gropuId}")]
        public IActionResult Delete([FromQuery]int groupId, [FromQuery]int userId)
        {
            try
            {
                var userGroupSerivce = new UserGroupService(_dbContext);
                string msg = userGroupSerivce.Delete(groupId, userId);
                if (msg.Length == 0) return Ok();
                return BadRequest(msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllMember/{groupId}")]
        public IActionResult GetAllMember(int groupId)
        {
            try
            {
                var userGroupService = new UserGroupService(_dbContext);
                var (msg, users) = userGroupService.GetAllMember(groupId);
                if (msg.Length == 0) return Ok(users.Select(x => new
                {
                    User = new
                    {
                        Id = x.UserId,
                        UserName = x.Users.UserName,
                        FirstName = x.Users.FirstName,
                        LastName = x.Users.LastName,
                        Role = x.RoleId,
                    },
                    Group = new
                    {
                        Id = x.GroupId,
                        Name = x.Groups.GroupName
                    },
                    Role = new
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name,
                        CanView = x.Role.CanView,
                        CanEdit = x.Role.CanEdit,
                        CanUpdate = x.Role.CanUpdate
                    },
                    IsAccepted = x.IsAccepted
                }));
                return BadRequest(msg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetInvitation/{userId}")]
        public IActionResult GetInvitation(int userId)
        {
            try
            {
                var userGroupService = new UserGroupService(_dbContext);
                var (msg, userGroups) = userGroupService.GetForUser(userId, false);
                if (msg.Length > 0) return BadRequest(msg);
                return Ok(userGroups.Select(x => new
                {
                    UserId = x.UserId,
                    GroupId = x.GroupId,
                    User = new
                    {
                        Id = x.UserId,
                        UserName = x.Users.UserName,
                        FirstName = x.Users.FirstName,
                        LastName = x.Users.LastName
                    },
                    Group = new
                    {
                        Id = x.GroupId,
                        Name = x.Groups.GroupName
                    },
                    Role = new
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name
                    },
                    IsAccepted = x.IsAccepted
                }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AcceptInvitation")]
        public IActionResult AcceptInvitation([FromBody]ReplyInvitationGroupModel model)
        {
            try
            {
                var userGroupService = new UserGroupService(_dbContext);
                string msg = userGroupService.AcceptInvitation(model.UserId, model.GroupId, model.Accept);
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
