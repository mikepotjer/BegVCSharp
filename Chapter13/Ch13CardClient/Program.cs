using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Ch13CardLib;

namespace Ch12CardClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Test 1");
            Deck deck1 = new Deck();
            Deck deck2 = (Deck)deck1.Clone();

            // First show that both decks start out the same.
            WriteLine($"The first card in the original deck is: {deck1.GetCard(0)}");
            WriteLine($"The first card in the cloned deck is: {deck2.GetCard(0)}");

            // Shuffle the first deck, then show that changes to it do not affect the cloned deck.
            deck1.Shuffle();
            WriteLine("Original deck shuffled.");
            WriteLine($"The first card in the original deck is: {deck1.GetCard(0)}");
            WriteLine($"The first card in the cloned deck is: {deck2.GetCard(0)}");

            WriteLine("\nTest 2");
            Card.isAceHigh = true;
            WriteLine("Aces are high.");

            Card.useTrumps = true;
            Card.trump = Suit.Club;
            WriteLine("Clubs are trumps.");

            Card card1, card2, card3, card4, card5;
            card1 = new Card(Suit.Club, Rank.Five);
            card2 = new Card(Suit.Club, Rank.Five);
            card3 = new Card(Suit.Club, Rank.Ace);
            card4 = new Card(Suit.Heart, Rank.Ten);
            card5 = new Card(Suit.Diamond, Rank.Ace);

            // Test the operator overloads by comparing various pairs of cards.
            WriteLine($"{card1.ToString()} == {card2.ToString()} ? {card1 == card2}");
            WriteLine($"{card1.ToString()} != {card3.ToString()} ? {card1 != card3}");
            WriteLine($"{card1.ToString()}.Equals({card4.ToString()}) ? " +
                  $" { card1.Equals(card4)}");
            WriteLine($"Card.Equals({card3.ToString()}, {card4.ToString()}) ? " +
                  $" { Card.Equals(card3, card4)}");
            WriteLine($"{card1.ToString()} > {card2.ToString()} ? {card1 > card2}");
            WriteLine($"{card1.ToString()} <= {card3.ToString()} ? {card1 <= card3}");
            WriteLine($"{card1.ToString()} > {card4.ToString()} ? {card1 > card4}");
            WriteLine($"{card4.ToString()} > {card1.ToString()} ? {card4 > card1}");
            WriteLine($"{card5.ToString()} > {card4.ToString()} ? {card5 > card4}");
            WriteLine($"{card4.ToString()} > {card5.ToString()} ? {card4 > card5}");

            WriteLine("\nTest 3");
            try
            {
                Card myCard = deck1.GetCard(60);
            }
            catch (CardOutOfRangeException e)
            {
                // Display the custom exception message, and the first card in the deck, to
                // show we have access to the deck.
                WriteLine(e.Message);
                WriteLine(e.DeckContents[0]);
            }

            ReadKey();
        }
    }
}
