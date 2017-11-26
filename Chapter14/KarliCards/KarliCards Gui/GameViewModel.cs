using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ch13CardLib;

namespace KarliCards_Gui
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Define a field and public property to store a reference to the current player, and
        // fire an event when the player changes.
        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        // Define a field and public property to store a reference to the collection of all
        // players for the current game, and fire an event when that list changes.
        private List<Player> _players;
        public List<Player> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

        // Define a field and public property to store a reference to available card on the
        // discard pile, and fire an event when that card changes.
        private Card _availableCard;
        public Card CurrentAvailableCard
        {
            get { return _availableCard; }
            set
            {
                _availableCard = value;
                OnPropertyChanged(nameof(CurrentAvailableCard));
            }
        }

        // Define a field and public property to store a reference to the deck of cards
        // for the current game, and fire an event when that deck changes.
        private Deck _deck;
        public Deck GameDeck
        {
            get { return _deck; }
            set
            {
                _deck = value;
                OnPropertyChanged(nameof(GameDeck));
            }
        }

        // Define a field and public property to store a flag indicating whether the
        // current game has started, and fire an event when that flag changes.
        private bool _gameStarted;
        public bool GameStarted
        {
            get { return _gameStarted; }
            set
            {
                _gameStarted = value;
                OnPropertyChanged(nameof(GameStarted));
            }
        }

        private GameOptions _gameOptions;

        /// <summary>
        /// A default constructor to initialize a couple of the fields.
        /// </summary>
        public GameViewModel()
        {
            _players = new List<Player>();
            _gameOptions = GameOptions.Create();
        }

        public void StartNewGame()
        {
            // If no players are selected, or there's only one player and we're not playing against
            // the computer, then don't allow the game to start.
            if (_gameOptions.SelectedPlayers.Count < 1
                    || (_gameOptions.SelectedPlayers.Count == 1 && !_gameOptions.PlayAgainstComputer))
                return;

            CreateGameDeck();
            CreatePlayers();
            InitializeGame();
            GameStarted = true;
        }

        /// <summary>
        /// Sets up the deck of cards for the current game.
        /// </summary>
        private void CreateGameDeck()
        {
            GameDeck = new Deck();
            GameDeck.Shuffle();
        }

        /// <summary>
        /// Adds the players to the current game.
        /// </summary>
        private void CreatePlayers()
        {
            Players.Clear();

            for (var i = 0; i < _gameOptions.NumberOfPlayers; i++)
            {
                // Add all selected players to the game. If there aren't enough selected players, add
                // computer players for the rest.
                if (i < _gameOptions.SelectedPlayers.Count)
                    InitializePlayer(new Player { Index = i, PlayerName = _gameOptions.SelectedPlayers[i] });
                else
                    InitializePlayer(new ComputerPlayer { Index = i, Skill = _gameOptions.ComputerSkill });
            }
        }

        /// <summary>
        /// Sets up a new game.
        /// </summary>
        private void InitializeGame()
        {
            // Set the first player as the current player, then turn over a card onto the discard pile.
            AssignCurrentPlayer(0);
            CurrentAvailableCard = GameDeck.Draw();
        }

        /// <summary>
        /// Sets the current player in this game.
        /// </summary>
        /// <param name="index">The index of the current player in the Players collection.</param>
        private void AssignCurrentPlayer(int index)
        {
            CurrentPlayer = Players[index];

            // If no player has won yet, set the current player state to Active, and set all
            // other players to Inactive.
            if (!Players.Any(x => x.State == PlayerState.Winner))
                Players.ForEach(x => x.State = (x == Players[index] ? PlayerState.Active : PlayerState.Inactive));
        }

        /// <summary>
        /// Initializes the specified player, adding him to the current game.
        /// </summary>
        /// <param name="player">A Player instance to add to the game.</param>
        private void InitializePlayer(Player player)
        {
            // Deal a hand to the player.
            player.DrawNewHand(GameDeck);

            // Bind player events to event handlers in this class.
            player.OnCardDiscarded += player_OnCardDiscarded;
            player.OnPlayerHasWon += player_OnPlayerHasWon;

            Players.Add(player);
        }

        private void player_OnPlayerHasWon(object sender, PlayerEventArgs e)
        {
            // Change the state of all the players. Set the specified player as the winner, and
            // all the others as losers.
            Players.ForEach(x => x.State = (x == e.Player ? PlayerState.Winner : PlayerState.Loser));
        }

        private void player_OnCardDiscarded(object sender, CardEventArgs e)
        {
            // Update the discard pile by making the specified card the available card.
            CurrentAvailableCard = e.Card;

            // Determine the index of the next player. If the current player is the last player,
            // then the first player is the next player.
            var nextIndex = CurrentPlayer.Index + 1 >= _gameOptions.NumberOfPlayers ? 0 : CurrentPlayer.Index + 1;

            // If the game deck is empty, the discarded cards need to be reshuffled.
            if (GameDeck.CardsInDeck == 0)
            {
                // Get a list of all the cards that are in play. This includes all the cards in
                // players' hands, and the top card on the discard pile.
                var cardsInPlay = new List<Card>();
                foreach (var player in Players)
                    cardsInPlay.AddRange(player.GetCards());
                cardsInPlay.Add(CurrentAvailableCard);

                // Reshuffle all the cards *except* for the list of cards that are in play.
                GameDeck.ReshuffleDiscarded(cardsInPlay);
            }

            // Finally, set the next player.
            AssignCurrentPlayer(nextIndex);
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Setup routed commands to link to the GameClient UI. Set a shortcut key for the new game item.
        public static RoutedCommand StartGameCommand = new RoutedCommand("Start New Game", typeof(GameViewModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.N, ModifierKeys.Control) }));

        public static RoutedCommand ShowAboutCommand = new RoutedCommand("Show About Dialog", typeof(GameViewModel));
    }
}
