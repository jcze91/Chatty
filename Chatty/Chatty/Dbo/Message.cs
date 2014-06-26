using Chatty.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Dbo
{
    public partial class Message : Utils.BaseModel<int>
    {
        public User Sender { get; set; }

        private int userFromId;
        public int UserFromId
        {
            get { return userFromId; }
            set { userFromId = value; LoadData(); }
        }

        async private void LoadData()
        {
            Sender = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-id", UserFromId.ToString() } });
            OnPropertyChanged("Sender");
        }

        public int UserToId { get; set; }
        public string Content { get; set; }
    }
}
