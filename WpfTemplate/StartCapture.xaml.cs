using ScreenCapture;
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

namespace WpfTemplate
{
    /// <summary>
    /// StartCapture.xaml 的交互逻辑
    /// </summary>
    public partial class StartCapture : Window
    {
        public StartCapture()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CaptureWindow captureWindow = new CaptureWindow();
            captureWindow.Show();
        }
    }
}
