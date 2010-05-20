using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using SimulatedAnnealing._5CardMatrix.classes;
using SimulatedAnnealing._5CardMatrix.interfaces;
using System.ComponentModel;
using System.Collections;


namespace SimulatedAnnealing._5CardMatrix
{
    class Program
    {
        static Dictionary<string, double> results = new Dictionary<string, double>();
        static int numRuns = 5;

        static void ReportSolution(ISimulatedAnnealingSubject solution, ISimulatedAnnealingDriverStatistics stats)
        {
           // System.Console.SetCursorPosition(0, 0);
            System.Console.Write(solution);
            System.Console.WriteLine();
            System.Console.WriteLine("Solution Iteration:{0,19}", stats.Iterations);
            System.Console.WriteLine("Better Solutions Taken:{0,15}", stats.BetterSolutionsTaken);
            System.Console.WriteLine("Worse Solutions Taken:{0,16}", stats.WorseSolutionsTaken);
        }
        static void ThereIsANewSolution(Object sender, PropertyChangedEventArgs e)
        {
            ISimulatedAnnealingDriver sln = sender as ISimulatedAnnealingDriver;
            ISimulatedAnnealingDriverStatistics stats = sender as ISimulatedAnnealingDriverStatistics;
            if (sln != null)
            {
                ReportSolution(sln.Solution, stats);
            }
        }
        static SimulatedAnnealingMathematicalDriver MathDriver
        {
            get
            {
                return new SimulatedAnnealingMathematicalDriver
                {
                    Temperature = 4000.0,
                    Epsilon = 0.1,
                    Alpha = 0.9999,
                    RandomSeed = System.Environment.TickCount
                };
            }
        }
        static SimulatedAnnealingDriver SetupAnnealingDriver(string fileNameOfCardPopulation)
        {
            SimulatedAnnealingDriver result;
            ISimulatedAnnealingSubject subject;
            IStatePermuter permuter = new CardSwapPermuter();
            List<Card> cards = new Deck(fileNameOfCardPopulation).Cards;

            subject = new CardGrid(5, cards, permuter, new MathematicalHandScorer());

            SimulatedAnnealingMathematicalDriver math = MathDriver;

            result = new SimulatedAnnealingDriver
            {
                Subject = subject,
                MathDriver = math as ISimulatedAnnealingMathematicalDriver,
                Randomizer = math as ISimulatedAnnealingRandomzier
            };
            //sad.NewSolution += new PropertyChangedEventHandler(ThereIsANewSolution);
            return result;
        }
        static void RunAnnealing(string fileNameOfCardPopulation)
        {
            SimulatedAnnealingDriver sad = SetupAnnealingDriver(fileNameOfCardPopulation);
            results[fileNameOfCardPopulation] = 0;
            int runsOfFile = 0;
            while (runsOfFile < 5)
            {
                sad.DriveAnnealing();               
                results[fileNameOfCardPopulation] += sad.Solution.Score;
                runsOfFile++;
                // resetup math portion of annealing
                ReportSolution(sad.Subject, sad as ISimulatedAnnealingDriverStatistics);
                sad.MathDriver = MathDriver;
            }
            System.Console.WriteLine();
            System.Console.WriteLine("***** After {1} runs of {0} average score is {2} *****", fileNameOfCardPopulation, runsOfFile, results[fileNameOfCardPopulation] / (double)runsOfFile);
            System.Console.WriteLine();
        }
        static void Main(string[] args)
        {
            string fileName = string.Empty;
            if (args.Length > 0 && !String.IsNullOrEmpty(args[0]))
            {
                if ((args.Length > 1) && int.TryParse(args[1], out numRuns))
                { // nothing to do here, done above as out param tryparse
                }
                //System.Console.Clear();
                if (System.IO.File.Exists(args[0]))
                {
                    fileName = args[0];
                    RunAnnealing(fileName);
                }
                else if (System.IO.Directory.Exists(args[0]))
                {
                    foreach (string file in System.IO.Directory.GetFiles(args[0]))
                    {
                        RunAnnealing(file);
                    }
                }
            }
        }

        private static void PrintSADStats(ISimulatedAnnealingDriverStatistics stats)
        {
            System.Console.WriteLine("Total Iterations:\t{0,14}", stats.Iterations);
            System.Console.WriteLine("Start Time:{0,27}", stats.StartTime);
            System.Console.WriteLine("End Time:{0,29}", stats.EndTime);
            System.Console.WriteLine("Time Taken:\t{0,30}", stats.EndTime - stats.StartTime);
        }
    }
}