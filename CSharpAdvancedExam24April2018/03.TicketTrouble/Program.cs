using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _03.TicketTrouble
{
    public class Program
    {
        public static void Main()
        {
            string travelLocation = Console.ReadLine();

            string suitcaseContent = Console.ReadLine();

            string pattern =
                @"(?<opening>{|\[).*?(?<firstInnerOpen>{|\[)" + travelLocation + @"(?<firstInnerClose>}|\]).*?(?<secondInnerOpen>{|\[)(?<seatNumber>[A-Z]\d{1,}).*?(?<secondInnerClose>}|\]).*?(?<closing>\]|})";

            Regex rgx = new Regex(pattern);

            List<string> validTicketsSeats = new List<string>();

            MatchCollection matches = rgx.Matches(suitcaseContent);

            foreach (Match matchedTicket in matches)
            {

                char outerOpening = matchedTicket.Groups["opening"].Value.First();
                char outerClosing = matchedTicket.Groups["closing"].Value.First();

                char firstInnerOpening = matchedTicket.Groups["firstInnerOpen"].Value.First();
                char firstInnerClosing = matchedTicket.Groups["firstInnerClose"].Value.First();

                char secondInnerOpening = matchedTicket.Groups["secondInnerOpen"].Value.First();
                char secondInnerClosing = matchedTicket.Groups["secondInnerClose"].Value.First();

                if (outerOpening == firstInnerOpening || outerOpening == secondInnerOpening)
                {
                    continue;
                }

                string curlyBrackets = "{}";
                string cubicBrackets = "[]";

                string outerBrackets = outerOpening.ToString() + outerClosing;
                string firstInnerBrackets = firstInnerOpening.ToString() + firstInnerClosing;
                string secondInnerBrackets = secondInnerOpening.ToString() + secondInnerClosing;

                if ((outerBrackets != curlyBrackets && outerBrackets != cubicBrackets)
                    || (firstInnerBrackets != curlyBrackets && firstInnerBrackets != cubicBrackets)
                    || (secondInnerBrackets != curlyBrackets && secondInnerBrackets != cubicBrackets))
                {
                    continue;
                }

                string seat = matchedTicket.Groups["seatNumber"].Value;
                if (seat.Length <= 3)
                {
                    validTicketsSeats.Add(seat);
                }                
            }

            if (validTicketsSeats.Any() )
            {
                if (validTicketsSeats.Count > 2)
                {
                    var groupedSeats = validTicketsSeats.GroupBy(s => s.Substring(1));

                    if (groupedSeats.Any(gr => gr.Count() >= 2))
                    {
                        validTicketsSeats = groupedSeats
                            .Where(gr => gr.Count() >= 2)
                            .Select(gr => gr.ToArray())
                            .First()
                            .ToList();
                    }
                }

                Console.WriteLine(
                    $"You are traveling to {travelLocation} on seats {validTicketsSeats[0]} and {validTicketsSeats[1]}.");
            }
            else
            {
                Console.WriteLine("No valid tickets found!");
            }           
        }
    }
}
