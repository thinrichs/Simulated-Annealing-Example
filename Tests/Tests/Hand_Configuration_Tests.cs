using System.Collections.Generic;
using FiveCardMatrix;
using FiveCardMatrix.Enumerations;
using FiveCardMatrix.Interfaces;
using FiveCardMatrix.Services;
using FiveCardMatrix.classes;
using NUnit.Framework;

namespace PokerTest
{   
    /// <summary>
    /// Summary description for When_Hand_With_Two_Same_Number_Cards_Is_Evaluated
    /// </summary>
    [TestFixture]
    public class When_5_Cards_Contain_Two_Cards_With_Same_Number : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;
        private HandFinder _handFinder;

        public override void CreateContext()
        {
            _handFinder = new HandFinder();
            _cards = new List<Card>{new Card(CardFigure.Ace,CardColor.Clubs),
                                    new Card(CardFigure.Ace,CardColor.Hearts)};

            // the other cards don't matter for this test except they have to exist for the hand to be valid.
            _cards.Add(new Card(CardFigure.King, CardColor.Clubs));
            _cards.Add(new Card(CardFigure.Queen, CardColor.Hearts));
            _cards.Add(new Card(CardFigure.Jack, CardColor.Diamonds));            
        }

        public override void Because()
        {
            _hand = _handFinder.FindBestHand(_cards);
        } 

        [Test]
        public void Hand_Should_Be_Evaluated_As_One_Pair()
        {
            _hand.ShouldEqual(Hand.OnePair);
        }

