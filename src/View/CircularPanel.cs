using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Magazine_WPF.View
{
    public class CircularPanel : Panel
    {
        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            foreach (UIElement child in Children)
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            return base.MeasureOverride(availableSize);
        }

        // Arrange stuff in a circle
        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {            
            if (Children.Count > 0)
            {
                // Center & radius of panel
                Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);
                double radius = Math.Min(finalSize.Width * 2, finalSize.Height) / 2.0;
                radius *= 0.8;   // To avoid hitting edges

                // # radians between children
                double angleIncrRadians = 2.0 * Math.PI / Children.Count;

                double angleInRadians = 0.0;

                foreach (UIElement child in Children)
                {
                    Point childPosition = new Point(
                        radius * Math.Cos(angleInRadians) + center.X,
                        radius * Math.Sin(angleInRadians) + center.Y);

                    child.Arrange(new Rect(childPosition, child.DesiredSize));

                    angleInRadians += angleIncrRadians;
                }
            }

            return finalSize;
        }
    }
}
