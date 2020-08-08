using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ViewModel.Base;

namespace ScreenCapture
{
    /// <summary>
    /// 父级grid的父级控件必须是Canvas才可以拖动
    /// </summary>
    public class DragResizeAction1Property : BaseAttachedProperty<DragResizeAction1Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double hChange = p.X - startPoint.X;
                    double vChange = p.Y - startPoint.Y;
                    var rect = Resize(hChange, vChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize(double hChange, double vChange)
        {
            Rect rect = new Rect();
            var width = startRect.Width - hChange;
            if (width < 0)
            {
                rect.X = startRect.X + startRect.Width - 1;
                rect.Width = 1;
            }
            else
            {
                rect.X = startRect.X + hChange;
                rect.Width = width;
            }
            var height = startRect.Height - vChange;
            if (height < 0)
            {
                rect.Y = startRect.Y + startRect.Height - 1;
                rect.Height = 1;
            }
            else
            {
                rect.Y = startRect.Y + vChange;
                rect.Height = height;
            }
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false;
            border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();
        }
        private Point startPoint;
        private Rect startRect;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
        }
    }
    public class DragResizeAction2Property : BaseAttachedProperty<DragResizeAction2Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double vChange = p.Y - startPoint.Y;
                    var rect = Resize(vChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize( double vChange)
        {
            Rect rect = new Rect();
            rect.Width = startRect.Width;
            rect.X = startRect.X;
            var height = startRect.Height - vChange;
            if (height < 0)
            {
                rect.Y = startRect.Y + startRect.Height - 1;
                rect.Height = 1;
            }
            else
            {
                rect.Y = startRect.Y + vChange;
                rect.Height = height;
            }
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false; border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                
                
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();

        }
        private Point startPoint;
        private Rect startRect;
        private int direction;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
            direction = SizeDirectionBorderProperty.GetValue(border);
        }
    }
    public class DragResizeAction3Property : BaseAttachedProperty<DragResizeAction3Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double hChange = p.X - startPoint.X;
                    double vChange = p.Y - startPoint.Y;
                    var rect = Resize(hChange, vChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize(double hChange, double vChange)
        {
            Rect rect = new Rect();
            var width = startRect.Width + hChange;
            if (width < 0)
            {
                rect.X = startRect.X;
                rect.Width = 1;
            }
            else
            {
                rect.X = startRect.X;
                rect.Width = width;
            }
            var height = startRect.Height - vChange;
            if (height < 0)
            {
                rect.Y = startRect.Y + startRect.Height - 1;
                rect.Height = 1;
            }
            else
            {
                rect.Y = startRect.Y + vChange;
                rect.Height = height;
            }
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false;
            border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();
        }
        private Point startPoint;
        private Rect startRect;
        private int direction;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
            direction = SizeDirectionBorderProperty.GetValue(border);
        }
    }
    public class DragResizeAction4Property : BaseAttachedProperty<DragResizeAction4Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double hChange = p.X - startPoint.X;
                    var rect = Resize(hChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize(double hChange)
        {
            Rect rect = new Rect();
            var width = startRect.Width - hChange;
            if (width < 0)
            {
                rect.X = startRect.X + startRect.Width - 1;
                rect.Width = 1;
            }
            else
            {
                rect.X = startRect.X + hChange;
                rect.Width = width;
            }
            rect.Height = startRect.Height;
            rect.Y = startRect.Y;
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false;
            border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();
        }
        private Point startPoint;
        private Rect startRect;
        private int direction;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
            direction = SizeDirectionBorderProperty.GetValue(border);
        }
    }
    public class DragResizeAction5Property : BaseAttachedProperty<DragResizeAction5Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double hChange = p.X - startPoint.X;
                    var rect = Resize(hChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize(double hChange)
        {
            Rect rect = new Rect();
            rect.X = startRect.X;
            var width = startRect.Width + hChange;
            rect.Width = Math.Max(1, width);
            rect.Height = startRect.Height;
            rect.Y = startRect.Y;
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false;
            border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();
        }
        private Point startPoint;
        private Rect startRect;
        private int direction;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
            direction = SizeDirectionBorderProperty.GetValue(border);
        }
    }
    public class DragResizeAction6Property : BaseAttachedProperty<DragResizeAction6Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double hChange = p.X - startPoint.X;
                    double vChange = p.Y - startPoint.Y;
                    var rect = Resize(hChange, vChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize(double hChange, double vChange)
        {
            Rect rect = new Rect();
            var width = startRect.Width - hChange;
            if (width < 0)
            {
                rect.X = startRect.X + startRect.Width - 1;
                rect.Width = 1;
            }
            else
            {
                rect.X = startRect.X + hChange;
                rect.Width = width;
            }
            rect.Y = startRect.Y;
            var height = startRect.Height + vChange;
            rect.Height = Math.Max(1, height);
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false; border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();
        }
        private Point startPoint;
        private Rect startRect;
        private int direction;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
            direction = SizeDirectionBorderProperty.GetValue(border);
        }
    }
    public class DragResizeAction7Property : BaseAttachedProperty<DragResizeAction7Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double vChange = p.Y - startPoint.Y;
                    var rect = Resize(vChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize(double vChange)
        {
            Rect rect = new Rect();
            rect.Width = startRect.Width;
            rect.X = startRect.X;
            rect.Y = startRect.Y;
            var height = startRect.Height + vChange;
            rect.Height = Math.Max(1, height);
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false; border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();
        }
        private Point startPoint;
        private Rect startRect;
        private int direction;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
            direction = SizeDirectionBorderProperty.GetValue(border);
        }
    }
    public class DragResizeAction8Property : BaseAttachedProperty<DragResizeAction8Property, Action<Grid, Rect>>
    {
        private Border border;
        private Grid parentGrid;
        private Canvas parentCanvas;
        private bool isDraging = false;
        Action<Grid, Rect> changed;
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Border b))
            {
                return;
            }
            var name = b.Name;
            border = b;
            border.Loaded += OnBorderLoaded;
            changed = e.NewValue as Action<Grid, Rect>;
        }

        private void OnMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDraging)
            {
                var s = e as System.Windows.Input.MouseEventArgs;
                if (s != null)
                {
                    var p = s.GetPosition(parentCanvas);
                    double hChange = p.X - startPoint.X;
                    double vChange = p.Y - startPoint.Y;
                    var rect = Resize(hChange, vChange);
                    changed?.Invoke(parentGrid, rect);
                }
            }
        }
        private Rect Resize(double hChange, double vChange)
        {
            Rect rect = new Rect();
            rect.X = startRect.X;
            rect.Y = startRect.Y;
            var width = startRect.Width + hChange;
            rect.Width = Math.Max(1, width);
            var height = startRect.Height + vChange;
            rect.Height = Math.Max(1, height);
            return rect;
        }
        private void OnMouseUp(object sender, RoutedEventArgs e)
        {
            isDraging = false;
            border.ReleaseMouseCapture();
        }

        private void OnMouseDown(object sender, RoutedEventArgs e)
        {
            var s = e as System.Windows.Input.MouseButtonEventArgs;
            if (s != null)
            {
                isDraging = true;
                startPoint = s.GetPosition(parentCanvas);
                var startX = Canvas.GetLeft(parentGrid);
                var startY = Canvas.GetTop(parentGrid);
                var width = parentGrid.ActualWidth;
                var height = parentGrid.ActualHeight;
                startRect = new Rect(startX, startY, width, height);
            }
            border.CaptureMouse();
        }
        private Point startPoint;
        private Rect startRect;
        private int direction;
        private void OnBorderLoaded(object sender, RoutedEventArgs e)
        {
            border.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(OnMouseDown), true);
            border.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(OnMouseUp), true);
            border.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(OnMouseMove), true);
            parentGrid = border.Parent as Grid;
            parentCanvas = parentGrid.Parent as Canvas;
            direction = SizeDirectionBorderProperty.GetValue(border);
        }
    }
}
