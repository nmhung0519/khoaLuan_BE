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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventUser>().HasKey(x => new { x.EventId, x.UserId });
            modelBuilder.Entity<EventUser>().Property(x => x.CreatedTime).ValueGeneratedOnAdd();
            modelBuilder.Entity<EventUser>().HasOne(x => x.Events).WithMany(x => x.EventUsers).HasForeignKey(x => x.EventId);
            modelBuilder.Entity<EventUser>().HasOne(x => x.Users).WithMany(x => x.EventUsers).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<UserGroups>().HasKey(x => new { x.GroupId, x.UserId });
            modelBuilder.Entity<Events>().Property(x => x.CreatedTime).ValueGeneratedOnAdd();
        }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }
    }
}
