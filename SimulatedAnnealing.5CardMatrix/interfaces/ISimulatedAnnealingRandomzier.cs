using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulatedAnnealing._5CardMatrix.interfaces
{
    public interface ISimulatedAnnealingRandomzier
    {
        int RandomSeed { get; set; }        
        bool TakeSolution(double delta);
        void Randomize(ISimulatedAnnealingSubject subject);
    }
}
