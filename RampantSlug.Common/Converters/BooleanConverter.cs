using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace RampantSlug.Common.Converters
{
    public class BooleanConverter<T> : IValueConverter
    {
        public T False { get; set; }
        public T True { get; set; }

        public BooleanConverter(T trueValue, T falseValue)
        {
            // Set the values of True and false to the values we were passed.
            True = trueValue;
            False = falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && ((bool)value) ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BooleanToBrushConverter : BooleanConverter<Brush>
    {
        /// <summary>
        /// Set us up so we convert true to Green <see cref="SolidColorBrush"/> and
        /// false to a Red SolidColorBrush.
        /// </summary>
        public BooleanToBrushConverter() :
            base(new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Red)) { }
    }

    /// <summary>
    /// Convert from Boolean value to Visibility value.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed) { }
    }


}