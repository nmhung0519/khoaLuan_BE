using Microsoft.AspNetCore.Mvc;
using SheduleManagement.Data;
using SheduleManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public GroupController(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("GetForUser/{userId}")]
        public IActionResult GetForUser([FromQuery]int userId)
        {
            try
            {
                var userGroupService = new UserGroupService(_dbContext);
                var (msg, groups) = userGroupService.GetForUser(userId);
                if (msg.Length > 0) return BadRequest(msg);
                return Ok(groups.Select(x => x.Id).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
