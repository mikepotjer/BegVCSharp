using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KarliCards_Gui
{
    /// <summary>
    /// Interaction logic for GameClient.xaml
    /// </summary>
    public partial class GameClient : Window
    {
        public GameClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method used for the CanExecute part of the command bindings defined in the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Close)
                e.CanExecute = true;
            if (e.Command == ApplicationCommands.Save)
                e.CanExecute = false;
            if (e.Command == GameViewModel.StartGameCommand)
                e.CanExecute = true;
            if (e.Command == GameOptions.OptionsCommand)
                e.CanExecute = true;
            if (e.Command == GameViewModel.ShowAboutCommand)
                e.CanExecute = true;
            e.Handled = true;
        }

        /// <summary>
        /// Method used for the Executed part of the command bindings defined in the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Close)
                this.Close();
            if (e.Command == GameViewModel.StartGameCommand)
            {
                var model = new GameViewModel();

                // Instantiate the start game dialog.
                StartGame startGameDialog = new StartGame();

                // Load the game options, and set them as the data context for the start game window.
                var options = GameOptions.Create();
                startGameDialog.DataContext = options;

                // Display the start game dialog.
                var result = startGameDialog.ShowDialog();
                if (result.HasValue && result.Value == true)
                {
                    // The user clicked OK on the start game dialog, so save the options, tell the
                    // model to start a new game, and set the model as the data context for the game
                    // client window.
                    options.Save();
                    model.StartNewGame();
                    DataContext = model;
                }
            }
            if (e.Command == GameOptions.OptionsCommand)
            {
                // Instantiate and display the options window.
                var dialog = new Options();
                var result = dialog.ShowDialog();

                // If the user clicked OK in the options dialog, clear the current game.
                if (result.HasValue && result.Value == true)
                    DataContext = new GameViewModel();
            }
            if (e.Command == GameViewModel.ShowAboutCommand)
            {
                // Instantiate and display the about dialog.
                var dialog = new About();
                dialog.ShowDialog();
            }
            e.Handled = true;
        }
    }
}
