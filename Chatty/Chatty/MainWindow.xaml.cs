using Chatty.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Chatty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = new ViewModel.MainViewModel();
            DataContext = viewModel;
            SimpleIoc.Default.Register<MainViewModel>(test);

            var login = ServiceLocator.Current.GetInstance<ViewModel.LoginViewModel>();
            login.Logged += Logged;
            login.Create += login_Create;

            var signUp = ServiceLocator.Current.GetInstance<ViewModel.SignUpViewModel>();
            signUp.GoBack += signUp_GoBack;
            signUp.SignUpped += signUp_SignUpped;

            (DataContext as MainViewModel).OnWizz += MainWindow_OnWizz;
        }

        private MainViewModel test()
        {
            return viewModel;
        }

        void MainWindow_OnWizz(object sender, EventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                Storyboard sb = this.FindResource("Shake") as Storyboard;
                Storyboard.SetTarget(sb, this);
                sb.Begin();
            }));
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
