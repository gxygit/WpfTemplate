using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScreenCapture
{
    /// <summary>
    /// Interaction logic for TextBlockBox.xaml
    /// </summary>
    public partial class TextBlockBox : UserControl
    {
       
        public static Brush borderBrush = new SolidColorBrush(Colors.Transparent);

        public TextBlockBox()
        {
            InitializeComponent();
        }
        public TextBlockBox Clone()
        {
            TextBlockBox tbb = new TextBlockBox();
            tbb.txtBox.Text = txtBox.Text;
            tbb.txtBox.BorderBrush = borderBrush;
            tbb.txtBox.FontSize = this.txtBox.FontSize;
            tbb.txtBox.Foreground = this.txtBox.Foreground;
            this.FontSize = 18.0;
            return tbb;
        }
    }
}
