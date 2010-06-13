using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using PokerTest;
using SimulatedAnnealing._5CardMatrix.classes;
using NUnit.Framework;

namespace HandTests.Tests
{
    /// <summary>
    /// Summary description for RealWorld_Grid_Test
    /// </summary>
    [TestFixture]
    public class When_CardGrid_Is_Permuted : SpecificationContext
    {
        private CardGrid _cardGrid;
      
        public override void CreateContext()
        {
            _cardGrid = new CardGrid(5);            
        }


        public override void Because()
        {
            _cardGrid.Permute();
        }

        [Test]        
        public void CardGrid_HandsValue_Should_Be_A_Double()
        {
            _cardGrid.Score.Is(Type.GetType("System.Double"));
        }
        // is unlikely but valid?
        //[Test]
        //public void CardGrid_HandsValue_Should_Not_Equal_Original_HandsValue()
        //{
        //    _cardGrid.HandsValue.ShouldNotEqual(_intialGridValue);
        //}
    }
}