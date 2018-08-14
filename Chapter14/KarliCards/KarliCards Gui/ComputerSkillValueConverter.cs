using Ch13CardLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KarliCards_Gui
{
    /// <summary>
    /// This class converts a skill level to a logical value indicating whether the specified skill
    /// level is selected.
    /// </summary>
    [ValueConversion(typeof(ComputerSkillLevel), typeof(bool))]
    class ComputerSkillValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string helper = parameter as string;
            if (string.IsNullOrWhiteSpace(helper))
                return false;

            ComputerSkillLevel skillLevel = (ComputerSkillLevel)value;
            return (skillLevel.ToString() == helper);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return ComputerSkillLevel.Dumb;

            return Enum.Parse(targetType, parameterString);
        }
    }
}
