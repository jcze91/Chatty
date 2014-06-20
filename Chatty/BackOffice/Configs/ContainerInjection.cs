using BackOffice.DataAccess;
using BackOffice.Services;
using Microsoft.Practices.Unity;

namespace BackOffice.Configs
{
    public class ContainerInjection : UnityContainer
    {
        public void Configure()
        {
            this.RegisterInstance<ChattyDbContext>(new ChattyDbContext());

            /**
             * DataAccess
             */
            this.RegisterInstance<DiscussionDao>(new DiscussionDao());
            this.RegisterInstance<GroupDao>(new GroupDao());
            this.RegisterInstance<GroupUserDao>(new GroupUserDao());
            this.RegisterInstance<InvitationDao>(new InvitationDao());
            this.RegisterInstance<MessageDao>(new MessageDao());
            this.RegisterInstance<DepartmentDao>(new DepartmentDao());
            this.RegisterInstance<UserDao>(new UserDao());

            /**
             * Services
             */
            this.RegisterInstance<DiscussionService>(new DiscussionService());
            this.RegisterInstance<GroupService>(new GroupService());
            this.RegisterInstance<GroupUserService>(new GroupUserService());
            this.RegisterInstance<InvitationService>(new InvitationService());
            this.RegisterInstance<MessageService>(new MessageService());
            this.RegisterInstance<DepartmentService>(new DepartmentService());
            this.RegisterInstance<UserService>(new UserService());
        }
    }
}