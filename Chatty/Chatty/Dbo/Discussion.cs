using Chatty.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Dbo
{
    public class Discussion : Utils.BaseModel<int>
    {
        public int GroupId { get; set; }
        public int UserFromId { get; set; }
        public string Content { get; set; }
        public User Sender { get; set; }

        protected override async void Initialize()
        {
            Sender = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-id", UserFromId.ToString() } });
            OnPropertyChanged("Sender");
        }
    }
}
