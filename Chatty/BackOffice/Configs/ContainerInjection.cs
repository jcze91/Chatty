using BackOffice.DataAccess;
using BackOffice.Providers;
using BackOffice.Services;
using BackOffice.Utils;
using Microsoft.Practices.Unity;
using System.Collections.Concurrent;

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
            this.RegisterInstance<ContactDao>(new ContactDao());
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
            this.RegisterInstance<ContactService>(new ContactService());
            this.RegisterInstance<DiscussionService>(new DiscussionService());
            this.RegisterInstance<GroupService>(new GroupService());
            this.RegisterInstance<GroupUserService>(new GroupUserService());
            this.RegisterInstance<InvitationService>(new InvitationService());
            this.RegisterInstance<MessageService>(new MessageService());
            this.RegisterInstance<DepartmentService>(new DepartmentService());
            this.RegisterInstance<UserService>(new UserService());

            /**
             * Runtime
             */
            this.RegisterInstance<Runtime>(new Runtime());

            /**
             * Providers
             */
            this.RegisterInstance<ContactProvider>(new ContactProvider());
            this.RegisterInstance<DiscussionProvider>(new DiscussionProvider());
            this.RegisterInstance<GroupProvider>(new GroupProvider());
            this.RegisterInstance<GroupUserProvider>(new GroupUserProvider());
            this.RegisterInstance<InvitationProvider>(new InvitationProvider());
            this.RegisterInstance<MessageProvider>(new MessageProvider());
            this.RegisterInstance<DepartmentProvider>(new DepartmentProvider());
            this.RegisterInstance<UserProvider>(new UserProvider());

            /**
             * Dictionary for mapping online/offline users
             */
            this.RegisterInstance<ConcurrentDictionary<string, int>>(new ConcurrentDictionary<string, int>());
        }
    }
}