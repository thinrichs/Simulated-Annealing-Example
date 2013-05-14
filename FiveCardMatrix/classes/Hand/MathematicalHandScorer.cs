using System.Collections.Generic;
using System.Linq;
using FiveCardMatrix.classes;
using FiveCardMatrix.Enumerations;
using FiveCardMatrix.interfaces;
using FiveCardMatrix.Services;

namespace FiveCardMatrix.classes
{
    class MathematicalHandScorer : IStateScorer
    {        
        private static HandFinder Finder = new HandFinder();
        
        public MathematicalHandScorer()
        {}
        public double Score<T>(IList<T> cards) where T : class
        {
            Hand hand = Finder.FindBestHand(cards as IList<Card>);
            double baseValue = hand.BaseValue;
            double value = FractionalHandValue(cards as IList<Card>);
            return (baseValue + value);
        }

        private double FractionalHandValue(IList<Card> cards)
        {
            double result = 0;
            int divisor = 1;
            //if I've constructed the linq correctly it should group by cardfigure count (thereby putting sets first), then select each card figure   
            // this code will start to be inaccurate if we're trying to do values of texas holdem hands for example (7 cards)
            var cardFigureCardinalities = cards
                            .GroupBy<Card, CardFigure>(key => key.Figure)
                            .OrderByDescending<IGrouping<CardFigure, Card>, int>(group => group.Count<Card>())
                            .ThenByDescending<IGrouping<CardFigure, Card>, CardFigure>(group => group.Key)
                            .Select(item => new { Figure = item.Key, Cardinality = item.Count<Card>() });

            foreach (var cardInfo in cardFigureCardinalities)
            {
                result += (FractionalValue(cardInfo.Figure) / divisor);
                divisor *= 100;
                result += (cardInfo.Cardinality / divisor);
            }
            return result;
        }
        private static double FractionalValue(CardFigure figure)
        {
            return ((double)figure / ((double)CardFigure.Ace + 1));
        }   

    }
}
