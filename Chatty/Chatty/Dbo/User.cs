using Chatty.ViewModel;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;
using System.Linq;

namespace Chatty.Dbo
{
    public partial class User : Utils.BaseModel<int>
    {
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }

        private bool isOnline;
        public bool IsOnline
        {
            get { return isOnline; }
            set { SetField(ref isOnline, value, "IsOnline"); }
        }

        public int DepartmentId { get; set; }
        private Department department;

        public Department Department
        {
            get { return department; }
            set { SetField(ref department, value, "Department"); }
        }

        public string Thumbnail { get; set; }

        private ICommand _inviteCommand;
        public ICommand InviteCommand
        {
            get
            {
                if (_inviteCommand == null)
                    _inviteCommand = new RelayCommand(Invite);
                return _inviteCommand;
            }
        }


        private ICommand _addToGroupCommand;
        public ICommand AddToGroupCommand
        {
            get
            {
                if (_addToGroupCommand == null)
                    _addToGroupCommand = new RelayCommand(AddToGroup);
                return _addToGroupCommand;
            }
        }

        async private void AddToGroup()
        {
            var chatViewModel = ServiceLocator.Current.GetInstance<ChatViewModel>();
            var group = chatViewModel.SelectedGroup;
            if (group != null)
            {
                var groupUser = await MainViewModel.Proxy.Invoke<Dbo.GroupUser>("Execute", new object[] { new string[] { "groupuser-insert", group.Id.ToString(), Id.ToString() } });
                if (groupUser != null)
                {
                    var addUserViewModel = ServiceLocator.Current.GetInstance<AddUserViewModel>();
                    var _user = addUserViewModel.Users.SingleOrDefault(x => x.Id == Id);
                    if (_user != null)
                        addUserViewModel.Users.Remove(_user);
                }
            }
        }

        protected override async void Initialize()
        {
            base.Initialize();
            IsOnline = await MainViewModel.Proxy.Invoke<bool>("IsUserOnline", new object[] { Id });
            Department = await MainViewModel.Proxy.Invoke<Department>("Execute", new object[] { new string[] { DepartmentId.ToString() } });
        }

        private void Invite()
        {
            var invite = new Views.Invite();
            (invite.DataContext as ViewModel.InviteViewModel).LoadData(Id);
            invite.ShowDialog();
        }
    }
}
