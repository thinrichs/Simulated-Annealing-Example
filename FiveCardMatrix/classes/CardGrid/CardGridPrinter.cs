using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiveCardMatrix.Services;

namespace FiveCardMatrix.classes
{
    class CardGridPrinter
    {
        private static void PrintCardsTo(StringBuilder sb, IList<Card> cards)
        {
            for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++)
            {
                sb.Append(String.Format("{0,12}\t", cards[cardIndex]));
            }
           // sb.AppendLine();
        }

        public static string PrintCardGrid(CardGrid grid)
        {
            StringBuilder sb = new StringBuilder();
            for (int startIndex = 0; startIndex <= grid.Cards.Count; startIndex += grid.LengthOfSide)
            {
                // print cards one row at a time
                PrintCardsTo(sb, grid.Cards
                                .Skip<Card>(startIndex)
                                .Take<Card>(grid.LengthOfSide)
                                .ToList<Card>());
            }
            sb.AppendLine();
            sb.Append("Observed Hands");
            sb.AppendLine();
            Hand col;
            Hand row;
            Hand diag;
            for (int handIndex = 0; handIndex < grid.LengthOfSide; handIndex++)
            {
                col = grid.Column(handIndex);
                row = grid.Row(handIndex);
                diag = grid.Diagonal(handIndex);

                sb.AppendLine();
                sb.AppendLine(String.Format("Columm {0}\t- {1,15}\t\t- Point Value {2}", handIndex, col, col.Value));
                sb.AppendLine(String.Format("Row {0}\t\t- {1,15}\t\t- Point Value {2}", handIndex, row, row.Value));
                if (handIndex < 2)
                {
                    sb.AppendLine(String.Format("Diagonal {0}\t- {1,15}\t\t- Point Value {2}", handIndex, diag, diag.Value));
                }
                PrintCardsTo(sb, col.Cards);
                PrintCardsTo(sb, row.Cards);

                // there are only two diagonal values for a hand
                if (handIndex < 2)
                {
                    PrintCardsTo(sb, diag.Cards);
                }
                sb.AppendLine();
            }

            sb.AppendLine(String.Format("For a point value of {0}", grid.Score));
            sb.AppendLine();
            return sb.ToString();
        }
    }
}