using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
                await ViewModel.MainViewModel.Proxy.Invoke("LogOut", new object[] { ViewModel.ChatViewModel.userId });
            }
            catch { }
        }
    }
}
