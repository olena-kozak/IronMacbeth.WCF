using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using IronMacbeth.Client.ViewModel;

namespace IronMacbeth.Client.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}