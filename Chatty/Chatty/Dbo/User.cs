using Chatty.ViewModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows.Media;

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

        protected override async void Initialize()
        {
            base.Initialize();
            IsOnline = await MainViewModel.Proxy.Invoke<bool>("IsUserOnline", new object[] { Id });
            Department = await MainViewModel.Proxy.Invoke<Department>("Execute", new object[] { new string[] { DepartmentId.ToString() } });
        }

        async private void Invite()
        {
            System.Diagnostics.Debug.WriteLine("invite called in Dbo.User : " + Username);
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "invitation-insert", ChatViewModel.userId.ToString(), Id.ToString(), "Add me " } });
        }
    }
}
