using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTest;
using SimulatedAnnealing._5CardMatrix.classes;

namespace HandTests.Tests
{
    /// <summary>
    /// Summary description for RealWorld_Grid_Test
    /// </summary>
    [TestClass]
    public class When_CardGrid_Is_Permuted : SpecificationContext
    {
        private CardGrid _cardGrid;
        private double _intialGridValue;

        public override void CreateContext()
        {
            _cardGrid = new CardGrid(5);
            _intialGridValue = _cardGrid.Score;
        }


        public override void Because()
        {
            _cardGrid.Permute();
        }

        [TestMethod]        
        public void CardGrid_HandsValue_Should_Be_A_Double()
        {
            _cardGrid.Score.Is(Type.GetType("System.Double"));
        }
        // is unlikely but valid?
        //[TestMethod]
        //public void CardGrid_HandsValue_Should_Not_Equal_Original_HandsValue()
        //{
        //    _cardGrid.HandsValue.ShouldNotEqual(_intialGridValue);
        //}
    }
}