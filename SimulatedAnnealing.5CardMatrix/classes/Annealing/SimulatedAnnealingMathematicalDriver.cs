using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulatedAnnealing._5CardMatrix.interfaces;

namespace SimulatedAnnealing._5CardMatrix.classes
{
    class SimulatedAnnealingMathematicalDriver : ISimulatedAnnealingRandomzier, ISimulatedAnnealingMathematicalDriver
    {
        public double Temperature { get; set; }
        public double Alpha { get; set; }
        public double Epsilon { get; set; }
        public int Iterations { get; set; }
      
        private Random r;
        private double Random
        {
            get
            {
                if (r == null)
                {
                    r = (RandomSeed == default(double)) ? new Random() : new Random(RandomSeed);
                }
                return r.NextDouble();
            }
        }

        public SimulatedAnnealingMathematicalDriver() { }         
       
        #region ISimulatedAnnealingRandomzier Members

        int _rand;
        public int RandomSeed 
        { 
            get {return _rand;}
            set
            {
                _rand = value;
                r = new Random(_rand);
            } 
        }        
        public bool TakeSolution(double delta)
        {
            int deltaMultiple = 220;
            double totalDelta = 0;
            int deltaIndex = 0;
            while (deltaIndex < deltaMultiple)
            {
                totalDelta += delta;
                deltaIndex++;
            }
            return (Random > (totalDelta));//Math.Exp(delta / Temperature));
        }
        #endregion

        #region ISimulatedAnnealingRandomzier Members


        public void Randomize(ISimulatedAnnealingSubject subject)
        {
            subject.Randomize();
        }

        #endregion
    }
}