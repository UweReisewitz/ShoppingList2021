using System;
using System.Globalization;
using Xamarin.Forms;

namespace ShoppingList2021.ValueConverters
{
    public class AutoSuggestBoxTextChangedEventArgsToPerformUpdateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs;
            return eventArgs.Reason == dotMorten.Xamarin.Forms.AutoSuggestionBoxTextChangeReason.UserInput;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
