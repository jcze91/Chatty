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
    public class NewGroupViewModel : Utils.BaseNotify
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value, "Name"); }
        }

        private ICommand _createCommand;
        public ICommand CreateCommand
        {
            get
            {
                if (_createCommand == null)
                    _createCommand = new RelayCommand(Create, CanCreate);
                return _createCommand;
            }
        }

        private bool CanCreate()
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        /// <summary>
        /// add group &amp; add current user to this group
        /// </summary>
        async private void Create()
        {
            var chatViewModel = ServiceLocator.Current.GetInstance<ChatViewModel>();
            Dbo.Group group = await MainViewModel.Proxy.Invoke<Dbo.Group>("Execute", new object[] { new string[] { "group-insert", name } });
            await MainViewModel.Proxy.Invoke("Execute", new object[] { new string[] { "groupuser-insert", group.Id.ToString(), chatViewModel.userId.ToString() } });
            OnClose(EventArgs.Empty);
        }

        public event EventHandler Close;
        protected virtual void OnClose(EventArgs e)
        {
            EventHandler handler = Close;
            if (handler != null)
                handler(this, e);
        }
    }
}
