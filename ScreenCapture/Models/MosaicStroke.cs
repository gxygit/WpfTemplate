using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScreenCapture
{
    public class MosaicStroke : Stroke
    {
        
        public MosaicStroke(StylusPointCollection stylusPoints) : base(stylusPoints)
        {
            
        }
        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            //base.DrawCore(drawingContext, drawingAttributes);

            SolidColorBrush solidColorBrush2 = new SolidColorBrush(Colors.Transparent);
            solidColorBrush2.Freeze();
            drawingContext.DrawGeometry(solidColorBrush2, null, GetGeometry(drawingAttributes));
        }
        
        
    }
}
