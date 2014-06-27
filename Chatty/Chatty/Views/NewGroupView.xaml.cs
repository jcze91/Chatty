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
    /// Interaction logic for NewGroupView.xaml
    /// </summary>
    public partial class NewGroupView : Window
    {
        public NewGroupView()
        {
            InitializeComponent();
            this.Loaded += NewGroupView_Loaded;
        }

        void NewGroupView_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.NewGroupViewModel).Close += NewGroupView_Close;
        }

        void NewGroupView_Close(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
