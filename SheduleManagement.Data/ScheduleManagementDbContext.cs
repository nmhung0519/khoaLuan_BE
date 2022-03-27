using Microsoft.EntityFrameworkCore;
using SheduleManagement.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace SheduleManagement.Data
{
    public class ScheduleManagementDbContext : DbContext
    {
        public ScheduleManagementDbContext(DbContextOptions<ScheduleManagementDbContext> options)
        : base(options)
        {
        }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
