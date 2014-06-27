using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private Dbo.User selectedContact;
        public Dbo.User SelectedContact
        {
            get { return selectedContact; }
            set { if (SetField(ref selectedContact, value, "SelectedContact")) OnContactChanged(); }
        }

        private ObservableCollection<Dbo.Invitation> invitations;
        public ObservableCollection<Dbo.Invitation> Invitations
        {
            get { return invitations; }
            set { SetField(ref invitations, value, "Invitations"); }
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

        private ObservableCollection<Dbo.Group> groups;
        public ObservableCollection<Dbo.Group> Groups
        {
            get { return groups; }
            set { SetField(ref groups, value, "Groups"); }
        }

        private Dbo.Group selectedGroup;
        public Dbo.Group SelectedGroup
        {
            get { return selectedGroup; }
            set { if (SetField(ref selectedGroup, value, "SelectedGroup")) OnGroupChanged(); }
        }

        private ObservableCollection<Dbo.Discussion> discussions;
        public ObservableCollection<Dbo.Discussion> Discussions
        {
            get { return discussions; }
            set { SetField(ref discussions, value, "Discussions"); }
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

        private ICommand _newGroupCommand;
        public ICommand NewGroupCommand
        {
            get
            {
                if (_newGroupCommand == null)
                    _newGroupCommand = new RelayCommand(NewGroup);
                return _newGroupCommand;
            }
        }

        public ChatViewModel()
        {
            messages = new ObservableCollection<Dbo.Message>();
            contacts = new ObservableCollection<Dbo.User>();
            users = new ObservableCollection<Dbo.User>();
            groups = new ObservableCollection<Dbo.Group>();
            discussions = new ObservableCollection<Dbo.Discussion>();
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


        private void NewGroup()
        {
            new Views.NewGroupView().ShowDialog();
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
                {
                    var entry = list.Single(x => x.Id == contact.ContactId);
                    Contacts.Add(entry);
                    Users.Remove(entry);
                }));

            /**
             * Load groups in which I am
             */
            var groupusers = await MainViewModel.Proxy.Invoke<IList<Dbo.GroupUser>>("Execute", new object[] { new string[] { "groupuser-all" } });
            foreach (var item in groupusers.Where(x => x.UserId == userId))
            {
                Dbo.Group group = await MainViewModel.Proxy.Invoke<Dbo.Group>("Execute", new object[] { new string[] { "group-id", item.GroupId.ToString() } });
                await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                    Groups.Add(group)
                ));
            }
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
            else if (cmd == "user-insert")
            {
                await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    var entry = JsonConvert.DeserializeObject<Dbo.User>(data);
                    Users.Add(entry);
                }));
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
                    {
                        Contacts.Add(newContact);
                        Users.Remove(newContact);
                    }));
                }
            }
            else if (cmd == "message-insert")
            {
                Dbo.Message item = JsonConvert.DeserializeObject<Dbo.Message>(data);
                if (selectedContact != null && item.UserToId == userId && item.UserFromId == selectedContact.Id || item.UserFromId == userId)
                    await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                            Messages.Add(item)
                    ));
            }
            else if (cmd == "groupuser-insert")
            {
                Dbo.GroupUser item = JsonConvert.DeserializeObject<Dbo.GroupUser>(data);
                if (item.UserId == userId)
                {
                    Dbo.Group group = await MainViewModel.Proxy.Invoke<Dbo.Group>("Execute", new object[] { new string[] { "group-id", item.GroupId.ToString() } });
                    await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                        Groups.Add(group)
                    ));
                }
            }
        }

        private bool messagingEnable = false;
        public bool MessagingEnable
        {
            get { return messagingEnable; }
            set { SetField(ref messagingEnable, value, "MessagingEnable"); }
        }

        private bool discussionEnable = false;
        public bool DiscussionEnable
        {
            get { return discussionEnable; }
            set { SetField(ref discussionEnable, value, "DiscussionEnable"); }
        }


        async internal void OnContactChanged()
        {
            if (selectedContact == null) return;
            MessagingEnable = true;
            DiscussionEnable = false;
            Messages.Clear();
            Discussions.Clear();
            CurrentMessage = string.Empty;
            OnSelectionChanged(new SelectionChangedEventArgs() { Sender = "Contact" });
            //SelectedGroup = null;
            //OnPropertyChanged("Groups");

            var list = await MainViewModel.Proxy.Invoke<IEnumerable<Dbo.Message>>("Execute", new object[] { new string[] { "message-getByContact", userId.ToString(), selectedContact.Id.ToString() } });
            await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                Messages = new ObservableCollection<Dbo.Message>(list)
            ));
        }

        async internal void OnGroupChanged()
        {
            if (selectedGroup == null) return;
            MessagingEnable = false;
            DiscussionEnable = true;
            Messages.Clear();
            Discussions.Clear();
            CurrentMessage = string.Empty;
            OnSelectionChanged(new SelectionChangedEventArgs() { Sender = "Group" });
            //SelectedContact = null;
            //OnPropertyChanged("Contacts");


            var list = await MainViewModel.Proxy.Invoke<IEnumerable<Dbo.Discussion>>("Execute", new object[] { new string[] { "discussion-getByGroupId", SelectedGroup.Id.ToString() } });
            await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                Discussions = new ObservableCollection<Dbo.Discussion>(list)
            ));
        }


        internal void OnConnectionInfo(string info, int uid)
        {
            Debug.Print("OnConnectionInfo : {0} => {1}", info, uid);
            var item = contacts.SingleOrDefault(x => x.Id == uid);
            if (item != null)
                item.IsOnline = info == "connexion";
        }

        public event SelectionChangedEventHandler SelectionChanged;
        protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            SelectionChangedEventHandler handler = SelectionChanged;
            if (handler != null)
                handler(this, e);
        }
    }

    public class SelectionChangedEventArgs : EventArgs
    {
        public string Sender { get; set; }
    }

    public delegate void SelectionChangedEventHandler(Object sender, SelectionChangedEventArgs e);
}
