using Chatty.ViewModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatty.Dbo
{
    public class Invitation : Utils.BaseModel<int>
    {
        public string Username { get; set; }

        private async void LoadData()
        {
            var res = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-id", FromUserId.ToString() } });
            Username = res.Username;
            OnPropertyChanged("Username");
        }

        private int fromUserId;
        public int FromUserId
        {
            get { return fromUserId; }
            set { fromUserId = value; LoadData(); }
        }
        public int ToUserId { get; set; }
        public string Content { get; set; }



        private ICommand _acceptCommand;
        public ICommand AcceptCommand
        {
            get
            {
                if (_acceptCommand == null)
                    _acceptCommand = new RelayCommand(Accept);
                return _acceptCommand;
            }
        }

        async private void Accept()
        {
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "invitation-delete", Id.ToString() } });
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "contact-insert", FromUserId.ToString(), ToUserId.ToString() } });
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "contact-insert", ToUserId.ToString(), FromUserId.ToString() } });
        }

        private ICommand _declineCommand;
        public ICommand DeclineCommand
        {
            get
            {
                if (_declineCommand == null)
                    _declineCommand = new RelayCommand(Accept);
                return _declineCommand;
            }
        }

        async private void Decline()
        {
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "invitation-delete", Id.ToString() } });
        }
    }
}
