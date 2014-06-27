using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatty.ViewModel
{
    public class InviteViewModel : Utils.BaseNotify
    {
        private Dbo.User user;
        public Dbo.User User
        {
            get { return user; }
            set { SetField(ref user, value, "User"); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetField(ref content, value, "Content"); }
        }


        private ICommand _sendMessageCommand;
        public ICommand SendMessageCommand
        {
            get
            {
                if (_sendMessageCommand == null)
                    _sendMessageCommand = new RelayCommand(SendMessage, canSendMessage);
                return _sendMessageCommand;
            }
        }

        private bool canSendMessage()
        {
            return !string.IsNullOrWhiteSpace(content);
        }

        async private void SendMessage()
        {
            var chatViewModel = ServiceLocator.Current.GetInstance<ChatViewModel>();
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "invitation-insert", chatViewModel.userId.ToString(), User.Id.ToString(), "Add me " } });
            OnClose(EventArgs.Empty);
        }

        public event EventHandler Close;
        protected virtual void OnClose(EventArgs e)
        {
            EventHandler handler = Close;
            if (handler != null)
                handler(this, e);
        }

        async public void LoadData(int uid)
        {
            User = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-id", uid.ToString() } });
        }
    }
}
