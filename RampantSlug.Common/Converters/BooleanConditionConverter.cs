using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RampantSlug.Common.Converters
{
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

