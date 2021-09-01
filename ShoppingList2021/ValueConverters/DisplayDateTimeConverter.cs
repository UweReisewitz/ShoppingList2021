using System;
using System.Globalization;
using Xamarin.Forms;

namespace ShoppingList2021.ValueConverters
{
    internal class DisplayDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nulltext = "";

            return value is null || !(value is DateTime datetime) || datetime == DateTime.MinValue
                ? nulltext
                : datetime.ToString("g", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
