using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ch13CardLib;
using static System.Console;

namespace Ch13CardClient
{
    public class Game
    {
        // Add private fields to keep track of the game. These are the deck we're using; the
        // current (or top) card in that deck; an array for the players; and a collection of
        // the cards that have been discarded.
        private Deck playDeck;
        private int currentCard;
        private Player[] players;
        private Cards discardedCards;

        public Game()
        {
            // Initialize the current card to the first card in the deck.
            currentCard = 0;

            // Create a new deck, with aces high, and shuffle it.
            playDeck = new Deck(true);
            playDeck.Shuffle();

            // Initialize the collection of discarded cards.
            discardedCards = new Cards();

            // Set up an event handler to reshuffle the deck when the last card is drawn.
            playDeck.LastCardDrawn += Reshuffle;
        }

        /// <summary>
        /// The event handler for the LastCardDrawn. This reshuffles the deck, resets the
        /// discard pile, and resets the top card.
        /// </summary>
        /// <param name="source">A reference to the current Deck being used.</param>
        /// <param name="args">Event arguments (nothing expected).</param>
        private void Reshuffle(object source, EventArgs args)
        {
            WriteLine("Discarded cards reshuffled into deck.");

            // Shuffle the deck.
            ((Deck)source).Shuffle();

            // Clear the discard pile, and reset the top card.
            discardedCards.Clear();
            currentCard = 0;
        }

        /// <summary>
        /// Sets the number of players for the current game.
        /// </summary>
        /// <param name="newPlayers">An array of Player objects. Must contain between 2 and 7 players.</param>
        public void SetPlayers(Player[] newPlayers)
        {
            if (newPlayers.Length > 7)
                throw new ArgumentException("A maximum of 7 players may play this game.");
            if (newPlayers.Length < 2)
                throw new ArgumentException("A minimum of 2 players may play this game.");
            players = newPlayers;
        }

        /// <summary>
        /// Internal method to deal a hand to all the players.
        /// </summary>
        private void DealHands()
        {
            // Deal a hand to each player.
            for (int p = 0; p < players.Length; p++)
            {
                // Deal 7 cards to the current player.
                for (int c = 0; c < 7; c++)
                {
                    players[p].PlayHand.Add(playDeck.GetCard(currentCard++));
                }
            }
        }

        public int PlayGame()
        {
            // Only play if players exist.
            if (players == null)
                return -1;

            // Deal initial hands.
            DealHands();

            // Initialize game vars, including an initial card to place on the table: playCard.
            bool GameWon = false;
            int currentPlayer;
            Card playCard = playDeck.GetCard(currentCard++);
            discardedCards.Add(playCard);

            // Main game loop, continues until GameWon == true.
            do
            {
                // Loop through players in each game round.
                for (currentPlayer = 0; currentPlayer < players.Length;
                     currentPlayer++)
                {
                    //Write out current player, player hand, and the card on the table.
                    WriteLine($"{players[currentPlayer].Name}'s turn.");
                    WriteLine("Current hand:");
                    // Sort the player's hand before displaying it.
                    players[currentPlayer].PlayHand.Sort(CardComparerSuit.Default);
                    foreach (Card card in players[currentPlayer].PlayHand)
                    {
                        WriteLine(card);
                    }
                    WriteLine($"Card in play: {playCard}");

                    // Prompt player to pick up card on table or draw a new one.
                    bool inputOK = false;
                    do
                    {
                        WriteLine("Press T to take card in play or D to draw:");
                        string input = ReadLine();
                        if (input.ToLower() == "t")
                        {
                            // Add card from table to player hand.
                            WriteLine($"Drawn: {playCard}");

                            // Remove from discarded cards if possible (if deck is reshuffled it
                            // won't be there any more)
                            if (discardedCards.Contains(playCard))
                            {
                                discardedCards.Remove(playCard);
                            }
                            players[currentPlayer].PlayHand.Add(playCard);
                            inputOK = true;
                        }
                        if (input.ToLower() == "d")
                        {
                            // Add new card from deck to player hand.
                            Card newCard;

                            // Only add card if it isn't already in a player hand or in the discard pile
                            bool cardIsAvailable;
                            do
                            {
                                newCard = playDeck.GetCard(currentCard++);
                                // Check if card is in discard pile
                                cardIsAvailable = !discardedCards.Contains(newCard);
                                if (cardIsAvailable)
                                {
                                    // Loop through all player hands to see if newCard is already in a hand.
                                    foreach (Player testPlayer in players)
                                    {
                                        if (testPlayer.PlayHand.Contains(newCard))
                                        {
                                            cardIsAvailable = false;
                                            break;
                                        }
                                    }
                                }
                            } while (!cardIsAvailable);

                            // Add the card found to player hand.
                            WriteLine($"Drawn: {newCard}");
                            players[currentPlayer].PlayHand.Add(newCard);
                            inputOK = true;
                        }
                    } while (inputOK == false);

                    // Display new hand with cards numbered.
                    WriteLine("New hand:");
                    // Sort the cards before displaying them to make the selection easier.
                    players[currentPlayer].PlayHand.Sort(CardComparerSuit.Default);
                    for (int i = 0; i < players[currentPlayer].PlayHand.Count; i++)
                    {
                        WriteLine($"{i + 1}: {players[currentPlayer].PlayHand[i]}");
                    }

                    // Prompt player for a card to discard.
                    inputOK = false;
                    int choice = -1;
                    do
                    {
                        WriteLine("Choose card to discard:");
                        string input = ReadLine();
                        try
                        {
                            // Attempt to convert input into a valid card number.
                            choice = Convert.ToInt32(input);
                            if ((choice > 0) && (choice <= 8))
                                inputOK = true;
                        }
                        catch
                        {
                            // Ignore failed conversions, just continue prompting.
                        }
                    } while (inputOK == false);

                    // Place reference to removed card in playCard (place the card on the table), then
                    // remove card from player hand and add to discarded card pile.
                    playCard = players[currentPlayer].PlayHand[choice - 1];
                    players[currentPlayer].PlayHand.RemoveAt(choice - 1);
                    discardedCards.Add(playCard);
                    WriteLine($"Discarding: { playCard}");

                    // Space out text for players
                    WriteLine();

                    // Check to see if player has won the game, and exit the player loop if so.
                    GameWon = players[currentPlayer].HasWon();
                    if (GameWon == true)
                        break;
                }
            } while (GameWon == false);

            // End game, noting the winning player.
            return currentPlayer;
        }
    }
}
