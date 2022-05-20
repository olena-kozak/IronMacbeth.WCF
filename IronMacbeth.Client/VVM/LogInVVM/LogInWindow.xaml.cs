using System.Windows;
using System.Windows.Controls;

namespace IronMacbeth.Client.VVM.LogInVVM
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                var passwordProperty = DataContext.GetType().GetProperty("Password");
                passwordProperty.SetValue(DataContext, ((PasswordBox)sender).Password);
            }
        }
    }
}