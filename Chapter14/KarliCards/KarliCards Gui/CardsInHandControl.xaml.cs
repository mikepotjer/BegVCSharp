using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Ch13CardLib;

namespace KarliCards_Gui
{
    /// <summary>
    /// Interaction logic for CardsInHandControl.xaml
    /// </summary>
    public partial class CardsInHandControl : UserControl
    {
        public CardsInHandControl()
        {
            InitializeComponent();
        }

        public Player Owner
        {
            get { return (Player)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register(
            "Owner", 
            typeof(Player), 
            typeof(CardsInHandControl), 
            new PropertyMetadata(null, new PropertyChangedCallback(OnOwnerChanged)));

        public GameViewModel Game
        {
            get { return (GameViewModel)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Game.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameProperty = DependencyProperty.Register(
            "Game", 
            typeof(GameViewModel), 
            typeof(CardsInHandControl), 
            new PropertyMetadata(null));


        public PlayerState PlayerState
        {
            get { return (PlayerState)GetValue(PlayerStateProperty); }
            set { SetValue(PlayerStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerStateProperty = DependencyProperty.Register(
            "PlayerState", 
            typeof(PlayerState), 
            typeof(CardsInHandControl), 
            new PropertyMetadata(PlayerState.Inactive, new PropertyChangedCallback(OnPlayerStateChanged)));

        public Orientation PlayerOrientation
        {
            get { return (Orientation)GetValue(PlayerOrientationProperty); }
            set { SetValue(PlayerOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerOrientationProperty = DependencyProperty.Register(
            "PlayerOrientation", 
            typeof(Orientation), 
            typeof(CardsInHandControl), 
            new PropertyMetadata(Orientation.Horizontal, new PropertyChangedCallback(OnPlayerOrientationChanged)));

        public bool ComputerPlaysWithOpenHand
        {
            get { return (bool)GetValue(ComputerPlaysWithOpenHandProperty); }
            set { SetValue(ComputerPlaysWithOpenHandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComputerPlaysWithOpenHand. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComputerPlaysWithOpenHandProperty = DependencyProperty.Register(
            "ComputerPlaysWithOpenHand",
            typeof(bool),
            typeof(CardsInHandControl),
            new PropertyMetadata(false));

        private static void OnOwnerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as CardsInHandControl;
            control.RedrawCards();
        }

        /// <summary>
        /// Handles redrawing the cards when the state of a player changes, but is also used to
        /// control some computer player actions.
        /// </summary>
        /// <param name="source">A CardsInHandControl object.</param>
        /// <param name="e"></param>
        private static void OnPlayerStateChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as CardsInHandControl;

            // Check if this is a computer player, and if so, we need to initiate some actions
            // for this player.
            var computerPlayer = control.Owner as ComputerPlayer;
            if (computerPlayer != null)
            {
                if (computerPlayer.State == PlayerState.MustDiscard)
                {
                    // The computer player needs to discard, so initiate that action, specifying
                    // a delegate method to execute on a separate thread.
                    Thread delayedWorker = new Thread(control.DelayDiscard);
                    delayedWorker.Start(new Payload
                    {
                        Deck = control.Game.GameDeck,
                        AvailableCard = control.Game.CurrentAvailableCard,
                        Player = computerPlayer
                    });
                }
                else if (computerPlayer.State == PlayerState.Active)
                {
                    // The computer player needs to draw a card, so initiate that action.
                    Thread delayedWorker = new Thread(control.DelayDraw);
                    delayedWorker.Start(new Payload
                    {
                        Deck = control.Game.GameDeck,
                        AvailableCard = control.Game.CurrentAvailableCard,
                        Player = computerPlayer
                    });
                }
            }
            control.RedrawCards();
        }

        private static void OnPlayerOrientationChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var control = source as CardsInHandControl;
            control.RedrawCards();
        }

        /// <summary>
        /// Class used to pass data to a worker thread.
        /// </summary>
        private class Payload
        {
            public Deck Deck { get; set; }
            public Card AvailableCard { get; set; }
            public ComputerPlayer Player { get; set; }
        }

        /// <summary>
        /// Draws a card for the computer player.
        /// </summary>
        /// <param name="payload"></param>
        private void DelayDraw(object payload)
        {
            Thread.Sleep(1250);
            var data = payload as Payload;
            // Execute the ComputerPlayer's PerformDraw() method, which takes 2 parameters.
            // Using the Dispatcher ensures the calls are made on the GUI thread.
            Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action<Deck, Card>(data.Player.PerformDraw),
                data.Deck,
                data.AvailableCard);
        }

        /// <summary>
        /// Discards a card for the computer player.
        /// </summary>
        /// <param name="payload"></param>
        private void DelayDiscard(object payload)
        {
            Thread.Sleep(1250);
            var data = payload as Payload;
            Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action<Deck>(data.Player.PerformDiscard),
                data.Deck);
        }

        /// <summary>
        /// Re-paints the cards for this hand.
        /// </summary>
        private void RedrawCards()
        {
            // Reset the contents of the control.
            CardSurface.Children.Clear();

            // If there is no player for this hand, make sure the name is cleared and exit.
            if (Owner == null)
            {
                PlayerNameLabel.Content = string.Empty;
                return;
            }
            DrawPlayerName();
            DrawCards();
        }

        /// <summary>
        /// Draws (or paints) the cards in the current player's hand.
        /// </summary>
        private void DrawCards()
        {
            // If the player isn't active, the cards will not be face up.
            bool isFaceup = (Owner.State != PlayerState.Inactive);

            // Display the computer player's hand when the game ends, or if the computer is
            // playing with an open hand.
            if (Owner is ComputerPlayer)
                isFaceup = (Owner.State == PlayerState.Loser
                    || Owner.State == PlayerState.Winner
                    || ComputerPlaysWithOpenHand);
                    // This condition will work, too, if the XAML binding isn't setup.
                    //|| Game == null ? true : Game.ComputerPlaysWithOpenHand);

            var cards = Owner.GetCards();

            // If the player doesn't have any cards, there's nothing to do.
            if (cards == null || cards.Count == 0)
                return;

            for (var i = 0; i < cards.Count; i++)
            {
                var cardControl = new CardControl(cards[i]);

                if (PlayerOrientation == Orientation.Horizontal)
                    cardControl.Margin = new Thickness(i * 35, 35, 0, 0);
                else
                    cardControl.Margin = new Thickness(5, 35 + i * 30, 0, 0);

                // Connect a double-click event handler to each card, and orient the cards face-up
                // or face-down depending on the setting determined above.
                cardControl.MouseDoubleClick += cardControl_MouseDoubleClick;
                cardControl.IsFaceUp = isFaceup;
                CardSurface.Children.Add(cardControl);
            }
        }

        /// <summary>
        /// Draws the player's name on the control.
        /// </summary>
        private void DrawPlayerName()
        {
            if (Owner.State == PlayerState.Winner || Owner.State == PlayerState.Loser)
                PlayerNameLabel.Content = Owner.PlayerName + (Owner.State == PlayerState.Winner ? " is the WINNER" : " has LOST");
            else
                PlayerNameLabel.Content = Owner.PlayerName;

            var isActivePlayer = (Owner.State == PlayerState.Active || Owner.State == PlayerState.MustDiscard);

            // Change the font size and color for the active player.
            PlayerNameLabel.FontSize = isActivePlayer ? 18 : 14;
            PlayerNameLabel.Foreground = isActivePlayer ? new SolidColorBrush(Colors.Gold) : new SolidColorBrush(Colors.White);
        }

        private void cardControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCard = sender as CardControl;

            // If there's no player for this hand, do nothing.
            if (Owner == null)
                return;

            // If this player has to discard, then discard the card that was clicked.
            if (Owner.State == PlayerState.MustDiscard)
                Owner.DiscardCard(selectedCard.Card);

            // Re-paint the hand after the discard.
            RedrawCards();
        }
    }
}
