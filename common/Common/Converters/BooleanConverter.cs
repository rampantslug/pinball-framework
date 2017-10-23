using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Common.Converters
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
            base(new SolidColorBrush(Colors.Green), new SolidColorBrush(Colors.Red))
        { }
    }

    /// <summary>
    /// Convert from Boolean value to Visibility value.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }

    /// <summary>
    /// Convert from Boolean value to Visibility value.
    /// </summary>
    public sealed class BooleanToVisibilityNoCollapsedConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityNoCollapsedConverter() :
            base(Visibility.Visible, Visibility.Hidden)
        { }
    }

    public sealed class InverseBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        { }
    }

    public class BoolToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value == true) ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value == 0) ? true : false;
        }
    }

    /// <summary>
    /// Converter that accepts a boolean and returns either the ConditionTrueValue or ConditionFalseValue property value
    /// of the parameter of type ConditionalValue, depending on whether the input value was true or false
    /// </summary>
    public class BooleanConditionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var conditionalValue = (parameter as ConditionalValue);
            Debug.Assert(conditionalValue != null, "conditionalValue != null");
            var returnValue = ((bool)value) ? conditionalValue.ConditionTrueValue : conditionalValue.ConditionFalseValue;
            return returnValue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ConditionalValue
    {

        public object ConditionTrueValue { get; set; }
        public object ConditionFalseValue { get; set; }
    }
}