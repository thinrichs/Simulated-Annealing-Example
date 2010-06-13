using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulatedAnnealing._5CardMatrix.Interfaces;
using SimulatedAnnealing._5CardMatrix.Enumerations;
using SimulatedAnnealing._5CardMatrix.Classes;

namespace SimulatedAnnealing._5CardMatrix.Services
{
    public class HandFinder : IHandFinder
    {
        #region IHandFinder Members

        public Hand FindBestHand(IList<Card> cards)
        {
            Hand result = Hand.HighCard;
            // there's got to be a better way? - I hate nested crud like this
            if (CardsCanConstituteHand(cards))
            {
                int numPairs = NumPairsInHand(cards);
                // if we have a pair, the possible hands are pair, two pair, and full house
                if (numPairs > 0)
                {
                    result = (numPairs == 2) ? Hand.TwoPair : Hand.OnePair;
                    // if we have one pair, we still have room for three of a kind so check for that
                    if (result == Hand.OnePair)
                    {
                        if (ThreeOfAKindInHand(cards))
                        {
                            result = Hand.FullHouse;
                        }
                    }
                }
                else
                {// should use switch but it complains about not having constant value
                    if (ThreeOfAKindInHand(cards))
                    {
                        result = Hand.ThreeOfAKind;
                    }
                    else if (FourOfAKindInHand(cards))
                    {
                        result = Hand.FourOfAKind;
                    }
                    else if (FlushInHand(cards))
                    {
                        result = (StraightInHand(cards)) ? Hand.StraightFlush : Hand.Flush;
                    }
                    else if (StraightInHand(cards))
                    {
                        result = Hand.Straight;
                    }
                }                
            }
            else
            {
                string cardList = string.Empty;
                foreach (Card c in cards)
                {
                    cardList += c +", ";
                }
                throw new ArgumentException("Can't define hand with given cards: " + cardList);
            }
            return result;
        }

        private bool FourOfAKindInHand(IList<Card> cards)
        {
            return GetFourOfAKind(cards) != null;
        }       

        private bool StraightInHand(IList<Card> cards)
        {
            // theres probably a better way to do this whole thing with linq... but I can't figure it out.  Ask Paul 
            int maxSequenceLength = 1;
            CardFigure minCard = cards.Min<Card, CardFigure>(key => key.Figure);
            CardFigure maxCard = cards.Max<Card, CardFigure>(key => key.Figure);
            CardFigure lastCard = minCard;
            int totalCardFigures = 0;
            // go through the cards except the lowest card - we've already handled it in variable assignment
            foreach (Card card in cards.OrderBy<Card, CardFigure>(key => key.Figure).Skip<Card>(1))
            {
                // if this card is one higher than the last one was               
                maxSequenceLength += ((lastCard + 1) == card.Figure) ? 1 : 0;                
                lastCard = card.Figure;
                // theres got to be a better way then summing all cards A,2,3,4,5 == 22
                totalCardFigures += (int)card.Figure;
            }
            // Either you have to have 5 cards in a row
            // or it is also valid to have 4 cards in a row, if your min card is a two and your max card is an ace
            // this handles the problematic ace wraparound where an ace can be at either end of a straight
            return (maxSequenceLength >= 5) ||
                  ((maxSequenceLength == 4) && (maxCard == CardFigure.Ace) && (totalCardFigures == 22)); // A,2,3,4,5 == 22
        }

        private bool ThreeOfAKindInHand(IList<Card> cards)
        {
            return GetThreeOfAKind(cards) != null;
        }

        protected internal IEnumerable<IGrouping<CardFigure, Card>> GetSets(IList<Card> cards, int cardsInSet)
        {
          // broke my original linq up so it can be debugged.  This is a good idea? or bad idea?  Then put it back together
            return cards
                      .GroupBy<Card, CardFigure>(key => key.Figure)
                      .Where<IGrouping<CardFigure, Card>>(group => group.Count<Card>() == cardsInSet);

        }

        public IEnumerable<IGrouping<CardFigure, Card>> GetPairs(IList<Card> cards)
        {
            return GetSets(cards, 2);            
        }

        public IGrouping<CardFigure, Card> GetThreeOfAKind(IList<Card> cards)
        {   // can only have one three of a kind in a hand (but the set of all cards could have many) - so we max
            return GetSets(cards, 3).Max<IGrouping<CardFigure, Card>>();
        }

        private IGrouping<CardFigure, Card> GetFourOfAKind(IList<Card> cards)
        {
            return GetSets(cards, 4).Max<IGrouping<CardFigure, Card>>();
        }

        // tried to use linq to find the number of pairs in an IList of cards
        protected internal int NumPairsInHand(IList<Card> cards)
        {
            int result = 0;            
            IEnumerable<IGrouping<CardFigure, Card>> pairs = GetPairs(cards);
           
            // we have pairs
            if (pairs != null)
            {
                result = pairs.Count<IGrouping<CardFigure, Card>>();
            }
            return result;                          
        }
        private bool FlushInHand(IList<Card> cards)
        {
            return (GetFlush(cards) != null);
        }

        private IGrouping<CardColor, Card> GetFlush(IList<Card> cards)
        {
            return cards
                     .GroupBy<Card, CardColor>(key => key.Color)
                     .Where<IGrouping<CardColor, Card>>(group => group.Count<Card>() >= 5)
                     .Max<IGrouping<CardColor, Card>>();
        }


        private static bool CardsCanConstituteHand(IList<Card> cards)
        {
            // are there any poker variants that allow more than 7 cards?
            return ((cards != null) && (cards.Count >= 5) && (cards.Count <= 7));           
        }
        #endregion
    }
}
