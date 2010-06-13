using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulatedAnnealing._5CardMatrix.interfaces
{
    public interface IStateScorer
    {
        double Score<T>(IList<T> elements) where T : class;
    }
}