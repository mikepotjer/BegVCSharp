using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Ch13CardLib;

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
            // The GameOptions class has a static method that handles loading the options and
            // creating an instance of itself.
            _gameOptions = GameOptions.Create();

            // This allows controls to bind to an instance of this object just by specifying the
            // property to use in the binding.
            DataContext = _gameOptions;

            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            // The GameOptions class can save its own settings.
            _gameOptions.Save();

            // Set the return value for the window, and close it.
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
