using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTest;
using SimulatedAnnealing._5CardMatrix;
using SimulatedAnnealing._5CardMatrix.Enumerations;

namespace HandTests.Tests
{
    /// <summary>
    /// Summary description for When_Two_Cards_Are_Compared
    /// </summary>
    [TestClass]
    public class When_Ace_Compared: SpecificationContext
    {
        private Card _card;

        public override void Because()
        {
           _card = new Card(CardFigure.Ace, CardColor.Clubs);
        }

        [TestMethod]
        public void Should_Be_Equal_To_Ace()
        {
            _card.Figure.ShouldEqual(CardFigure.Ace);
        }
        [TestMethod]
        public void Should_Be_Greater_Than_King()
        {
            _card.Figure.ShouldBeGreaterThan(CardFigure.King);
        }
    }

    /// <summary>
    /// Summary description for When_Two_Cards_Are_Compared
    /// </summary>
    [TestClass]
    public class When_Ace_Of_Clubs_Created_With_AC_String_Representation : SpecificationContext
    {
        private Card _card;

        public override void Because()
        {
            _card = Card.StringToCard("AC");
        }

        [TestMethod]
        public void Should_Be_Equal_To_Ace()
        {
            _card.Figure.ShouldEqual(CardFigure.Ace);
        }
        [TestMethod]
        public void Should_Be_A_Club()
        {
            _card.Color.ShouldEqual(CardColor.Clubs);
        }
    }
    /// <summary>
    /// Summary description for When_Two_Cards_Are_Compared
    /// </summary>
    [TestClass]
    public class When_Two_Of_Diamonds_Created_With_2D_String_Representation : SpecificationContext
    {
        private Card _card;

        public override void Because()
        {
            _card = Card.StringToCard("2D");
        }

        [TestMethod]
        public void Should_Be_Equal_To_Two()
        {
            _card.Figure.ShouldEqual(CardFigure.Two);
        }
        [TestMethod]
        public void Should_Be_A_Diamond()
        {
            _card.Color.ShouldEqual(CardColor.Diamonds);
        }
    }
    /// <summary>
    /// Summary description for When_Two_Cards_Are_Compared
    /// </summary>
    [TestClass]
    public class When_Ten_Of_Hearts_Created_With_10H_String_Representation : SpecificationContext
    {
        private Card _card;

        public override void Because()
        {
            _card = Card.StringToCard("10H");
        }

        [TestMethod]
        public void Should_Be_Equal_To_Ten()
        {
            _card.Figure.ShouldEqual(CardFigure.Ten);
        }
        [TestMethod]
        public void Should_Be_A_Hearts()
        {
            _card.Color.ShouldEqual(CardColor.Hearts);
        }
    }   
}
