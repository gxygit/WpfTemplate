using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//BaseAttachedProperty 使用示例
namespace ViewModel.Base
{
    /// <summary>
    /// create a clipping region from the parent <see cref="Border"/> corner raidus
    /// </summary>
    public class ClipFromBorderProperty : BaseAttachedProperty<ClipFromBorderProperty, bool>
    {
        #region private members
        /// <summary>
        /// called when the parent border first loads
        /// </summary>
        private RoutedEventHandler mBorder_Loaded;
        /// <summary>
        /// called when then border size changed
        /// </summary>
        private SizeChangedEventHandler mBorder_SizeChanged;
        #endregion

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var self = (sender as FrameworkElement);
            if (self == null) return;
            //check we have a parent border
            if (!(self.Parent is Border))
            {
                Debugger.Break();
                return;
            }
            mBorder_Loaded = (ss, ee) => Border_OnChange(ss, ee, self);

            mBorder_SizeChanged = (ss, ee) => Border_OnChange(ss, ee, self);

            Border border = self.Parent as Border;
            //if true hook into events
            if ((bool)e.NewValue)
            {
                border.Loaded += mBorder_Loaded;
                border.SizeChanged += mBorder_SizeChanged;

            }
            else
            {
                //otherwise unhook
                border.Loaded -= mBorder_Loaded;
                border.SizeChanged -= mBorder_SizeChanged;
            }

        }

        /// <summary>
        /// called when the border is loaded and sizechanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_OnChange(object sender, RoutedEventArgs e, FrameworkElement child)
        {
            var border = (Border)sender;

            //check we have an actual size
            if (border.ActualWidth == 0 && border.ActualHeight == 0)
                return;
            //setup the new child clipping area
            var rect = new RectangleGeometry();
            //match the corner radius with the borders corner radius
            rect.RadiusX = rect.RadiusY = Math.Max(0, border.CornerRadius.TopLeft - (border.BorderThickness.Left * 0.5));
            //set rectangle size to match child's actual sieze
            rect.Rect = new Rect(child.RenderSize);
            //assign clipping area to the child 
            child.Clip = rect;
        }
    }



}
