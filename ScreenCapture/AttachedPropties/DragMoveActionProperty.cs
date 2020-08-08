using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ViewModel.Base;

namespace ScreenCapture
{
    /// <summary>
    /// 父级控件必须是Canvas才可以拖动
    /// </summary>
    public class DragMoveActionProperty : BaseAttachedProperty<DragMoveActionProperty, Action<double,double>>
    {
        private Border border;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<double, double> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<double, double>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double h = startX+ p.X - startPoint.X;
                    double v = startY+ p.Y - startPoint.Y;
                    if (h < 0) h = 0;
                    if (h > parentCanvas.ActualWidth - border.ActualWidth) h = parentCanvas.ActualWidth - border.ActualWidth;
                    if (v < 0) v = 0;
                    if (v > parentCanvas.ActualHeight - border.ActualHeight) v = parentCanvas.ActualHeight - border.ActualHeight;
                    changed?.Invoke(h, v);
                }
            }
        }

        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false;
            (sender as UIElement).ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                startX = Canvas.GetLeft(border);
                startY = Canvas.GetTop(border);
            }
            (sender as UIElement).CaptureMouse();
        }
        private Point startPoint;
        private double startX, startY;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentCanvas = border.Parent as Canvas;
        }
    }
}
