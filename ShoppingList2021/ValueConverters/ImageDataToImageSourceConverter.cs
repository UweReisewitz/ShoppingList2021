using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace ShoppingList2021.ValueConverters
{
    public class ImageDataToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value is byte[] imageData
                ? ImageSource.FromStream(() => new MemoryStream(imageData))
                : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
