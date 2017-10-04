using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace KarliCards_Gui
{
    /// <summary>
    /// Interaction logic for StartGame.xaml
    /// </summary>
    public partial class StartGame : Window
    {
        private GameOptions _gameOptions;

        public StartGame()
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

            if (_gameOptions.PlayAgainstComputer)
                playerNamesListBox.SelectionMode = SelectionMode.Single;
            else
                playerNamesListBox.SelectionMode = SelectionMode.Extended;
        }

        private void playerNamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Only allow one user name when playing against the computer, otherwise there must
            // be as many names entered as the number of players selected. Don't allow the user
            // to continue unless one of those is true.
            if (_gameOptions.PlayAgainstComputer)
                okButton.IsEnabled = (playerNamesListBox.SelectedItems.Count == 1);
            else
                okButton.IsEnabled = (playerNamesListBox.SelectedItems.Count == _gameOptions.NumberOfPlayers);
        }
    }
}
