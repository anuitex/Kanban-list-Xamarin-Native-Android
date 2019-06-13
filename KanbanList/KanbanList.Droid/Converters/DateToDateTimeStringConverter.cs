using System;
using MvvmCross.Converters;

namespace KanbanList.Droid.Converters
{
    public class DateToDateTimeStringConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime? date = value as DateTime?;

            if (date == null)
            {
                return string.Empty;
            }

            DateTime nowDateTime = DateTime.Now;

            if (date.Value.DayOfYear != nowDateTime.DayOfYear && date.Value.Year != nowDateTime.Year)
            {
                return value.ToShortDateString();
            }

            return value.ToShortTimeString();
        }

        protected override DateTime ConvertBack(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateTime.UtcNow;
        }
    }
}