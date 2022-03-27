using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SheduleManagement.Data;
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
        // GET: api/<UserController>
        [HttpPost("Login")]
        public IActionResult LogIn(LoginInfos loginInfos)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel(status:400,"Login infomation invalid"));
            }
            UserService userService = new UserService(_dbContext);
            var (status, message) = userService.CheckLogin(loginInfos.Username, loginInfos.Password);
            if (status == 200)
            {
                return Ok(new ResponseViewModel(status: status, message));
            }
            return BadRequest(new ResponseViewModel(status: status, message));
            
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser(Users users)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel(status: 400, "User input invalid"));
            }
            UserService userService = new UserService(_dbContext);
            var (message, user) = userService.AddUser(users);
            if (user != null)
            {
                return Ok(new ResponseViewModel(status: 200, message, user));
            }
            return BadRequest(new ResponseViewModel(status: 400, message));

        }
        // GET api/<UserController>/5
       
      
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel(status: 400, "User input invalid"));
            }
            UserService userService = new UserService(_dbContext);
            var (message, user) = userService.UpdateUser(id,users);
            if (user != null)
            {
                return Ok(new ResponseViewModel(status: 200, message, user));
            }
            return BadRequest(new ResponseViewModel(status: 400, message));
        }
            
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserService userService = new UserService(_dbContext);
            string result = userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
