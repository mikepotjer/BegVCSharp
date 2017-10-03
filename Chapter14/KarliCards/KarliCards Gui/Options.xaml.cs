using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace KarliCards_Gui
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private GameOptions _gameOptions;

        public Options()
        {
            if (_gameOptions == null)
            {
                // No game options have been loaded yet, so check if an options file exists.
                if (File.Exists("GameOptions.xml"))
                {
                    // The options file exists, so read the settings from it.
                    using (var stream = File.OpenRead("GameOptions.xml"))
                    {
                        var serializer = new XmlSerializer(typeof(GameOptions));
                        _gameOptions = serializer.Deserialize(stream) as GameOptions;
                    }
                }
                else
                    // The options file doesn't exist, so we need to load a new options class.
                    _gameOptions = new GameOptions();
            }

            InitializeComponent();
        }

        private void dumbAIRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _gameOptions.ComputerSkill = ComputerSkillLevel.Dumb;
        }

        private void goodAIRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _gameOptions.ComputerSkill = ComputerSkillLevel.Good;
        }

        private void cheatingAIRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _gameOptions.ComputerSkill = ComputerSkillLevel.Cheats;
        }
    }
}
