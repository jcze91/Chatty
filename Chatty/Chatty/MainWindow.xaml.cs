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
            var dc = DataContext as ViewModel.MainViewModel;
            dc.Login.Logged += MainWindow_Logged;
        }

        void MainWindow_Logged(object sender, ViewModel.LoginEventArgs e)
        {
            if(e.Logged)
            {
                System.Windows.MessageBox.Show("OK");
            }
            else
            {
                System.Windows.MessageBox.Show("KO");

            }
        }


    }
}
