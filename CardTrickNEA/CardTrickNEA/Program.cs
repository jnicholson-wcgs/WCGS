using System;
using System.Collections;


/*
 * Card Trick NEA
 * AQA practice NEA for GCSE.
 */

namespace CardTrickNEA
{
    class Program
    {

        static void Main(string[] args)
        {
            ArrayList deck = generateDeck();        // Generate the deck

            //printDeal(deck, "Initial Deck");

            ArrayList deal = dealDeck(deck);        // Deal out 21 cards

            Console.WriteLine("Think of card\n");
            for (int turn = 0; turn < 3; turn++)
            {
                printDeal(deal, "Select which column your card is in [1, 2, or 3]");

                int col = inputColumn();        // get the column from user
                deal = reorderDeal(deal, col);  // re-deal 

            }
            // Da da - the big reveal.
            Console.WriteLine("Your card is: {0}", deal[10]);
        }

        /*
         * Generate a pack of 52 cards
         */

        static ArrayList generateDeck()
        {
            string[] suits = { "S", "H", "D", "C" };
            string[] ranks = {"A", "K", "Q", "J", "10", "9", "8", "7", "6",
                "5", "4", "3", "2"};

            ArrayList deck = new ArrayList();

            foreach (var suit in suits)
                foreach (var rank in ranks)
                    deck.Add(rank + " " + suit);

            return deck;
        }

        /*
         * Print out the deal in three columns
         */

        static void printDeal(ArrayList deal, string comment)
        {
            Console.WriteLine(comment);     // Print out the comment

            int rows = deal.Count / 3; // round down the rows to print, in case we are debugging

            Console.WriteLine("Col 1\tCol 2\tCol 3");
            for (int i = 0; i < rows; i++)
            {
                int index = i * 3;      // deal out three cards in each row
                Console.WriteLine("{0}\t{1}\t{2}", deal[index], deal[index + 1], deal[index + 2]);
            }
            Console.WriteLine("\n");
        }

        /*
         * Deal the deck by picking a random number between 0 and the number of cards
         * still in the deck. Each card used, is removed from the deck so that it is not
         * reused
         */

        static ArrayList dealDeck(ArrayList deck)
        {
            int randmax = deck.Count - 1;
            ArrayList deal = new ArrayList();

            Random random = new Random();

            // generate a random number from a decreasing range of numbers
            // Start with 52, pick a card from the deck and then pick a random
            // card from remaining 51 cards in the deck

            for (int i = 0; i < 21; i++)
            {
                int cardindex = random.Next(0, randmax - i); // pick the card

                deal.Add(deck[cardindex]);                  // Add it to the deal
                deck.RemoveAt(cardindex);                   // remove from deck
            }
            return deal;
        }

        /*
         * Prompt user for a column in which their card is
         * Use try/catch for invalid inputs
         */

        static int inputColumn()
        {
            Boolean ok = false;
            int selection = 0;
            while (!ok)
            {
                try
                {
                    Console.Write("Enter column: ");
                    selection = Int32.Parse(Console.ReadLine());
                    if (selection >= 1 && selection <= 3)
                        ok = true;
                    else
                        Console.WriteLine("Invalid column, please try again");
                }
                catch
                {
                    Console.WriteLine("Invalid integer value please try again");
                }
            }
            return selection;
        }

        /*
         * Reorder the deal so that the selected column is in the middle of the deal.
         * Firstly remove the selected column from the deal
         * Secondly put 7 cards before and then 7 cards after it
         * Not strictly how you do it in the card demo but no one will notice.....
         */

        static ArrayList reorderDeal(ArrayList deal, int col)
        {
            ArrayList reorder = new ArrayList();
            col = (col - 1);

            // Remove the selected column from the deal
            // Start by removing from the end of the deal, as it makes the
            // calculation of the indices easier...
            for (int i = 6; i >= 0; i--)
            {
                int index = (i * 3) + col;          // index of card
                reorder.Insert(0, deal[index]);     // add at beginning
                deal.RemoveAt(index);               // remove from deal
            }

            // Having extracted the chosen column, insert 7 cards before it
            for (int i = 0; i < 7; i++)
            {
                reorder.Insert(0, deal[i]);
            }

            // Put the remaining cards after the selected column
            for (int i = 0; i < 7; i++)
            {
                reorder.Insert(14, deal[i]);
            }

            return reorder;
        }
    }

}

