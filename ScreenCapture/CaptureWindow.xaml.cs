using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ViewModel.Base;

namespace ScreenCapture
{
    public partial class CaptureWindow : Window, INotifyPropertyChanged
    {
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

        #region NotifyPropties
        #region NotifyProp FullScreenSource
        private BitmapSource fullScreenSource;
        public BitmapSource FullScreenSource
        {
            get => fullScreenSource;
            set
            {
                fullScreenSource = value;
                OnPropertyChanged(nameof(FullScreenSource));
            }
        }
        #endregion

        #region NotifyProp ToolVisible
        private bool toolVisible;
        public bool ToolVisible
        {
            get => toolVisible;
            set
            {
                toolVisible = value;
                OnPropertyChanged(nameof(ToolVisible));
            }
        }
        #endregion

        #region NotifyProp ToolLeft
        private double toolLeft;
        public double ToolLeft
        {
            get => toolLeft;
            set
            {
                toolLeft = value;
                OnPropertyChanged(nameof(ToolLeft));
            }
        }
        #endregion

        #region NotifyProp ToolTop
        private double toolTop;
        public double ToolTop
        {
            get => toolTop;
            set
            {
                toolTop = value;
                OnPropertyChanged(nameof(ToolTop));
            }
        }
        #endregion

        #region NotifyProp CaptureRect
        private Rect captureRect = new Rect(0, 0, 0, 0);
        public Rect CaptureRect
        {
            get { return captureRect; }
            set
            {
                captureRect = value;
                OnPropertyChanged(nameof(CaptureRect));
            }
        }
        #endregion

        #region NotifyProp ToolType
        private ToolTypes toolType = ToolTypes.None;
        public ToolTypes ToolType
        {
            get { return toolType; }
            set
            {
                toolType = value;
                OnPropertyChanged(nameof(ToolType));
            }
        }
        #endregion

        #region NotifyProp InkStrokes
        private StrokeCollection inkStrokes = new StrokeCollection();
        public StrokeCollection InkStrokes
        {
            get { return inkStrokes; }
            set
            {
                inkStrokes = value;
                OnPropertyChanged(nameof(InkStrokes));
            }
        }
        #endregion

        #region NotifyProp AddingText
        private bool addingText;

        public bool AddingText
        {
            get { return addingText; }
            set
            {
                addingText = value;
                OnPropertyChanged(nameof(AddingText));
            }
        }
        #endregion
        #endregion

        public CaptureWindow()
        {
            InitializeComponent();
            this.Height =  SystemInformation.VirtualScreen.Height;
            this.Width =  SystemInformation.VirtualScreen.Width;
            this.Left = 0;
            this.Top = 0;
            DataContext = this;
            var bitmap = GetScreenSnapshot();
            FullScreenSource = ImageHelper.BitmapToBitmapImage(bitmap);
            bitmap.Dispose();
            inkCanvasMeasure.DefaultDrawingAttributes = new DrawingAttributes()
            {
                Color = Colors.Red
            };
        }
        public Bitmap GetScreenSnapshot()
        {
            System.Drawing.Rectangle rc = SystemInformation.VirtualScreen;
            var bitmap = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics memoryGrahics = Graphics.FromImage(bitmap))
            {
                memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
            }
            return bitmap;
        }

        private bool isCaptured;
        private System.Windows.Point point1, point2;
        private Stroke currentStroke;
        /// <summary>
        /// 0没有按下 
        /// 1没有选择好区域的时候mousedow
        /// 2 ToolTypes.Rect ToolTypes.Circle ToolTypes.Arrow 时 mouseDown
        /// 3 选中文字工具时 mouseDown
        /// </summary>
        private int mouseDownState;

        #region drag 回调
        private void OnDragMove(double x, double y)
        {
            captureRect.X = x;
            captureRect.Y = y;
            OnPropertyChanged(nameof(CaptureRect));
            RefreshToolLocation();
        }
        private void OnDragResize(Grid grid, Rect rect)
        {
            CaptureRect = rect;
            RefreshToolLocation();
        }
        #endregion

