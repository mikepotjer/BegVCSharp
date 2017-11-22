using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace KarliCards_Gui
{
    /// <summary>
    /// The Rank is to be displayed as mix of names and numbers, so a ValueConverter is defined to
    /// handle this.
    /// </summary>
    [ValueConversion(typeof(Ch13CardLib.Rank), typeof(string))]
    public class RankNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int source = (int)value;
            if (source == 1 || source > 10)
            {
                // For the values for Ace, Jack, Queen, and King, we want to display the name.
                switch (source)
                {
                    case 1:
                        return "Ace";
                    case 11:
                        return "Jack";
                    case 12:
                        return "Queen";
                    case 13:
                        return "King";
                    default:
                        return DependencyProperty.UnsetValue;
                }
            }
            else
                // For the rest of the Rank values, we want to display the digit (2, 3, 4, etc.)
                return source.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
