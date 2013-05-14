using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiveCardMatrix.Enumerations;
using System.Collections;

namespace FiveCardMatrix
{
    public class Card
    {
        // how can I move this into a type, dictionary collection with both maps as different entries by type?
        private static Dictionary<String, CardColor> colorMap = new Dictionary<String, CardColor> 
        {
            {"D", CardColor.Diamonds}, {"S", CardColor.Spades}, {"H",CardColor.Hearts}, {"C", CardColor.Clubs}
        };
        private static Dictionary<String, CardFigure> figureMap = new Dictionary<String, CardFigure> 
        {
            { "J", CardFigure.Jack}, {"Q", CardFigure.Queen}, {"K", CardFigure.King}, {"A", CardFigure.Ace}
        };

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
            set
            {
                Figure = ValueToCard(value).Figure; 
                Color = ValueToCard(value).Color;
            }
        }
        private int CardToValue(Card card)
        {
            return (int)Figure + (int)(Color) * 13;
        }
        private Card ValueToCard(int value)
        {
            var remainder = (value % 13 == 0 ? 13 : value % 13);
            var figure = (CardFigure)remainder;
            var color = (CardColor)((value - remainder) / 13);
            return new Card(figure, color);
        }

        private static Tuple<string, string> ParseColorAndFigure(string representation)
        {
            string colorRepresentation = representation.Substring(representation.Length - 1, 1);
            string figureRepresentation = representation.Substring(0, representation.Length - 1);
            return Tuple.Create<string, string>(colorRepresentation, figureRepresentation);
        }

        private static CardFigure AsFigure(string representation)
        {
            CardFigure figure;
            int potentialFigure;
            if (!(int.TryParse(representation, out potentialFigure)))
            {
                if (figureMap.ContainsKey(representation))
                {
                    figure = figureMap[representation];
                }
                else
                {
                    throw new ArgumentException("Can't create a card of unknown figure (number)");
                }
            }
            else
            {
                figure = (CardFigure)(potentialFigure - 1);
            }
            return figure;
        }
        private static CardColor AsColor(string representation)
        {
            CardColor color;
            if (colorMap.ContainsKey(representation))
            {
                color = colorMap[representation];
            }
            else
            {
                throw new ArgumentException("Can't create a card of unknown color (suit)");
            }
            return color;
        }
        public static Card StringToCard(string representation)
        {
            var tuple = ParseColorAndFigure(representation);
            var color = AsColor(tuple.Item1);
            var figure = AsFigure(tuple.Item2);
            return new Card(figure, color);
        }

        public override string ToString()
        {
            var result = String.Format("{0} of {1}", Figure, Color.ToString().Substring(0, 1));
            return result.Substring(0, Math.Min(result.Length, 14));
        }

        public override bool Equals(object obj)
        {
            var match = obj as Card;
            return (match != null) ? Figure.Equals(match.Figure) : false;
        }

        public override int GetHashCode()
        {
            return (int)Color + ((int)Figure * 10) + (Value * 1000);
        }
    }
}