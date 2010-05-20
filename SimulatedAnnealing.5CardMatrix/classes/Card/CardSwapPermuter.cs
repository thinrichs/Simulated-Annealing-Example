using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulatedAnnealing._5CardMatrix.interfaces;

namespace SimulatedAnnealing._5CardMatrix.classes
{
    class CardSwapPermuter : BaseSwapPermuter, IStatePermuter
    {   
        private void SwapTwoCardsAtRandom(IList<Card> Cards)
        {
            Random r = new Random();
            int firstCardIndex = 0;
            int secondCardIndex = 0;

            // in case we somehow pick the same
            while (firstCardIndex == secondCardIndex)
            {
                firstCardIndex = r.Next() % Cards.Count;
                secondCardIndex = r.Next() % Cards.Count;
            }

            SwapCards(Cards, firstCardIndex, secondCardIndex);
        }


        public void PermuteStateOf<T>(IList<T> cards) where T : class
        {
            SwapTwoCardsAtRandom(cards as IList<Card>);
        }
    }
}
