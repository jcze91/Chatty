using Chatty.ViewModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Chatty.Dbo
{
    public partial class User : Utils.BaseEntity<int>
    {
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }

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

        async private void Invite()
        {
            System.Diagnostics.Debug.WriteLine("invite called in Dbo.User : " + Username);
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "invitation-insert", ChatViewModel.userId.ToString(), Id.ToString(), "Add me " } });
        }
    }
}
