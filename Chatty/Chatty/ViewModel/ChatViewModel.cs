using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Chatty.ViewModel
{
    public class ChatViewModel : Utils.BaseNotify
    {
        public static int userId;

        private Dbo.User user;
        public Dbo.User User
        {
            get { return user; }
            set { SetField(ref user, value, "User"); }
        }

        private ObservableCollection<Dbo.User> contacts;
        public ObservableCollection<Dbo.User> Contacts
        {
            get { return contacts; }
            set { SetField(ref contacts, value, "Contacts"); }
        }

        private ObservableCollection<Dbo.User> users;
        public ObservableCollection<Dbo.User> Users
        {
            get { return users; }
            set { SetField(ref users, value, "Users"); }
        }

        private ObservableCollection<Dbo.Invitation> invitations;
        public ObservableCollection<Dbo.Invitation> Invitations
        {
            get { return invitations; }
            set { SetField(ref invitations, value, "Invitations"); }
        }

        async public void LoadData()
        {
            /**
             * Get User list
             */
            var list = await MainViewModel.Proxy.Invoke<IEnumerable<Dbo.User>>("Execute", new object[] { new string[] { "user-all" } });
            Users = new ObservableCollection<Dbo.User>(list.Where(x => x.Id != userId));

            /**
             * Get user details
             */
            User = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-id", userId.ToString() } });

            /** 
             * Get invitations
             * 
             * for me
             */
            var tmpiList = await MainViewModel.Proxy.Invoke<IList<Dbo.Invitation>>("Execute", new object[] { new string[] { "invitation-all" } });
            Invitations = new ObservableCollection<Dbo.Invitation>(tmpiList.Where(x => x.ToUserId == userId));

            /**
             * Get contact list
             * 
             * Add users in my contact list
             */
            Contacts = new ObservableCollection<Dbo.User>();
            var c_users = await MainViewModel.Proxy.Invoke<IList<Dbo.Contact>>("Execute", new object[] { new string[] { "contact-all" } });
            foreach (var contact in c_users.Where(x => x.ContactId != userId && x.UserId == userId))
                await App.Current.Dispatcher.BeginInvoke((Action)(() => Contacts.Add(list.Single(x => x.Id == contact.ContactId))));
        }

        async public void Callback(string[] args, dynamic result)
        {
            var data = result.ToString();
            string cmd = args[0];

            if (cmd == "invitation-insert")
            {
                Dbo.Invitation item = JsonConvert.DeserializeObject<Dbo.Invitation>(data);
                if (item.ToUserId == userId)
                    await App.Current.Dispatcher.BeginInvoke((Action)(() => Invitations.Add(item)));
            }
            else if (cmd == "invitation-delete")
            {
                var item = Invitations.SingleOrDefault(x => x.Id == int.Parse(args[1]));
                if (item != null)
                    await App.Current.Dispatcher.BeginInvoke((Action)(() => Invitations.Remove(item)));
            }
            else if (cmd == "contact-insert")
            {
                Dbo.Contact item = JsonConvert.DeserializeObject<Dbo.Contact>(data);
                if (item.UserId == userId)
                {
                    var newContact = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-id", item.ContactId.ToString() } });
                    await App.Current.Dispatcher.BeginInvoke((Action)(() => Contacts.Add(newContact)));
                }
            }
        }

        private ICommand _debugCommand;
        public ICommand DebugCommand
        {
            get
            {
                if (_debugCommand == null)
                    _debugCommand = new RelayCommand(Debug);
                return _debugCommand;
            }
        }

        async private void Debug()
        {
        }
    }
}
