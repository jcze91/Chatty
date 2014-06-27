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
using System.Windows.Shapes;

namespace Chatty.Views
{
    /// <summary>
    /// Interaction logic for Invite.xaml
    /// </summary>
    public partial class Invite : Window
    {
        public Invite()
        {
            InitializeComponent();
            this.Loaded += Invite_Loaded;
        }

        void Invite_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.InviteViewModel).Close += Invite_Close;
        }

        void Invite_Close(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