        #region 鼠标事件
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isCaptured = true;
                RefreshToolLocation();
                ToolVisible = true;
                if (mouseDownState != 2)
                {
                    ClipBorderCanvas.IsHitTestVisible = true;
                }
                mouseDownState = 0;
                currentStroke = null;
            }
            (sender as UIElement).ReleaseMouseCapture();
        }
        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mouseDownState == 1)
            {
                var p = e.GetPosition(this);
                point2 = p;
                CaptureRect = new Rect(point1, point2);
            }
            else if (mouseDownState == 2)
            {
                point2 = e.GetPosition(this);
                InkStrokes.Remove(currentStroke);
                currentStroke = CreateStroke();
                InkStrokes.Add(currentStroke);
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                point1 = e.GetPosition(this);
                if (!isCaptured)
                {
                    mouseDownState = 1;
                }
                else
                {
                    if (ToolType == ToolTypes.Rect || ToolType == ToolTypes.Circle || ToolType == ToolTypes.Arrow || ToolType == ToolTypes.Pen || ToolType == ToolTypes.Mosaic)
                    {
                        mouseDownState = 2;
                    }
                    else if (ToolType == ToolTypes.Text)
                    {
                        if (input.Visibility == Visibility.Hidden)
                        {
                            mouseDownState = 3;
                            input.SetValue(Canvas.LeftProperty, point1.X);
                            input.SetValue(Canvas.TopProperty, point1.Y);
                            AddingText = true;
                            input.Visibility = Visibility.Visible;
                            input.txtBox.Text = "";
                            input.txtBox.Focus();
                        }
                    }
                }
            }
            (sender as UIElement).CaptureMouse();
        }
        #endregion
        #region 添加文字
        #endregion
        #region 生成Stroke的方法
        private Stroke CreateStroke()
        {
            if (ToolType == ToolTypes.Rect)
            {
                return CreateRectStroke(point1, point2);
            }
            else if (ToolType == ToolTypes.Circle)
            {
                return CreateCircleStroke(point1, point2);
            }
            else if (ToolType == ToolTypes.Arrow)
            {
                return CreateArrowStroke(point1, point2);
            }
            else if (ToolType == ToolTypes.Pen)
            {
                return CreatePenStroke(point1, point2);
            }
            else if (ToolType == ToolTypes.Mosaic)
            {
                return CreateMosaicStroke(point1, point2);
            }
            return null;
        }
        public Stroke CreateRectStroke(System.Windows.Point p1, System.Windows.Point p2)
        {
            List<System.Windows.Point> pointList = new List<System.Windows.Point>
                    {
                        new System.Windows.Point(p1.X, p1.Y),
                        new System.Windows.Point(p1.X, p2.Y),
                        new System.Windows.Point(p2.X, p2.Y),
                        new System.Windows.Point(p2.X, p1.Y),
                        new System.Windows.Point(p1.X, p1.Y),
                    };
            StylusPointCollection point = new StylusPointCollection(pointList);
            var stroke = new Stroke(point)
            {
                DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
            };
            return stroke;
        }
        public Stroke CreateCircleStroke(System.Windows.Point p1, System.Windows.Point p2)
        {
            double a = 0.5 * (p2.X - p1.X);
            double b = 0.5 * (p2.Y - p1.Y);
            List<System.Windows.Point> pointList = new List<System.Windows.Point>();
            for (double r = 0; r <= 2 * Math.PI; r = r + 0.01)
            {
                pointList.Add(new System.Windows.Point(0.5 * (p1.X + p2.X) + a * Math.Cos(r), 0.5 * (p1.Y + p2.Y) + b * Math.Sin(r)));
            }
            StylusPointCollection point = new StylusPointCollection(pointList);
            Stroke stroke = new Stroke(point)
            {
                DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
            };
            return stroke;
        }

        private double ArrowAngle = 0.30453292519943295;
        private double ArrowLengh = 25.0;
        public Stroke CreateArrowStroke(System.Windows.Point p1, System.Windows.Point p2)
        {
            double num = Math.Atan((p2.Y - p1.Y) / (p2.X - p1.X));
            double d = num - ArrowAngle;
            double num3 = num + ArrowAngle;
            int num4 = (p2.X > p1.X) ? -1 : 1;
            if (p2.X == p1.X)
            {
                num4 = (p2.Y > p1.Y) ? -1 : 1;
            }
            double x3 = p2.X + ((num4 * ArrowLengh) * Math.Cos(d));
            double y3 = p2.Y + ((num4 * ArrowLengh) * Math.Sin(d));
            double x4 = p2.X + ((num4 * ArrowLengh) * Math.Cos(num3));
            double y4 = p2.Y + ((num4 * ArrowLengh) * Math.Sin(num3));
            double x5 = p2.X + ((num4 * 18) * Math.Cos(num));
            double y5 = p2.Y + ((num4 * 18) * Math.Sin(num));
            System.Windows.Point point3 = new System.Windows.Point(x3, y3);
            System.Windows.Point point4 = new System.Windows.Point(x4, y4);
            System.Windows.Point point5 = new System.Windows.Point(x5, y5);
            List<System.Windows.Point> pointList = new List<System.Windows.Point>
            {
                p1,point5,point3,p2,point4,point5
            };
            StylusPointCollection point = new StylusPointCollection(pointList);
            var stroke = new Stroke(point);
            stroke.DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone();
            return stroke;
        }
        public Stroke CreatePenStroke(System.Windows.Point p1, System.Windows.Point p2)
        {
            Stroke stroke;
            if (currentStroke != null)
            {
                stroke = currentStroke;
                StylusPoint stylusPoint = new StylusPoint(p2.X, p2.Y);
                stroke.StylusPoints.Add(stylusPoint);
            }
            else
            {
                List<System.Windows.Point> pointList = new List<System.Windows.Point>
                {
                    new System.Windows.Point(p1.X, p1.Y),
                    new System.Windows.Point(p2.X, p2.Y)
                };
                StylusPointCollection point = new StylusPointCollection(pointList);
                stroke = new Stroke(point)
                {
                    DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone()
                };
            }
            return stroke;
        }
        public Stroke CreateMosaicStroke(System.Windows.Point p1, System.Windows.Point p2)
        {
            Stroke stroke;
            if (currentStroke != null)
            {
                stroke = currentStroke;
                StylusPoint stylusPoint = new StylusPoint(p2.X, p2.Y);
                stroke.StylusPoints.Add(stylusPoint);
            }
            else
            {
                List<System.Windows.Point> pointList = new List<System.Windows.Point>
                {
                    new System.Windows.Point(p1.X, p1.Y),
                    new System.Windows.Point(p2.X, p2.Y)
                };
                StylusPointCollection point = new StylusPointCollection(pointList);
                stroke = new Stroke(point);
                stroke.DrawingAttributes = inkCanvasMeasure.DefaultDrawingAttributes.Clone();
                stroke.DrawingAttributes.Width = 16;
                stroke.DrawingAttributes.Height = 16;
            }
            return stroke;
        }
        #endregion

        private BitmapSource GetCaptureSource()
        {
            imageSourceBack.Visibility = Visibility.Visible;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Default);
            rtb.Render(imageSourceGrid);
            BitmapImage image = ImageHelper.RenderTargetBitmapToBitmapImage(rtb);
            var rect = new Int32Rect((int)captureRect.X, (int)captureRect.Y, (int)captureRect.Width, (int)captureRect.Height);
            var stride = image.Format.BitsPerPixel * rect.Width / 8;
            byte[] data = new byte[rect.Height * stride];
            image.CopyPixels(rect, data, stride, 0);
            var res = BitmapSource.Create(rect.Width, rect.Height, 0, 0, PixelFormats.Bgr32, null, data, stride);
            return res;
        }
        private void RefreshToolLocation()
        {
            ToolLeft = captureRect.X;
            var y = captureRect.Y;
            if (this.Height - y - captureRect.Height < 35 && y < 35)
                ToolTop = y + captureRect.Height - 30;
            else if (this.Height - y - captureRect.Height < 35)
                ToolTop = y - 35;
            else
                ToolTop = y + captureRect.Height + 5;
        }

        #region tool buttons click
        private void OnRectClick(object sender, RoutedEventArgs e)
        {
            if (ToolType == ToolTypes.Rect)
            {
                ToolType = ToolTypes.None;
                ClipBorderCanvas.IsHitTestVisible = true;
            }
            else
            {
                ToolType = ToolTypes.Rect;
                ClipBorderCanvas.IsHitTestVisible = false;
            }
        }
        private void OnCircleClick(object sender, RoutedEventArgs e)
        {
            if (ToolType == ToolTypes.Circle)
            {
                ToolType = ToolTypes.None;
                ClipBorderCanvas.IsHitTestVisible = true;
            }
            else
            {
                ToolType = ToolTypes.Circle;
                ClipBorderCanvas.IsHitTestVisible = false;
            }
        }
        private void OnArrowClick(object sender, RoutedEventArgs e)
        {
            if (ToolType == ToolTypes.Arrow)
            {
                ToolType = ToolTypes.None;
                ClipBorderCanvas.IsHitTestVisible = true;
            }
            else
            {
                ToolType = ToolTypes.Arrow;
                ClipBorderCanvas.IsHitTestVisible = false;
            }
        }
        private void OnPenClick(object sender, RoutedEventArgs e)
        {
            if (ToolType == ToolTypes.Pen)
            {
                ToolType = ToolTypes.None;
                ClipBorderCanvas.IsHitTestVisible = true;
            }
            else
            {
                ToolType = ToolTypes.Pen;
                ClipBorderCanvas.IsHitTestVisible = false;
            }
        }
        private void OnMosaicClick(object sender, RoutedEventArgs e)
        {
            if (ToolType == ToolTypes.Mosaic)
            {
                ToolType = ToolTypes.None;
                ClipBorderCanvas.IsHitTestVisible = true;
            }
            else
            {
                ToolType = ToolTypes.Mosaic;
                ClipBorderCanvas.IsHitTestVisible = false;
            }
        }
        private void OnDownLoadClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "png图片|*.png|jpg图片|*.jpg";
            sfd.FileName = DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".png";
            if (sfd.ShowDialog() == true)
            {
                var image = GetCaptureSource();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    encoder.Save(fs);
                    fs.Close();
                }
                this.Close();
            }

        }
        private void OnTextClick(object sender, RoutedEventArgs e)
        {
            if (ToolType == ToolTypes.Text)
            {
                ToolType = ToolTypes.None;
                ClipBorderCanvas.IsHitTestVisible = true;
            }
            else
            {
                ToolType = ToolTypes.Text;
                ClipBorderCanvas.IsHitTestVisible = false;
            }
        }
        private void OnDingClick(object sender, RoutedEventArgs e)
        {
            var res = GetCaptureSource();
            DingWindow dingWindow = new DingWindow(res);
            dingWindow.Left = captureRect.X - 10;
            dingWindow.Top = captureRect.Y - 10;
            dingWindow.Show();
            this.Close();
        }
        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void OnCopyClick(object sender, RoutedEventArgs e)
        {
            var res = GetCaptureSource();
            System.Windows.Clipboard.SetImage(res);
            this.Close();
        }
        #endregion

        private void OnBorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            AddText();
        }
        public void AddText()
        {
            if (!addingText)
                return;
            TextBlockBox tbb = input.Clone();
            inkCanvasMeasure.Children.Add(tbb);
            tbb.SetValue(InkCanvas.LeftProperty, input.GetValue(Canvas.LeftProperty));
            tbb.SetValue(InkCanvas.TopProperty, input.GetValue(Canvas.TopProperty));
            input.Visibility = Visibility.Hidden;
            ClipBorderCanvas.IsHitTestVisible = false;
            //AddingText = false;
            //e.Handled = true;
        }

    }


}
