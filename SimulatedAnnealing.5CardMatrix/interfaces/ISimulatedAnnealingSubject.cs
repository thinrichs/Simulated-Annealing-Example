using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulatedAnnealing._5CardMatrix.classes;

namespace SimulatedAnnealing._5CardMatrix.interfaces
{
    public interface ISimulatedAnnealingSubject
    {
        IStatePermuter StatePermuter { set; }
        IStateScorer StateScorer { set; }
        double Score {get;}
        void Permute();
        void Randomize();
        string ToString();
    }
}
