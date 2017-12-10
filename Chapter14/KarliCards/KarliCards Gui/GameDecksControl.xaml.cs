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
using Ch13CardLib;

namespace KarliCards_Gui
{
    /// <summary>
    /// Interaction logic for GameDecksControl.xaml
    /// </summary>
    public partial class GameDecksControl : UserControl
    {
        public GameDecksControl()
        {
            InitializeComponent();
        }

        public bool GameStarted
        {
            get { return (bool)GetValue(GameStartedProperty); }
            set { SetValue(GameStartedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GameStarted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameStartedProperty =
            DependencyProperty.Register("GameStarted", typeof(bool), typeof(GameDecksControl), 
                new PropertyMetadata(false, new PropertyChangedCallback(OnGameStarted)));

        public Player CurrentPlayer
        {
            get { return (Player)GetValue(CurrentPlayerProperty); }
            set { SetValue(CurrentPlayerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPlayer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPlayerProperty =
            DependencyProperty.Register("CurrentPlayer", typeof(Player), typeof(GameDecksControl), 
                new PropertyMetadata(null, new PropertyChangedCallback(OnPlayerChanged)));

        public Deck Deck
        {
            get { return (Deck)GetValue(DeckProperty); }
            set { SetValue(DeckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Deck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeckProperty =
            DependencyProperty.Register("Deck", typeof(Deck), typeof(GameDecksControl), 
                new PropertyMetadata(null, new PropertyChangedCallback(OnDeckChanged)));

        public Card AvailableCard
        {
            get { return (Card)GetValue(AvailableCardProperty); }
            set { SetValue(AvailableCardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AvailableCard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AvailableCardProperty =
            DependencyProperty.Register("AvailableCard", typeof(Card), typeof(GameDecksControl), 
                new PropertyMetadata(null, new PropertyChangedCallback(OnAvailableCardChanged)));

        private static void OnGameStarted(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as GameDecksControl;
            control.DrawDecks();
        }

        private static void OnPlayerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as GameDecksControl;

            if (control.CurrentPlayer == null)
                return;

            control.CurrentPlayer.OnCardDiscarded += control.CurrentPlayer_OnCardDiscarded;
            control.DrawDecks();
        }

        private void CurrentPlayer_OnCardDiscarded(object sender, CardEventArgs e)
        {
            AvailableCard = e.Card;
            DrawDecks();
        }

        private static void OnDeckChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as GameDecksControl;
            control.DrawDecks();
        }

        private static void OnAvailableCardChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as GameDecksControl;
            control.DrawDecks();
        }

        /// <summary>
        /// Repaints the game decks.
        /// </summary>
        private void DrawDecks()
        {
            controlCanvas.Children.Clear();

            // Don't do anything if nothing has happened yet.
            if (CurrentPlayer == null || Deck == null || !GameStarted)
                return;

            // Build a stack of card controls for all the cards in the deck.
            List<CardControl> stackedCards = new List<CardControl>();
            for (int i = 0; i < Deck.CardsInDeck; i++)
                stackedCards.Add(
                    new CardControl(Deck.GetCard(i))
                    {
                        Margin = new Thickness(150 + (i * 1.25), 25 - (i * 1.25), 0, 0),
                        IsFaceUp = false
                    });

            // If the deck isn't empty, add a double-click event to the top card to make it clickable.
            if (stackedCards.Count > 0)
                stackedCards.Last().MouseDoubleClick += Deck_MouseDoubleClick;

            if (AvailableCard != null)
            {
                var availableCard = new CardControl(AvailableCard)
                {
                    Margin = new Thickness(0, 25, 0, 0)
                };

                // Add a double-click event to the available card.
                availableCard.MouseDoubleClick += AvailableCard_MouseDoubleClick;
                controlCanvas.Children.Add(availableCard);
            }

            stackedCards.ForEach(x => controlCanvas.Children.Add(x));
        }

        /// <summary>
        /// Handles the double-click event for the discard pile.
        /// </summary>
        /// <param name="sender">The card clicked on the discard pile.</param>
        /// <param name="e"></param>
        private void AvailableCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Don't do anything if the current player isn't active.
            if (CurrentPlayer.State != PlayerState.Active)
                return;

            var control = sender as CardControl;

            // Add the available card to the player's hand.
            CurrentPlayer.AddCard(control.Card);
            AvailableCard = null;
            DrawDecks();
        }

        /// <summary>
        /// Handles the double-click event for the draw pile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deck_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Don't do anything if the current player isn't active.
            if (CurrentPlayer.State != PlayerState.Active)
                return;

            // Take the top card from the deck and add it to the player's hand.
            CurrentPlayer.DrawCard(Deck);
            DrawDecks();
        }
    }
}
