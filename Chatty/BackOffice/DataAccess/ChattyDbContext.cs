using BackOffice.Dbo;
using System.Data.Entity;

namespace BackOffice.DataAccess
{
    public class ChattyDbContext : DbContext
    {
        public ChattyDbContext() : base("name=ChattyDbContext") { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}