        [Test]
        public void Hand_Value_Should_Be_Less_Than_TwoPair()
        {
            new Hand(_cards).Value.ShouldBeLessThan(Hand.TwoPair.BaseValue);
        }
    }

    /// <summary>
    /// Summary description for When_Hand_With_Two_Same_Number_Cards_Is_Evaluated
    /// </summary>
    [TestFixture]
    public class When_5_Cards_Contain_Two_Sets_Of_Two_Cards_With_Same_Number : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;
        private HandFinder _handFinder;

        public override void CreateContext()
        {
            _handFinder = new HandFinder();
            _cards = new List<Card>{new Card(CardFigure.Ace,CardColor.Clubs),
                                    new Card(CardFigure.Ace,CardColor.Hearts),
                                    new Card(CardFigure.King,CardColor.Clubs),
                                    new Card(CardFigure.King,CardColor.Hearts),
                                   };

            // the other cards don't matter for this test except they have to exist for the hand to be valid.
            _cards.Add(new Card(CardFigure.Queen, CardColor.Clubs));
        }

        public override void Because()
        {
            _hand = _handFinder.FindBestHand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_Two_Pair()
        {
            _hand.ShouldEqual(Hand.TwoPair);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_ThreeOfAKind()
        {
            new Hand(_cards).Value.ShouldBeLessThan(Hand.ThreeOfAKind.BaseValue);
        }
    }

    /// <summary>
    /// Summary description for When_Hand_With_Two_Same_Number_Cards_Is_Evaluated
    /// </summary>
    [TestFixture]
    public class When_5_Cards_Contain_Three_Cards_With_Same_Number : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;

        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Ace,CardColor.Clubs),
                                    new Card(CardFigure.Ace,CardColor.Hearts),
                                    new Card(CardFigure.Ace,CardColor.Diamonds)
                                   };

            // the other cards don't matter for this test except they have to exist for the hand to be valid.
            _cards.Add(new Card(CardFigure.King, CardColor.Clubs));
            _cards.Add(new Card(CardFigure.Queen, CardColor.Hearts));
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_ThreeOfAKind()
        {
            _hand.BaseValue.ShouldEqual(Hand.ThreeOfAKind.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_Straight()
        {
            _hand.Value.ShouldBeLessThan(Hand.Straight.BaseValue);
        }
    }

    /// <summary>
    /// Summary description for When_Hand_With_Two_Same_Number_Cards_Is_Evaluated
    /// </summary>
    [TestFixture]
    public class When_5_Cards_Are_A_Set_Of_Three_And_A_Set_Of_Two : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;

        public override void CreateContext()
        {            
            _cards = new List<Card>{new Card(CardFigure.Ace,CardColor.Clubs),
                                    new Card(CardFigure.Ace,CardColor.Hearts),
                                    new Card(CardFigure.Ace,CardColor.Diamonds),
                                    new Card(CardFigure.King, CardColor.Clubs),
                                    new Card(CardFigure.King, CardColor.Hearts)
                                   };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_FullHouse()
        {
            _hand.BaseValue.ShouldEqual(Hand.FullHouse.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_FourOfAKind()
        {
            _hand.Value.ShouldBeLessThan(Hand.FourOfAKind.BaseValue);
        }
    }
    [TestFixture]
    public class When_5_Cards_Are_Same_Suit : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;
       

        public override void CreateContext()
        {
        
            _cards = new List<Card>{new Card(CardFigure.Nine,CardColor.Clubs),
                                    new Card(CardFigure.Jack,CardColor.Clubs),
                                    new Card(CardFigure.Queen,CardColor.Clubs),
                                    new Card(CardFigure.King, CardColor.Clubs),
                                    new Card(CardFigure.Ace, CardColor.Clubs)
                                   };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_Flush()
        {
            _hand.BaseValue.ShouldEqual(Hand.Flush.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_FullHouse()
        {
            _hand.Value.ShouldBeLessThan(Hand.FullHouse.BaseValue);
        }
    }
    [TestFixture]
    public class When_5_Cards_Are_Same_Suit_And_Ordered : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;
   
        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Ten,CardColor.Clubs),
                                    new Card(CardFigure.Jack,CardColor.Clubs),
                                    new Card(CardFigure.Queen,CardColor.Clubs),
                                    new Card(CardFigure.King, CardColor.Clubs),
                                    new Card(CardFigure.Ace, CardColor.Clubs)
                                   };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_StraightFlush()
        {
            _hand.BaseValue.ShouldEqual(Hand.StraightFlush.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_StraightFlush_Plus_One()
        {
            _hand.Value.ShouldBeLessThan(Hand.StraightFlush.BaseValue + 1);
        }
    }
    [TestFixture]
    public class When_5_Cards_Are_Ordered_And_Dont_Contain_Ace : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;
        
        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Five,CardColor.Clubs),
                                    new Card(CardFigure.Four,CardColor.Clubs),
                                    new Card(CardFigure.Three, CardColor.Clubs),
                                    new Card(CardFigure.Six, CardColor.Hearts)
                                   };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_Straight()
        {
            _hand.BaseValue.ShouldEqual(Hand.Straight.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_Flush()
        {
            _hand.Value.ShouldBeLessThan(Hand.Flush.BaseValue);
        }
    }
    [TestFixture]
    public class When_5_Cards_Are_Ordered_And_Contain_Ace_As_Low_Card : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;

        public override void CreateContext()
        {            
            _cards = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Five,CardColor.Clubs),
                                    new Card(CardFigure.Four,CardColor.Clubs),
                                    new Card(CardFigure.Three, CardColor.Clubs),
                                    new Card(CardFigure.Ace, CardColor.Hearts)
                                   };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_Straight()
        {
            _hand.BaseValue.ShouldEqual(Hand.Straight.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_Flush()
        {
            _hand.Value.ShouldBeLessThan(Hand.Flush.BaseValue);
        }
    }
    [TestFixture]
    public class When_5_Cards_Are_Ordered_And_Contain_Ace_As_High_Card : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;        

        public override void CreateContext()
        {           
            _cards = new List<Card>{new Card(CardFigure.Ten,CardColor.Clubs),
                                    new Card(CardFigure.Jack,CardColor.Clubs),
                                    new Card(CardFigure.Queen,CardColor.Clubs),
                                    new Card(CardFigure.King, CardColor.Clubs),
                                    new Card(CardFigure.Ace, CardColor.Hearts)
                                   };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_Straight()
        {
            _hand.BaseValue.ShouldEqual(Hand.Straight.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_Flush()
        {
            _hand.Value.ShouldBeLessThan(Hand.Flush.BaseValue);
        }
    }

    //Queen of Dia    Jack of Spad    King of Diam    Ace of Diamo    Two of Clubs
    // this test was actually found in the wild
    [TestFixture]
    public class When_5_Cards_Are_Ordered_And_Contain_Ace_As_High_Card_But_Two_As_Low_Card : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;
        
        public override void CreateContext()
        {        
            _cards = new List<Card>{new Card(CardFigure.Queen,CardColor.Diamonds),
                                    new Card(CardFigure.Jack,CardColor.Spades),
                                    new Card(CardFigure.King,CardColor.Diamonds),
                                    new Card(CardFigure.Ace, CardColor.Diamonds),
                                    new Card(CardFigure.Two, CardColor.Clubs)
                                   };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_High_Card()
        {
            _hand.BaseValue.ShouldEqual(Hand.HighCard.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_Pair()
        {
            _hand.Value.ShouldBeLessThan(Hand.OnePair.BaseValue);
        }
    }
    /// <summary>
    /// Summary description for When_Hand_With_Two_Same_Number_Cards_Is_Evaluated
    /// </summary>
    [TestFixture]
    public class When_5_Cards_Contain_Four_Cards_With_Same_Number : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;

        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Ace,CardColor.Clubs),
                                    new Card(CardFigure.Ace,CardColor.Hearts),
                                    new Card(CardFigure.Ace,CardColor.Diamonds),
                                    new Card(CardFigure.Ace,CardColor.Spades)
                                   };

            // the other cards don't matter for this test except they have to exist for the hand to be valid.            
            _cards.Add(new Card(CardFigure.King, CardColor.Hearts));
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_FourOfAKind()
        {
            _hand.BaseValue.ShouldEqual(Hand.FourOfAKind.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_StraightFlush()
        {
            _hand.Value.ShouldBeLessThan(Hand.StraightFlush.BaseValue);
        }
    }

    /// <summary>
    /// Summary description for When_Hand_With_Two_Same_Number_Cards_Is_Evaluated
    /// </summary>
    [TestFixture]
    public class When_5_Cards_Contain_No_Sets_By_Color_Figure_Or_Order : SpecificationContext
    {
        private Hand _hand;
        private IList<Card> _cards;

        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Two,CardColor.Clubs),
                                    new Card(CardFigure.Three,CardColor.Hearts),
                                    new Card(CardFigure.Seven, CardColor.Clubs),
                                    new Card(CardFigure.Six, CardColor.Hearts),
                                    new Card(CardFigure.Four, CardColor.Diamonds) };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }

        [Test]
        public void Hand_Should_Be_Evaluated_As_HighCard()
        {
            _hand.BaseValue.ShouldEqual(Hand.HighCard.BaseValue);
        }
    }
    [TestFixture]
    public class When_Hand_Contains_High_Card : SpecificationContext
    {
        //Nine of Spades  Ace of Clubs    Five of Hearts  Jack of Diamon  Eight of Diamo
        private Hand _hand;
        private IList<Card> _cards;

        public override void CreateContext()
        {
            _cards = new List<Card>{new Card(CardFigure.Nine,CardColor.Spades),
                                    new Card(CardFigure.Ace,CardColor.Clubs),
                                    new Card(CardFigure.Five, CardColor.Hearts),
                                    new Card(CardFigure.Jack, CardColor.Diamonds),
                                    new Card(CardFigure.Eight, CardColor.Diamonds) };
        }

        public override void Because()
        {
            _hand = new Hand(_cards);
        }
        [Test]
        public void Hand_Should_Be_High_Card()
        {
            _hand.BaseValue.ShouldEqual(Hand.HighCard.BaseValue);
        }
        [Test]
        public void Hand_Value_Should_Be_Less_Than_Pair()
        {
            _hand.Value.ShouldBeLessThan(Hand.OnePair.BaseValue);
        }
    }
}
