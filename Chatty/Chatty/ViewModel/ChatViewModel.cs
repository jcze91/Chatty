using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
             */
            Invitations = await MainViewModel.Proxy.Invoke<ObservableCollection<Dbo.Invitation>>("Execute", new object[] { new string[] { "invitation-all" } });

            /**
             * Get contact list
             */
            var c_users = await MainViewModel.Proxy.Invoke<IList<Dbo.Contact>>("Execute", new object[] { new string[] { "contact-all" } });
            Contacts = new ObservableCollection<Dbo.User>(Users.Where(x => c_users.Count(i => i.UserId == userId) > 0));
        }

        async public void Callback(string cmd, dynamic result)
        {
            var data = result.ToString();

            if (cmd == "invitation-add")
            {
                Dbo.Invitation item = JsonConvert.DeserializeObject<Dbo.Invitation>(data);
                if (item.ToUserId == userId)
                    Invitations.Add(item);
            }
            else if (cmd == "invitation-delete")
            {
                Dbo.Invitation item = JsonConvert.DeserializeObject<Dbo.Invitation>(data);
                Invitations.Remove(invitations.SingleOrDefault(x => x.Id == item.Id));
            }
            else if (cmd == "contact-insert")
            {
                Dbo.Contact item = JsonConvert.DeserializeObject<Dbo.Contact>(data);
                if (item.UserId == userId)
                {
                    var newContact = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-id", item.ContactId.ToString() } });
                    contacts.Add(newContact);
                }
            }
        }
    }
}
