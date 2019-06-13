using System;
using System.Globalization;
using System.IO;
using Android.Graphics;
using MvvmCross.Converters;

namespace KanbanList.Droid.Converters
{
    public class BytesToBitmapValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null && value is byte[])
            {
                return null;
            }
            byte[] _value = value as byte[];

            return BitmapFactory.DecodeByteArray(_value, 0, _value.Length);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            byte[] bitmapData = null;
            Bitmap bitmap = value as Bitmap;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
            return bitmapData;
        }
    }
}