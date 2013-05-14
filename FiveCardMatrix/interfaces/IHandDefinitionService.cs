using FiveCardMatrix.Enumerations;
using System.Collections.Generic;
using FiveCardMatrix.classes;

namespace FiveCardMatrix.Interfaces
{
    public interface IHandFinder
    { 
        Hand FindBestHand(IList<Card> cards);
    }
}