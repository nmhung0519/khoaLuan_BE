﻿using SheduleManagement.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheduleManagement.Data
{
    public class UserService
    {
        private readonly ScheduleManagementDbContext _dbContext;
        public UserService(ScheduleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public (int, string) CheckLogin(string userName, string Password)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserName == userName && x.PassWord == Password);
            if (user == null)
            {
                return (400, "Username or password incorrect");
            }
            return (200, "Login succesfull");
        }
        public (string, Users) AddUser(Users users)
        {
            try
            {
                if (_dbContext.Users.Any(x=>x.UserName ==  users.UserName))
                {
                    return ("User name exist", null);
                }
                _dbContext.Add(users);
                _dbContext.SaveChanges();
                return (string.Empty, users);
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
        public (string, Users) UpdateUser(int Id, Users users)
        {
            try
            {
                var useUpdate = _dbContext.Users.Find(Id);
                useUpdate.FirstName = users.FirstName;
                useUpdate.LastName = users.LastName;
                useUpdate.Phone = users.Phone;
                useUpdate.Address = users.Address;
                useUpdate.Department = users.Department;
                useUpdate.Position = users.Position;              
                useUpdate.PassWord = users.PassWord;
                _dbContext.SaveChanges();
                return (string.Empty, users);
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
        public string DeleteUser(int Id)
        {
            Users userDel = null;
            try
            {
                userDel = _dbContext.Users.Find(Id);
                _dbContext.Users.Remove(userDel);
                _dbContext.SaveChanges();
                return String.Empty;
            }
            catch (Exception ex)
            {
                //return ex.Message;
                //Ghi log lỗi lại.
                return (userDel == null ? "Không tìm thấy thông tin người dùng tương ứng." : $"Xóa người dùng {userDel} không thành công.");
            }
        }
    }
}
