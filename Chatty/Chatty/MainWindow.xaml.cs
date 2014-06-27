using Chatty.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chatty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModel.MainViewModel();

            var login = ServiceLocator.Current.GetInstance<ViewModel.LoginViewModel>();
            login.Logged += Logged;
            login.Create += login_Create;

            var signUp = ServiceLocator.Current.GetInstance<ViewModel.SignUpViewModel>();
            signUp.GoBack += signUp_GoBack;
            signUp.SignUpped += signUp_SignUpped;
        }

        void signUp_SignUpped(object sender, EventArgs e)
        {
            this.loginView.Visibility = Visibility.Visible;
            this.signInView.Visibility = Visibility.Hidden;
        }


        void signUp_GoBack(object sender, EventArgs e)
        {
            this.loginView.Visibility = Visibility.Visible;
            this.signInView.Visibility = Visibility.Hidden;
        }

        void login_Create(object sender, EventArgs e)
        {
            this.loginView.Visibility = Visibility.Hidden;
            this.signInView.Visibility = Visibility.Visible;
        }

        void Logged(object sender, ViewModel.LoginEventArgs e)
        {
            if (e.Logged)
            {
                this.loginView.Visibility = Visibility.Hidden;
                this.signInView.Visibility = Visibility.Hidden;
                this.chatView.Visibility = Visibility.Visible;
                var chatViewModel = ServiceLocator.Current.GetInstance<ChatViewModel>();
                chatViewModel.userId = e.UserId;
                (this.chatView.DataContext as ViewModel.ChatViewModel).LoadData();
                Width *= 3;
            }
            else
            {
                System.Windows.MessageBox.Show("KO");
            }
        }
    }
}
