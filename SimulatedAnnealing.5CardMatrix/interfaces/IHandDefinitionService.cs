using SimulatedAnnealing._5CardMatrix.Enumerations;
using System.Collections.Generic;
using SimulatedAnnealing._5CardMatrix.Classes;

namespace SimulatedAnnealing._5CardMatrix.Interfaces
{
    public interface IHandFinder
    { 
        Hand FindBestHand(IList<Card> cards);
    }
}