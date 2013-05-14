using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using PokerTest;
using FiveCardMatrix;
using FiveCardMatrix.Enumerations;
using FiveCardMatrix.classes;
using NUnit.Framework;

namespace HandTests.Tests
{
    /// <summary>
    /// Summary description for Hand_Value_Tests
    /// </summary>
    // somethings wrong with this tests suite... too large?
    [TestFixture]
    public class When_Hand_Contains_High_Card : SpecificationContext
    {
        private IList<Card> _cardsUnderTest;
        private double _cardsUnderTestHandValue;               
        private IList<Card> _lowerCardHand;
        private double _lowerCardHandValue;
        private IList<Card> _sameHand;
        private double _sameHandValue;
        private IList<Card> _pairHand;
        private double _pairHandValue;

        public override void CreateContext()
        {

            _cardsUnderTest = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Eight,CardColor.Hearts),
                                    new Card(CardFigure.Three, CardColor.Clubs),
                                    new Card(CardFigure.Six, CardColor.Hearts),
                                    new Card(CardFigure.Four, CardColor.Diamonds)};
            
            _lowerCardHand = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Seven,CardColor.Hearts),
                                    new Card(CardFigure.Three, CardColor.Clubs),
                                    new Card(CardFigure.Six, CardColor.Hearts),
                                    new Card(CardFigure.Four, CardColor.Diamonds)};

            _sameHand = new List<Card>{new Card(CardFigure.Two,CardColor.Diamonds),
                                    new Card(CardFigure.Eight,CardColor.Diamonds),
                                    new Card(CardFigure.Three, CardColor.Diamonds),
                                    new Card(CardFigure.Six, CardColor.Spades),
                                    new Card(CardFigure.Four, CardColor.Diamonds)};

            _pairHand = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Two,CardColor.Hearts),
                                    new Card(CardFigure.Three, CardColor.Clubs),
                                    new Card(CardFigure.Six, CardColor.Hearts),
                                    new Card(CardFigure.Four, CardColor.Diamonds)};
           
            _lowerCardHandValue = new Hand(_lowerCardHand).Value;
            _pairHandValue = new Hand(_pairHand).Value;
            _sameHandValue = new Hand(_sameHand).Value;
        }

        public override void Because()
        {
            _cardsUnderTestHandValue =  new Hand(_cardsUnderTest).Value;
        }

        [Test]
        public void Hand_Value_Should_Be_Less_Than_One_Pair()
        {
            _cardsUnderTestHandValue.ShouldBeLessThan(_pairHandValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_Hand_With_Higher_Card()
        {
            _cardsUnderTestHandValue.ShouldBeLessThan(_pairHandValue);
        }
        [Test]
        public void Hand_Value_Should_Be_More_Than_Hand_With_Lower_Card()
        {
            _cardsUnderTestHandValue.ShouldBeGreaterThan(_lowerCardHandValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Same_As_Hand_With_Same_Figures()
        {
            _cardsUnderTestHandValue.ShouldEqual(_sameHandValue);
        }
    }
    [TestFixture]
    public class When_Hand_Contains_Pair : SpecificationContext
    {
        private IList<Card> _cardsUnderTest;
        private double _cardsUnderTestHandValue;

        private IList<Card> _lowerPairHand;
        private double _lowerPairHandValue;

        public override void CreateContext()
        {
            _cardsUnderTest = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Three,CardColor.Hearts),
                                    new Card(CardFigure.Five, CardColor.Clubs),
                                    new Card(CardFigure.Four, CardColor.Hearts),
                                    new Card(CardFigure.Four, CardColor.Diamonds)};


            _lowerPairHand = new List<Card>{new Card(CardFigure.Ace,CardColor.Clubs),
                                    new Card(CardFigure.King,CardColor.Hearts),
                                    new Card(CardFigure.Queen, CardColor.Clubs),
                                    new Card(CardFigure.Three, CardColor.Hearts),
                                    new Card(CardFigure.Three, CardColor.Diamonds)};


            _lowerPairHandValue = new Hand(_lowerPairHand).Value;
        }

        public override void Because()
        {
            _cardsUnderTestHandValue = new Hand(_cardsUnderTest).Value;
        }
        [Test]
        public void Hand_Value_Should_Be_Greater_Than_Lower_Pair_With_Higher_Insignificant_Cards()
        {
            _cardsUnderTestHandValue.ShouldBeGreaterThan(_lowerPairHandValue);
        }
    }
}