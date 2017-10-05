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

namespace Ch14Ex99
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Initialize our data object, setting some default values.
        private FormData _formData = new FormData
        {
            MinValue = 1,
            MaxValue = 200,
            CurrentValue = 150
        };

        public MainWindow()
        {
            DataContext = _formData;

            InitializeComponent();
        }
    }
}
