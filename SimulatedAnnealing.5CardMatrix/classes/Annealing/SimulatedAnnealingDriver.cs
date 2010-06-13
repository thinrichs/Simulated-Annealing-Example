using System;
using System.ComponentModel;
using SimulatedAnnealing._5CardMatrix.interfaces;

namespace SimulatedAnnealing._5CardMatrix.classes
{
    class SimulatedAnnealingDriver : ISimulatedAnnealingDriver, ISimulatedAnnealingDriverStatistics
    {
        public SimulatedAnnealingDriver()  {}

        #region ISimulatedAnnealingDriverStatistics Members

        public int BetterSolutionsTaken { get; private set; }
        public int WorseSolutionsTaken { get; private set; }
       
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int Iterations 
        { 
            get {return MathDriver.Iterations;}
            private set {MathDriver.Iterations = value;}
        }

        #endregion
        #region ISimulatedAnnealingDriver Members

        public ISimulatedAnnealingSubject Subject { get; set; }
        public ISimulatedAnnealingSubject Solution { get; set; }
        public ISimulatedAnnealingRandomzier Randomizer { get; set; }
        public ISimulatedAnnealingMathematicalDriver MathDriver { get; set; }
        public string SolutionReport { get; set; }

        #endregion

        private double _bestSolutionScore;

        // null object pattern
        //http://stackoverflow.com/questions/1131184/c-initializing-an-event-handler-with-a-dummy
        private event PropertyChangedEventHandler newSolution = (s, e) => { };
        public event PropertyChangedEventHandler NewSolution
        {
            add { newSolution += value; }
            remove { newSolution -= value; }
        }

        private void OnNewSolution()
        {
            newSolution(this, new PropertyChangedEventArgs(String.Empty));
        }
        
        public void DriveAnnealing()
        {   // make sure we are sufficeintly random
            Subject.Randomize();
            StartTime = DateTime.Now;
            // initial state is always first solution          
            ThereIsANewSolution(Subject);

            if ((MathDriver != null) && (Randomizer != null))
            {
                double delta;

                while (MathDriver.Temperature > MathDriver.Epsilon)
                {
                    // change configuration of cards
                    Subject.Permute();

                    delta = _bestSolutionScore - Solution.Score;
                    if (delta < 0)
                    {
                        ThereIsANewSolution(Subject);
                        BetterSolutionsTaken++;
                        
                    }
                    else if (Randomizer.TakeSolution(delta))
                    {
                        ThereIsANewSolution(Subject);
                        WorseSolutionsTaken++;                       
                    }

                    MathDriver.Temperature *= MathDriver.Alpha;
                    MathDriver.Iterations++;
                }
            }
            EndTime = DateTime.Now;
        }

        private void ThereIsANewSolution(ISimulatedAnnealingSubject newSolution)
        {
            Solution = newSolution;
            SolutionReport = newSolution.ToString();
            _bestSolutionScore = Solution.Score;
            OnNewSolution();
        }
    }
}