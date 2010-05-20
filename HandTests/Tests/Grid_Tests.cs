using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTest;
using SimulatedAnnealing._5CardMatrix.Enumerations;
using SimulatedAnnealing._5CardMatrix.classes;
using SimulatedAnnealing._5CardMatrix;

namespace HandTests.Tests
{
    /// <summary>
    /// Summary description for Grid_Tests
    /// </summary>
    [TestClass]
    public class When_CardGrid_Instantiated_With_5_Card_Side : SpecificationContext
    {
        private CardGrid _cardGrid;

        public override void Because()
        {
            _cardGrid = new CardGrid(5);
        }

        [TestMethod]
        public void CardGrid_Should_Contain_25_Cards()
        {
            _cardGrid.Cards.Count.ShouldEqual(25);
        }
        [TestMethod]
        public void CardGrid_ToString_Should_Return_String()
        {
            _cardGrid.ToString().Is(Type.GetType("System.String"));
        }

        [TestMethod]
        public void CardGrid_ToString_Should_Not_Be_Null()
        {
            _cardGrid.ToString().ShouldNotBeNull();
        }

        [TestMethod]
        public void CardGrid_ToString_Should_Be_Greater_Than_Count_Of_Cards_Times_Two()
        {
            _cardGrid.ToString().Length.ShouldBeGreater(50);
        }
    }
    /// <summary>
    /// Summary description for Grid_Tests
    /// </summary>
    [TestClass]
    public class When_CardGrid_Instantiated_With_2_Card_Side_And_Cards_Supplied : SpecificationContext
    {
        private CardGrid _cardGrid;
        private List<Card> _cards;

        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Three,CardColor.Hearts),
                                    new Card(CardFigure.Seven, CardColor.Clubs),
                                    new Card(CardFigure.Six, CardColor.Hearts) };
        }

        public override void Because()
        {
            _cardGrid = new CardGrid(2, _cards);
        }

        [TestMethod]
        public void CardGrid_Should_Contain_4_Cards()
        {
            _cardGrid.Cards.Count.ShouldEqual(4);
        }

        [TestMethod]
        public void CardGrid_Should_Contain_Supplied_Cards()
        {
            _cardGrid.Cards.ShouldEqual(_cards);
        }
    }

    /// <summary>
    /// Summary description for Grid_Tests
    /// </summary>
    [TestClass]
    public class When_CardGrid_Instantiated_With_2_Card_Side : SpecificationContext
    {
        private CardGrid _cardGrid;
        private List<Card> _cards;

        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Four,CardColor.Hearts),
                                    new Card(CardFigure.Three, CardColor.Clubs),
                                    new Card(CardFigure.Six, CardColor.Hearts) };
        }

        public override void Because()
        {
            _cardGrid = new CardGrid(2, _cards);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Column_0_Should_Contain_Two_Cards()
        {
            _cardGrid.Column(0).Cards.Count.ShouldEqual(2);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Column_1_Should_Contain_Two_Cards()
        {
            _cardGrid.Column(1).Cards.Count.ShouldEqual(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Row_0_Should_Contain_Two_Cards()
        {
            _cardGrid.Row(0).Cards.Count.ShouldEqual(2);            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Row_1_Should_Contain_Two_Cards()
        {
            _cardGrid.Row(1).Cards.Count.ShouldEqual(2);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Diagonal_0_Should_Contain_Two_Cards()
        {
            _cardGrid.Diagonal(0).Cards.Count.ShouldEqual(2);          
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Diagonal_1_Should_Contain_Two_Cards()
        {
            _cardGrid.Diagonal(1).Cards.Count.ShouldEqual(2);
        }
    }
    /// <summary>
    /// Summary description for Grid_Tests
    /// </summary>
    [TestClass]
    public class When_CardGrid_Instantiated_With_2_Card_SideAs_2C3C4C5C : SpecificationContext
    {
        private CardGrid _cardGrid;
        private List<Card> _cards;

        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Three,CardColor.Clubs),
                                    new Card(CardFigure.Four, CardColor.Clubs),
                                    new Card(CardFigure.Five, CardColor.Clubs) };
        }

        public override void Because()
        {
            _cardGrid = new CardGrid(2, _cards);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Column_0_Should_Contain_2C4C()
        {
            _cardGrid.Column(0).Cards.ShouldUnorderedEqual<Card>(new List<Card> { new Card(CardFigure.Two, CardColor.Clubs), new Card(CardFigure.Four, CardColor.Clubs) });            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Column_1_Should_Contain_3C5C()
        {
            _cardGrid.Column(1).Cards.ShouldUnorderedEqual<Card>(new List<Card> { new Card(CardFigure.Three, CardColor.Clubs), new Card(CardFigure.Five, CardColor.Clubs) });            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Row_0_Should_Contain_2C3C()
        {
            _cardGrid.Row(0).Cards.ShouldUnorderedEqual<Card>(new List<Card> { new Card(CardFigure.Two, CardColor.Clubs), new Card(CardFigure.Three, CardColor.Clubs) });                    
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Row_1_Should_Contain_4C5C()
        {
            _cardGrid.Row(1).Cards.ShouldUnorderedEqual<Card>(new List<Card> { new Card(CardFigure.Four, CardColor.Clubs), new Card(CardFigure.Five, CardColor.Clubs) });            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Diagonal_0_Should_Contain_2C5C()
        {
            _cardGrid.Diagonal(0).Cards.ShouldUnorderedEqual<Card>(new List<Card> { new Card(CardFigure.Two, CardColor.Clubs), new Card(CardFigure.Five, CardColor.Clubs) });            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_Diagonal_1_Should_Contain_3C4C()
        {
            _cardGrid.Diagonal(1).Cards.ShouldUnorderedEqual<Card>(new List<Card> { new Card(CardFigure.Three, CardColor.Clubs), new Card(CardFigure.Four, CardColor.Clubs) });            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CardGrid_HandsValue_Should_Throw_Exception_On_NonRealWorld_Grid()
        {
            _cardGrid.Score.Is(Type.GetType("System.Double"));
        }

     
    }
}
