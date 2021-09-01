using System;
using System.Globalization;
using Xamarin.Forms;

namespace ShoppingList2021.ValueConverters
{
    public class DisplayGuidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nulltext = "";

            return value is null || !(value is Guid guid) || guid == Guid.Empty
                ? nulltext
                : guid.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
