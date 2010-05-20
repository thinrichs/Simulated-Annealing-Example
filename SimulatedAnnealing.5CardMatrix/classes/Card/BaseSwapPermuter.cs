using System.Collections.Generic;

namespace SimulatedAnnealing._5CardMatrix.classes
{
    class BaseSwapPermuter
    {
        protected static void SwapCards(IList<Card> Cards, int firstCardIndex, int secondCardIndex)
        {
            Card randomlyPickedCard = Cards[firstCardIndex];
            Cards[firstCardIndex] = Cards[secondCardIndex];
            Cards[secondCardIndex] = randomlyPickedCard;
        }
    }
}