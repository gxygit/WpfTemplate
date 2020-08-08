using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScreenCapture
{
    /// <summary>
    /// Interaction logic for DingWindow.xaml
    /// </summary>
    public partial class DingWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BitmapSource bitmap;

        public BitmapSource Bmp
        {
            get { return bitmap; }
            set
            {
                bitmap = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bmp)));
            }
        }

        public DingWindow(BitmapSource bitmapSource)
        {
            InitializeComponent();
            pictureGrid.MouseLeftButtonDown += (s, e) => DragMove();
            DataContext = this;
            Bmp = bitmapSource;
            this.Width = Bmp.Width+20;
            this.Height = Bmp.Height+20;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}