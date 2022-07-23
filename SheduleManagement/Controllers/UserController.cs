using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SheduleManagement.Data;
using SheduleManagement.Data.Services;
using SheduleManagement.Data.EF;
using SheduleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SheduleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public UserController(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("Search")]
        public IActionResult Search(string prefix, string exclusions)
        {
            try
            {
                var userService = new UserService(_dbContext);
                var (msg, users) = userService.Search(prefix, (exclusions ?? "").Split(",").Where(x => x.Length != 0).Select(x => Convert.ToInt32(x)).ToList());
                if (msg.Length > 0) return BadRequest(msg);
                return Ok(users.Select(x => new
                {
                    Id = x.Id,
                    Username = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone
                }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: api/<UserController>
        [HttpPost("Login")]
        public IActionResult LogIn(LoginInfos loginInfos)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest("Login infomation invalid");
            }
            UserService userService = new UserService(_dbContext);
            var (msg, userId) = userService.CheckLogin(loginInfos.Username, loginInfos.Password);
            if (msg.Length == 0) return Ok(userId);
            return BadRequest(msg);
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            UserService userService = new UserService(_dbContext);
            var (msg, userId) = userService.CheckLogin(model.UserName, model.OldPassword);
            if (msg.Length > 0) return BadRequest(msg);
            msg = userService.ChangePassword(userId, model.NewPassword);
            if (msg.Length == 0) return Ok(userId);
            else return BadRequest(msg);
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser(AddUserModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel(status: 400, "User input invalid"));
            }
            UserService userService = new UserService(_dbContext);
            var (message, user) = userService.AddUser(model.UserName, model.FirstName, model.LastName, model.Password, model.Phone, model.Address);
            if (user != null)
            {
                return Ok(new ResponseViewModel(status: 200, message, user.Id));
            }
            return BadRequest(new ResponseViewModel(status: 400, message));

        }
        [HttpPut()]
        public IActionResult Put([FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel(status: 400, "User input invalid"));
            }
            UserService userService = new UserService(_dbContext);
            var (message, user) = userService.UpdateUser(users);
            if (user != null)
            {
                return Ok(new ResponseViewModel(status: 200, message, user));
            }
            return BadRequest(new ResponseViewModel(status: 400, message));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserService userService = new UserService(_dbContext);
            string result = userService.DeleteUser(id);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var userService = new UserService(_dbContext);
                var (msg, users) = userService.GetAll();
                if (msg.Length > 0) return BadRequest(msg);
                return Ok(users.Select(x => new
                {
                    Id = x.Id,
                    Username = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone
                }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
