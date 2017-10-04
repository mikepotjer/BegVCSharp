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
                        // Read the XML from the file, and generate a GameOptions instance
                        // from it.
                        var serializer = new XmlSerializer(typeof(GameOptions));
                        _gameOptions = serializer.Deserialize(stream) as GameOptions;
                    }
                }
                else
                    // The options file doesn't exist, so we need to load a new options class.
                    _gameOptions = new GameOptions();
            }

            // This allows controls to bind to an instance of this object just by specifying the
            // property to use in the binding.
            DataContext = _gameOptions;

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

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the options file if it doesn't exist, or overwrite it if it does.
            using (var stream = File.Open("GameOptions.xml", FileMode.Create))
            {
                // Generate XML from the GameOptions instance, writing it to the file.
                var serializer = new XmlSerializer(typeof(GameOptions));
                serializer.Serialize(stream, _gameOptions);
            }
            // Close the window.
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the game settings and close the window.
            _gameOptions = null;
            Close();
        }
    }
}
