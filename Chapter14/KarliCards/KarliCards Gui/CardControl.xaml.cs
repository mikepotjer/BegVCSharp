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

namespace KarliCards_Gui
{
    /// <summary>
    /// Interaction logic for CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        public CardControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This constructor allows us to initialize the appearance of the CardControl based on
        /// a Card class.
        /// </summary>
        /// <param name="card">A Card class</param>
        public CardControl(Ch13CardLib.Card card)
        {
            InitializeComponent();
            Card = card;
        }

        // This method can be private, because the OnSuitChanged() method that calls it is a
        // member of the same class.
        private void SetTextColor()
        {
            // When a Club or Spade is selected, set the text color of the 3 labels to black,
            // otherwise set the color to red. This method is triggered by OnSuitChanged.
            var color = (Suit == Ch13CardLib.Suit.Club || Suit == Ch13CardLib.Suit.Spade)
                ? new SolidColorBrush(Color.FromRgb(0, 0, 0)) : new SolidColorBrush(Color.FromRgb(255, 0, 0));
            RankLabel.Foreground = SuitLabel.Foreground = RankLabelInverted.Foreground = color;
        }

        // 3 dependency properties
        // This registers 3 properties of this class which are bound to the CardControl.
        // We set the property name, its type, its owner (the CardControl), and meta data such as the
        // default value, and a callback method for the Suit and IsFaceUp properties when they change.
        public static DependencyProperty SuitProperty = DependencyProperty.Register(
            "Suit",
            typeof(Ch13CardLib.Suit),
            typeof(CardControl),
            new PropertyMetadata(Ch13CardLib.Suit.Club, new PropertyChangedCallback(OnSuitChanged)));

        public static DependencyProperty RankProperty = DependencyProperty.Register(
            "Rank",
            typeof(Ch13CardLib.Rank),
            typeof(CardControl),
            new PropertyMetadata(Ch13CardLib.Rank.Ace));

        public static DependencyProperty IsFaceUpProperty = DependencyProperty.Register(
            "IsFaceUp",
            typeof(bool),
            typeof(CardControl),
            new PropertyMetadata(true, new PropertyChangedCallback(OnIsFaceUpChanged)));

        // 3 public properties for accessing the dependency properties
        public bool IsFaceUp
        {
            get { return (bool)GetValue(IsFaceUpProperty); }
            set { SetValue(IsFaceUpProperty, value); }
        }

        public Ch13CardLib.Suit Suit
        {
            get { return (Ch13CardLib.Suit)GetValue(SuitProperty); }
            set { SetValue(SuitProperty, value); }
        }

        public Ch13CardLib.Rank Rank
        {
            get { return (Ch13CardLib.Rank)GetValue(RankProperty); }
            set { SetValue(RankProperty, value); }
        }

        // Change event handlers
        public static void OnSuitChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            // The text color of the control needs to be set whenever the Suit changes.
            // Since this event handler is a static method, we want to call an instance
            // method of the passed control to set the color.
            var control = source as CardControl;
            control.SetTextColor();
        }

        private static void OnIsFaceUpChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            // The visibility of the images and labels in the control need to be set
            // whenever IsFaceUp changes.
            var control = source as CardControl;
            control.RankLabel.Visibility = control.SuitLabel.Visibility 
                = control.RankLabelInverted.Visibility 
                = control.TopRightImage.Visibility 
                = control.BottomLeftImage.Visibility 
                = control.IsFaceUp
                ? Visibility.Visible : Visibility.Hidden;
        }

        // Add a field and property for storing a reference to the Card class for the
        // current instance of the control.
        private Ch13CardLib.Card _card;
        public Ch13CardLib.Card Card
        {
            get { return _card; }
            // The suit and rank of the Card class will be copied to the corresponding
            // properties of this control.
            private set { _card = value; Suit = _card.suit; Rank = _card.rank; }
        }
    }
}
