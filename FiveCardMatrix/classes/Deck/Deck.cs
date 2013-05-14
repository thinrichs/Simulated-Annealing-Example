using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FiveCardMatrix
{    // card class from http://www.conceptdevelopment.net/Fun/Pontifex/ just because it was the first I ran across ( then modified to suck less).
    /*  This class control List<Card> in which all cards are. */
    public class Deck : IEnumerable
    {
        private const int CARDS_IN_DECK = 52;
        private int CardIndex { get; set; }
        /* -- -- -- -- -- -- Variables -- -- -- -- -- -- */
        public List<Card> Cards {get; set;}        
        /* -- -- -- -- -- -- Accessors -- -- -- -- -- -- */
        public Card this[int i]
        {
            get { return Cards[i]; }
            set { Cards[i] = value; }
        }
        private void validateCards(Array cards)
        {
            if (cards == null)
            {
                throw new ArgumentNullException();
            }           
        }
       
        /* -- -- -- -- -- -- Constructors -- -- -- -- -- -- */
        public Deck(Array cards)
        {           
            validateCards(cards);
            Cards = new List<Card>();
        }
        public Deck(Card[] cards) 
            : this(cards as Array)
        {
            Cards.AddRange(cards);
        }
        public Deck(int[] cards)
            : this(cards as Array)
        {          
            foreach (int c in cards)
            {
                Cards.Add(new Card(c));
            }
        }
        public Deck(string DeckPopulation) : this(FileToRandomCardArray(DeckPopulation)) { }
        public Deck() : this(false) {}
        public Deck(bool randomize) 
            : this(CreateDeck())
        {            
            if (randomize)
            {
                Cards = Shuffler.RandomizeListOf<Card>(Cards);
            }
        }

        public List<Card> SelectFromTop(int numberOfCards)
        {
            return Cards.Take<Card>(numberOfCards).ToList<Card>();
        }
        private static Card[] CreateDeck()
        {
            Card[] result = new Card[CARDS_IN_DECK];
            for (int i = 0; i< CARDS_IN_DECK; i++)
            {
                // cards are 1 based
                result[i] = new Card(i + 1);
            }
            return result;
        }
        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return Cards.GetEnumerator();
        }
        #endregion

        private static Card[] FileToRandomCardArray(string fileName)
        {
            List<Card> cards = System.IO.File.ReadAllLines(fileName)
                    .Where(line => !line.StartsWith("#"))                    
                    .Select(line => line.Trim())
                    .Where(line => (line.Length == 2) || (line.Length == 3))
                    .Select<string,Card>(line => Card.StringToCard(line))
                    .ToList<Card>();

            return Shuffler.RandomizeListOf<Card>(cards).ToArray<Card>();
        }
    }
}