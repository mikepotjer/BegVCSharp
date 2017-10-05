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

            // When playing against the computer, only allow one player to be selected, otherwise
            // allow multiples to be selected.
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

        private void addNewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a non-empty string has been entered in the textbox. If so, add it to the
            // list of player names.
            if (!string.IsNullOrWhiteSpace(newPlayerTextBox.Text))
                _gameOptions.AddPlayer(newPlayerTextBox.Text);
            newPlayerTextBox.Text = string.Empty;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Copy all the player names in the listbox to the players collection of the game
            // options class.
            foreach (string item in playerNamesListBox.SelectedItems)
            {
                _gameOptions.SelectPlayers.Add(item);
            }

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
