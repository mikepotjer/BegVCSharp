using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KarliCards_Gui
{
    /// <summary>
    /// In order to bind together 2 properties that take different values, we can use a class
    /// that implements the IValueConverter interface. In this case, when one property is true,
    /// we want the other property to be false, and vice versa. But in other cases, we could
    /// implement something much more complex.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
