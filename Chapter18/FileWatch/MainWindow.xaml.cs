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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace FileWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // File system watcher object.
        private FileSystemWatcher watcher;
        
        /// <summary>
        /// Adds a message to the top of the item collection for the WatchOutput list box.
        /// </summary>
        /// <param name="message">A message to display in the list box.</param>
        private void AddMessage(string message)
        {
            // Asynchronously adds a message to the list box.
            Dispatcher.BeginInvoke(new Action(() => WatchOutput.Items.Insert(0, message)));
        }

        public MainWindow()
        {
            InitializeComponent();

            // Setup a watcher and attach event handlers to its various events. Lambda expressions
            // are used to create anonymous event handlers to simply report the file changes.
            watcher = new FileSystemWatcher();
            watcher.Deleted += (s, e) => AddMessage($"File: {e.FullPath} Deleted");
            watcher.Renamed += (s, e) => AddMessage($"File renamed from {e.OldName} to {e.FullPath}");
            watcher.Changed += (s, e) => AddMessage($"File: {e.FullPath} {e.ChangeType.ToString()}");
            watcher.Created += (s, e) => AddMessage($"File: {e.FullPath} Created");
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Display an open file dialog for the user to selected a file.
            OpenFileDialog dialog = new OpenFileDialog();

            // Make sure the user clicked OK (and not Cancel).
            if (dialog.ShowDialog(this) == true)
            {
                LocationBox.Text = dialog.FileName;
            }
        }

        private void LocationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Enable the Watch button when a file name is entered.
            WatchButton.IsEnabled = !string.IsNullOrEmpty(LocationBox.Text);
        }

        private void WatchButton_Click(object sender, RoutedEventArgs e)
        {
            // Setup the watcher to monitor certain changes on the selected file.
            watcher.Path = System.IO.Path.GetDirectoryName(LocationBox.Text);
            watcher.Filter = System.IO.Path.GetFileName(LocationBox.Text);
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;
            AddMessage($"Watching {LocationBox.Text}");

            // Activate the watcher.
            watcher.EnableRaisingEvents = true;

            // Enable the End Watch button once the watcher is enabled.
            EndWatchButton.IsEnabled = true;
        }

        private void EndWatchButton_Click(object sender, RoutedEventArgs e)
        {
            // Disable events on the watcher if it is currently enabled.
            if (watcher?.EnableRaisingEvents == true)
            {
                AddMessage($"Stopped watching {LocationBox.Text}");
                watcher.EnableRaisingEvents = false;
                EndWatchButton.IsEnabled = false;
            }
        }
    }
}
