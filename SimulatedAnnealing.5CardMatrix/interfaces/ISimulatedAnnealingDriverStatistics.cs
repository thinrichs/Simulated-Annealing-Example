using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulatedAnnealing._5CardMatrix.interfaces
{
    interface ISimulatedAnnealingDriverStatistics
    {
        int BetterSolutionsTaken { get; }
        int WorseSolutionsTaken { get; }
        int Iterations { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
    }
}
