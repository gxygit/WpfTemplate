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
using System.Management;
using System.Security;
using System.Runtime.InteropServices;

namespace WpfTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window,IHavePassword
    {
        public MainWindow()
        {
            InitializeComponent(); ;
            DataContext = new MainWindowViewModel(this);
        }

        public SecureString SecurePassword => PasswordText?.SecurePassword;

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
