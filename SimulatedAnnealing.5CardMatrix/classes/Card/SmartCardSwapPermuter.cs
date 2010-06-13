using System;
using System.Collections.Generic;
using SimulatedAnnealing._5CardMatrix.interfaces;

namespace SimulatedAnnealing._5CardMatrix.classes
{
    class SmartCardSwapPermuter : BaseSwapPermuter, IStatePermuter
    {
        private void SwapCardsForCornerAndCenterPositions(IList<Card> Cards)
        {
            // assumes a grid
            double lengthOfSide = Math.Sqrt(Cards.Count);

            // if length of side is integral
            if (Math.Round(lengthOfSide) == lengthOfSide)
            {
                // init array for the four "corners" of our list ( if it was a 2D grid )
                int[] corners = new int[4] {0, (int)lengthOfSide-1, (int)(Cards.Count - lengthOfSide) - 1, Cards.Count - 1};
                int center =(int)Math.Round((decimal)Cards.Count / 2);
                Random r = new Random();
                int firstCardIndex = r.Next() % Cards.Count;
                int secondCardIndex = r.Next() % Cards.Count;
                int thirdCardIndex = r.Next() % Cards.Count;
                int fourthCardIndex = r.Next() % Cards.Count;
                int fifthCardIndex = r.Next() % Cards.Count;

                SwapCards(Cards, corners[0], firstCardIndex);
                SwapCards(Cards, corners[1], secondCardIndex);
                SwapCards(Cards, corners[2], thirdCardIndex);
                SwapCards(Cards, corners[3], fourthCardIndex);
                SwapCards(Cards, center, fifthCardIndex);

            }
            else //punt on smart swap just do random
            {
                new CardSwapPermuter().PermuteStateOf<Card>(Cards);
            }
        }
        public void PermuteStateOf<T>(IList<T> cards) where T : class
        {
            SwapCardsForCornerAndCenterPositions(cards as IList<Card>);
        }
    }
}