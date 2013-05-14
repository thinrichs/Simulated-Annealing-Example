using System.Collections.Generic;
using System.Linq;
using FiveCardMatrix.Enumerations;
using FiveCardMatrix.Services;
using System;
using FiveCardMatrix.interfaces;

namespace FiveCardMatrix.classes
{// I fear this class is getting muddled with many concerns.  Got the idea for this class from the refactoring to patterns book
    public class Hand
    {
        private enum BaseHandValues
        {
            HighCard = 0,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush
        }
        public readonly static Hand HighCard = new Hand(BaseHandValues.HighCard);
        public readonly static Hand OnePair = new Hand(BaseHandValues.OnePair);
        public readonly static Hand TwoPair = new Hand(BaseHandValues.TwoPair);
        public readonly static Hand ThreeOfAKind = new Hand(BaseHandValues.ThreeOfAKind);
        public readonly static Hand Straight = new Hand(BaseHandValues.Straight);
        public readonly static Hand Flush = new Hand(BaseHandValues.Flush);
        public readonly static Hand FullHouse = new Hand(BaseHandValues.FullHouse);
        public readonly static Hand FourOfAKind = new Hand(BaseHandValues.FourOfAKind);
        public readonly static Hand StraightFlush = new Hand(BaseHandValues.StraightFlush);

        public int BaseValue {get; set;}
        public Hand BaseHand {get; private set;}
        
        public double Value {get; set;}
        public IList<Card> Cards { get; private set; }
        private IStateScorer _handScorer;

        private Hand(BaseHandValues baseHandValue) : this((int)baseHandValue) { }
        private Hand(int baseValue)
        {
            BaseValue = baseValue;
        }

        public Hand(IList<Card> cards)
            : this(cards, new MathematicalHandScorer())
        {}

        public Hand(IList<Card> cards, IStateScorer handScorer)
        {
            Cards = cards;
            _handScorer = handScorer;
            if ((handScorer != null) && (cards != null))
            {
                Value = _handScorer.Score(Cards);
            }
            BaseValue = (int)Math.Floor(Value);            
            BaseHand = new HandFinder().FindBestHand(Cards);
        }     
        
        public override string ToString()
        { 
            // switch to use as  <Hand, String> map
            if (BaseHand == Hand.HighCard) { return "High Card"; }
            if (BaseHand == Hand.OnePair) { return "One Pair"; }
            if (BaseHand == Hand.TwoPair) { return "Two Pair"; }
            if (BaseHand == Hand.ThreeOfAKind) { return "Three Of A Kind"; }
            if (BaseHand == Hand.Straight) { return "Straight"; }
            if (BaseHand == Hand.Flush) { return "Flush"; }
            if (BaseHand == Hand.FullHouse) { return "Full House"; }
            if (BaseHand == Hand.FourOfAKind) { return "Four of a Kind"; }
            if (BaseHand == Hand.StraightFlush) { return "Straight Flush"; }
            return base.ToString();
        }
    }
}