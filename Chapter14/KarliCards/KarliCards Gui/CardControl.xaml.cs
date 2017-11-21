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

        public CardControl(Ch13CardLib.Card card)
        {
            InitializeComponent();
            Card = card;
        }

        private void SetTextColor()
        {
            var color = (Suit == Ch13CardLib.Suit.Club || Suit == Ch13CardLib.Suit.Spade)
                ? new SolidColorBrush(Color.FromRgb(0, 0, 0)) : new SolidColorBrush(Color.FromRgb(255, 0, 0));
            RankLabel.Foreground = SuitLabel.Foreground = RankLabelInverted.Foreground = color;
        }

        // 3 dependency properties
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
            var control = source as CardControl;
            control.SetTextColor();
        }

        private static void OnIsFaceUpChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var control = source as CardControl;
            control.RankLabel.Visibility = control.SuitLabel.Visibility 
                = control.RankLabelInverted.Visibility 
                = control.TopRightImage.Visibility 
                = control.BottomLeftImage.Visibility 
                = control.IsFaceUp
                ? Visibility.Visible : Visibility.Hidden;
        }

        private Ch13CardLib.Card _card;
        public Ch13CardLib.Card Card
        {
            get { return _card; }
            private set { _card = value; Suit = _card.suit; Rank = _card.rank; }
        }
    }
}
