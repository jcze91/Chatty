using BackOffice.DepartmentProxy;
using BackOffice.GroupProxy;
using BackOffice.InvitationProxy;
using BackOffice.MessageProxy;
using BackOffice.UserProxy;
using Microsoft.Practices.Unity;

namespace BackOffice.Configs
{
    public class ContainerInjection : UnityContainer
    {
        public void Configure()
        {
            /**
             * Services
             */
            this.RegisterInstance<DepartmentContractClient>(new DepartmentContractClient());
            this.RegisterInstance<GroupContractClient>(new GroupContractClient());
            this.RegisterInstance<InvitationContractClient>(new InvitationContractClient());
            this.RegisterInstance<MessageContractClient>(new MessageContractClient());
            this.RegisterInstance<UserContractClient>(new UserContractClient());
        }
    }
}