using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SimulatedAnnealing._5CardMatrix.interfaces
{
    interface ISimulatedAnnealingDriver
    {
        ISimulatedAnnealingSubject Subject {set;}
        ISimulatedAnnealingRandomzier Randomizer { set; }
        ISimulatedAnnealingSubject Solution { get; }
        ISimulatedAnnealingMathematicalDriver MathDriver { set; }
        event PropertyChangedEventHandler NewSolution;

        string SolutionReport { get; }       
        void DriveAnnealing();

    }
}
