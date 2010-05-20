using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulatedAnnealing._5CardMatrix.Classes;
using System.Collections.Specialized;
using SimulatedAnnealing._5CardMatrix.Services;
using SimulatedAnnealing._5CardMatrix.interfaces;

namespace SimulatedAnnealing._5CardMatrix.classes
{
    public class CardGrid : ISimulatedAnnealingSubject
    {
        private enum HandType
        {
            Column,
            Row,
            Diagonal
        }

        // the set would be private but we need to be able to have contrived cases for unit tests
        public List<Card> Cards { get; private set; }
        public int LengthOfSide {get; private set;}
        public IStatePermuter StatePermuter { get; set; }
        public IStateScorer StateScorer { get; set; }

        public CardGrid(int numberOfCardsPerSide) : this(numberOfCardsPerSide, null as List<Card>) { }

        public CardGrid(int numberOfCardsPerSide, List<Card> cards)            
        {
            StateScorer = new MathematicalHandScorer();
            StatePermuter = new CardSwapPermuter();
            LengthOfSide = numberOfCardsPerSide;          
            if ((cards == null) || (cards.Count < numberOfCardsPerSide * numberOfCardsPerSide))
            {
                SetupCards(numberOfCardsPerSide);
            }
            else
            {
                Cards = cards;
            }
        }
        public CardGrid(int numberOfCardsPerSide, IStatePermuter permuter)
            : this(numberOfCardsPerSide, null as List<Card>)
        {
            StatePermuter = permuter;
        }
        public CardGrid(int numberOfCardsPerSide, IStatePermuter permuter, IStateScorer scorer)
            : this(numberOfCardsPerSide, null, permuter, scorer)
        { }

        public CardGrid(int numberOfCardsPerSide, List<Card> cards, IStatePermuter permuter, IStateScorer scorer) : this(numberOfCardsPerSide, cards)
        {
            StatePermuter = permuter;
            StateScorer = scorer;           
        }

        private void SetupCards(int numberOfCardsPerSide)
        {
            Deck deck = new Deck(true);
            // instead of x*x isn't there a way to say x squared - that will return ints?
            Cards = deck.SelectFromTop(numberOfCardsPerSide * numberOfCardsPerSide);
        }

        private bool PositionBelongsInHand(HandType handType, int handCardinality, int position)
        {
            bool result = false;
            switch (handType)
            {
                case HandType.Column: result = (position % LengthOfSide) == handCardinality; break;                  
                case HandType.Row:    result = (position / LengthOfSide) == handCardinality; break;
                case HandType.Diagonal:
                    {
                        // adding 1 to divisor gives us the diagonal offset needed for \
                        if (handCardinality == 0)
                        {
                            result = (position % (LengthOfSide + 1) == 0);
                        }
                        else if (handCardinality == 1)
                        {   // this is diagonal needed for / 
                            result = ((position != 0) && (position != Cards.Count - 1));
                            result &= (position % (LengthOfSide - 1) == 0);
                        }
                        break;
                    }
            }
            return result;
        }

        private Hand GetHandByType(HandType handType, int index)
        {
            List<Card> cards = null;

            if (LengthOfSide >= index)
            {
                cards = new List<Card>(LengthOfSide);               
                for (int cardIndex = 0; cardIndex < Cards.Count; cardIndex++)
                {
                    if (PositionBelongsInHand(handType,index,cardIndex))
                    {
                        cards.Add(Cards[cardIndex]);
                    }
                }
            }
            // look at implementing null object pattern for hand class.  the fact that we need these checks mean somethign is wrong
            return ((cards!= null) && (cards.Count > 0)) ? new Hand(cards, StateScorer) : null;            
        }

        // TODO: refactor Column, Row, Diagonal to common composed method with another method for whether a given index should be included in result
        public Hand Column(int columnIndex)
        {
            return GetHandByType(HandType.Column, columnIndex);
        }
        public Hand Row(int rowIndex)
        {
            return GetHandByType(HandType.Row, rowIndex);
        }
        public Hand Diagonal(int diagonalIndex)
        {
            return GetHandByType(HandType.Diagonal, diagonalIndex);
        }      
        public double Score
        {
            get
            {
                double result = 0;
                for (int handIndex = 0; handIndex < LengthOfSide; handIndex++)
                {
                    result += Column(handIndex).Value;
                    result += Row(handIndex).Value;
                    // there are only two diagonal values for a hand
                    if (handIndex < 2)
                    {
                        result += Diagonal(handIndex).Value;
                    }
                }
                return result;
            }
        }

        public void Permute()
        {
            StatePermuter.PermuteStateOf<Card>(Cards);
        }
        public override string ToString()
        {
            return CardGridPrinter.PrintCardGrid(this);
        }


        #region ISimulatedAnnealingSubject Members


        public void Randomize()
        {
            Cards = Shuffler.RandomizeListOf<Card>(Cards);
        }

        #endregion
    }
}