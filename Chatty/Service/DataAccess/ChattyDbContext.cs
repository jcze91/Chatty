﻿using Service.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Service.DataAccess
{
    public class ChattyDbContext : DbContext
    {
        public ChattyDbContext() : base("name=ChattyDbContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
    }
}
