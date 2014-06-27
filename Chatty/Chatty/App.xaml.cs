using Chatty.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace Chatty
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            LogOut();
            base.OnExit(e);
        }

        private async void LogOut()
        {
            try
            {
                var chatViewModel = ServiceLocator.Current.GetInstance<ChatViewModel>();
                await ViewModel.MainViewModel.Proxy.Invoke("LogOut", new object[] { chatViewModel.userId });
            }
            catch { }
        }
    }
}
