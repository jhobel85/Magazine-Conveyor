using System;
using System.Globalization;
using System.Windows.Data;

namespace Magazine_WPF.View
{
    /// <summary>
    /// Converts position count to a diameter size for circular display.
    /// Calculates responsive size based on number of items.
    /// </summary>
    public class PositionCountToSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                // Much larger base size with aggressive growth
                // Minimum 400, grows significantly with count
                double size = 400 + (count * 20);
                
                // Very high cap for maximum visibility
                return Math.Min(size, 1200);
            }

            return 500;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
