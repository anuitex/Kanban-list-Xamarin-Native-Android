using System;
using System.Globalization;
using MvvmCross.Converters;

namespace KanbanList.Droid.Converters
{
    public class StringToDoubleConverter : MvxValueConverter<double, string>
    {
        protected override string Convert(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == default(double) ? string.Empty : value.ToString();
        }

        protected override double ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value, out double resultValue))
            {
                return resultValue;
            }
            return default(double);
        }
    }
}