using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chatty.ViewModel
{
    public class AddUserViewModel : Utils.BaseNotify
    {
        private ObservableCollection<Dbo.User> users;
        public ObservableCollection<Dbo.User> Users
        {
            get { return users; }
            set { SetField(ref users, value, "Users"); }
        }

        async public void LoadData()
        {
            var list = await MainViewModel.Proxy.Invoke<IEnumerable<Dbo.User>>("Execute", new object[] { new string[] { "user-all" } });
            var chatViewModel = ServiceLocator.Current.GetInstance<ChatViewModel>();
            Users = new ObservableCollection<Dbo.User>(list.Where(x => x.Id != chatViewModel.userId));
        }
    }
}
