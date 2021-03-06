﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chatty.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
            this.Loaded += ChatView_Loaded;
        }

        void ChatView_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ViewModel.ChatViewModel;
            viewModel.SelectionChanged += viewModel_SelectionChanged;
        }

        void viewModel_SelectionChanged(object sender, ViewModel.SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as ViewModel.ChatViewModel;
            if (e.Sender == "Contact")
                GroupsBox.UnselectAll();
            if (e.Sender == "Group")
                ContactsBox.UnselectAll();
        }

    }
}
