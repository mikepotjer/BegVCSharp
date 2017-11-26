using System.Collections.Generic;
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
            InitializeComponent();
            DataContextChanged += StartGame_DataContextChanged;
        }

        /// <summary>
        /// The event handler for a data context change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StartGame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _gameOptions = DataContext as GameOptions;
            ChangeListBoxOptions();
        }

        /// <summary>
        /// Sets the option that determines how players may be selected for the game.
        /// </summary>
        private void ChangeListBoxOptions()
        {
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
            var gameOptions = DataContext as GameOptions;
            gameOptions.SelectedPlayers = new List<string>();

            // Copy all the player names in the listbox to the players collection of the game
            // options class.
            foreach (string item in playerNamesListBox.SelectedItems)
            {
                gameOptions.SelectedPlayers.Add(item);
            }

            // Set the return value and close the window.
            DialogResult = true;
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
