using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Magazine_WPF.View
{
    /// <summary>
    /// Converts position count to visibility based on a specified limit.
    /// Parameter should be the limit number (e.g., "500").
    /// </summary>
    public class ItemLimitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count && parameter is string paramStr)
            {
                // Parse the limit from parameter
                int limit = 500; // default
                if (int.TryParse(paramStr, out int parsedLimit))
                {
                    limit = parsedLimit;
                }

                return count <= limit ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Inverse of ItemLimitConverter - shows when over limit.
    /// </summary>
    public class ItemLimitExceededConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count && parameter is string paramStr)
            {
                int limit = 500;
                if (int.TryParse(paramStr, out int parsedLimit))
                {
                    limit = parsedLimit;
                }

                return count > limit ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
