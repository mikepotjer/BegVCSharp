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

namespace Ch08Ex01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender object as a button so we can access its properties.
            ((Button)sender).Content = "Clicked!";

            Button newButton = new Button();
            newButton.Content = "New Button! ";

            // The Margin property is of type Thickness, so create a Thickness object
            // to assign to it.
            newButton.Margin = new Thickness(10, 10, 200, 200);

            // Register the event handler newButton_Click() to the Click event of the
            // new button.
            newButton.Click += newButton_Click;

            // Find the parent of the existing button, cast it to the correct type of
            // Grid, then add the new button to the Children property of the grid.
            ((Grid)((Button)sender).Parent).Children.Add(newButton);
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = "Clicked!!";
        }
    }
}
