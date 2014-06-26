using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
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

        private Dbo.User selectedContact;
        public Dbo.User SelectedContact
        {
            get { return selectedContact; }
            set { if (SetField(ref selectedContact, value, "SelectedContact")) OnContactChanged(); }
        }

        private string currentMessage;
        public string CurrentMessage
        {
            get { return currentMessage; }
            set { SetField(ref currentMessage, value, "CurrentMessage"); }
        }

        private ObservableCollection<Dbo.Message> messages;
        public ObservableCollection<Dbo.Message> Messages
        {
            get { return messages; }
            set { SetField(ref messages, value, "Messages"); }
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

        public ChatViewModel()
        {
            messages = new ObservableCollection<Dbo.Message>();
        }

        private bool canSendMessage()
        {
            return !string.IsNullOrWhiteSpace(currentMessage);
        }

        async private void SendMessage()
        {
            var result = await MainViewModel.Proxy.Invoke<object>("Execute", new object[] { new string[] { "message-insert", userId.ToString(), selectedContact.Id.ToString(), currentMessage } });
            if (result == null || result is bool && !((bool)result))
                System.Windows.MessageBox.Show("Fail to send message");
            else
                CurrentMessage = string.Empty;

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
                await App.Current.Dispatcher.BeginInvoke((Action)(() => 
                    Contacts.Add(list.Single(x => x.Id == contact.ContactId))
                ));
        }

        async public void Callback(string[] args, dynamic result)
        {
            var data = result.ToString();
            string cmd = args[0];

            if (cmd == "invitation-insert")
            {
                Dbo.Invitation item = JsonConvert.DeserializeObject<Dbo.Invitation>(data);
                if (item.ToUserId == userId)
                    await App.Current.Dispatcher.BeginInvoke((Action)(() => 
                        Invitations.Add(item)
                    ));
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
                    await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                        Contacts.Add(newContact)
                    ));
                }
            }
            else if (cmd == "message-insert")
            {
                Dbo.Message item = JsonConvert.DeserializeObject<Dbo.Message>(data);
                if (item.UserToId == userId && item.UserFromId == selectedContact.Id || item.UserFromId == userId)
                {
                    await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                            Messages.Add(item)
                    ));
                }
            }
        }

        async private void OnContactChanged()
        {
            Messages.Clear();
            CurrentMessage = string.Empty;
            var list = await MainViewModel.Proxy.Invoke<IEnumerable<Dbo.Message>>("Execute", new object[] { new string[] { "message-getByContact", userId.ToString(), selectedContact.Id.ToString() } });

            await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                Messages = new ObservableCollection<Dbo.Message>(list)
            ));
        }

    }
}
