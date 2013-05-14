using System.Collections.Generic;

namespace FiveCardMatrix.classes
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