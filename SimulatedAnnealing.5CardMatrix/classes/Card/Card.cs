using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulatedAnnealing._5CardMatrix.Enumerations;
using System.Collections;

namespace SimulatedAnnealing._5CardMatrix
{
    public class Card
    {
        public CardFigure Figure { get; set; }
        public CardColor Color { get; set; }

        public Card(int value)
        {
            Color = ValueToCard(value).Color;
            Figure = ValueToCard(value).Figure;
        }
        public Card(CardFigure figure, CardColor color)
        {
            Figure = figure;
            Color = color;
        }

        public int Value
        {
            get { return CardToValue(this); }
            set { Figure = ValueToCard(value).Figure; Color = ValueToCard(value).Color; }
        }
        private int CardToValue(Card card)
        {

            return (int)Figure + (int)(Color) * 13;

        }
        private Card ValueToCard(int value)
        {
            /* This function return card that match given value. */

            return new Card((CardFigure)(value % 13 == 0 ? 13 : value % 13), (CardColor)((value - (value % 13 == 0 ? 13 : value % 13)) / 13));
        }

        public static Card StringToCard(string representation)
        {
            string colorRepresentation = representation.Substring(representation.Length - 1, 1);
            string figureRepresentation = representation.Substring(0, representation.Length - 1);
            CardColor color;
            CardFigure figure;

            switch (colorRepresentation)
            {
                case "D": color = CardColor.Diamonds; break;
                case "S": color = CardColor.Spades; break;
                case "H": color = CardColor.Hearts; break;
                case "C": color = CardColor.Clubs; break;
                default: throw new ArgumentException("Can't create a card of unknown color (suit)");
            }
            int potentialFigure;

            if (!(int.TryParse(figureRepresentation, out potentialFigure)))
            {
                switch (figureRepresentation)
                {
                    case "J": figure = CardFigure.Jack; break;
                    case "Q": figure = CardFigure.Queen; break;
                    case "K": figure = CardFigure.King; break;
                    case "A": figure = CardFigure.Ace; break;
                    default: throw new ArgumentException("Can't create a card of unknown figure (number)");
                }
            }
            else
            {
                figure = (CardFigure)potentialFigure - 1;
            }
            return new Card(figure, color);
        }

        public override string ToString()
        { 
            string result = String.Format("{0} of {1}", Figure, Color.ToString().Substring(0, 1));
            return result.Substring(0, Math.Min(result.Length, 14));
        }

        public override bool Equals(object obj)
        {
            Card match = obj as Card;
            return (match != null) ? this.Figure.Equals(match.Figure) : false;
        }

        public override int GetHashCode()
        {
            return (int)Color + ((int)Figure * 10) + (Value * 1000);
        }
    }
}