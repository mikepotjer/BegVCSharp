using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ch13CardLib
{
    [Serializable]
    public class Player : INotifyPropertyChanged
    {
        public int Index { get; set; }
        protected Cards Hand { get; set; }
        private string _name;
        private PlayerState _state;

        // Define an event method to call when the player discards a card.
        public event EventHandler<CardEventArgs> OnCardDiscarded;

        // Define an event method to call when the player has won the current game.
        public event EventHandler<PlayerEventArgs> OnPlayerHasWon;

        // A property to track the player's state, and generate an event whenever the player's
        // state changes.
        public PlayerState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public virtual string PlayerName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }

        /// <summary>
        /// Adds a card to the player's hand.
        /// </summary>
        /// <param name="card">A reference to a card to be added to the player's hand.</param>
        public void AddCard(Card card)
        {
            Hand.Add(card);

            // If the player has more cards than is allowed in a hand, change the player state
            // to indicate that the player must remove a card from the hand.
            if (Hand.Count > 7)
                State = PlayerState.MustDiscard;
        }

        /// <summary>
        /// Draws a card from the current deck, adding it to the player's hand.
        /// </summary>
        /// <param name="deck">A reference to the deck in use for the game.</param>
        public void DrawCard(Deck deck)
        {
            AddCard(deck.Draw());
        }

        /// <summary>
        /// Removes a card from the player's current hand.
        /// </summary>
        /// <param name="card">A reference to the card which is to be removed from the hand.</param>
        public void DiscardCard(Card card)
        {
            // Remove the card from the collection representing the current hand.
            Hand.Remove(card);

            // If the player now has a winning hand, generate an event if there is an event binding.
            if (HasWon && OnPlayerHasWon != null)
                OnPlayerHasWon(this, new PlayerEventArgs { Player = this, State = PlayerState.Winner });

            // If there is an event binding for discarding a card, generate that event.
            if (OnCardDiscarded != null)
                OnCardDiscarded(this, new CardEventArgs { Card = card });
        }

        /// <summary>
        /// Draws a whole new hand for the player.
        /// </summary>
        /// <param name="deck">A reference to the deck in use for the game.</param>
        public void DrawNewHand(Deck deck)
        {
            Hand = new Cards();
            for (int i = 0; i < 7; i++)
                Hand.Add(deck.Draw());
        }

        /// <summary>
        /// Determines if the player has won the current hand.
        /// </summary>
        public bool HasWon
        {
            get
            {
                if (Hand.Count == 7)
                {
                    // This game uses the simple rule that all cards in the hand must be of
                    // the same suit to win.
                    var suit = Hand[0].suit;
                    for (int i = 1; i < Hand.Count; i++)
                        if (suit != Hand[i].suit)
                            return false;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Retrieves a collection containing all the cards currently in the player's hand.
        /// </summary>
        /// <returns>A Cards collection containing a copy of the player's hand.</returns>
        public Cards GetCards()
        {
            return Hand.Clone() as Cards;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
