using System;
using System.Globalization;
using System.Windows.Data;

namespace Magazine_WPF.View
{
    /// <summary>
    /// Converts position count to column count with a maximum of 50 columns.
    /// </summary>
    public class MaxColumnsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                // Maximum 50 columns per row
                return Math.Min(count, 50);
            }

            return 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
