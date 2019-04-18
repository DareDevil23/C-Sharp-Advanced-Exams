using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.TravelMap
{
    public class Program
    {
        private static Dictionary<string, Dictionary<string, long>> travelCosts;

        public static void Main()
        {
            string inputLine;

            travelCosts = new Dictionary<string, Dictionary<string, long>>();

            while ((inputLine = Console.ReadLine()) != "END")
            {
                string[] tokens = inputLine.Split(new [] {'>'}, StringSplitOptions.RemoveEmptyEntries).ToArray();

                string country = tokens[0].Trim();
                string town = tokens[1].Trim();
                long cost = long.Parse(tokens[2].Trim());

                FillDictionaryWithData(country, town, cost); 
            }

            PrintSortedDictionaryEntries();
        }

        private static void PrintSortedDictionaryEntries()
        {
            foreach (var pair in travelCosts.OrderBy(x => x.Key))
            {
                string country = pair.Key;

                var innerDictionary = pair.Value.OrderBy(x => x.Value);

                string townsWithCost = string.Empty;
                foreach (var innerPair in innerDictionary)
                {
                    townsWithCost += $" {innerPair.Key} -> {innerPair.Value}";
                }

                Console.WriteLine($"{country} ->{townsWithCost}");
            }
        }

        private static void FillDictionaryWithData(string country, string town, long cost)
        {
            if (!travelCosts.ContainsKey(country))
            {
                travelCosts.Add(country, new Dictionary<string, long>());
            }

            if (!travelCosts[country].ContainsKey(town))
            {
                travelCosts[country].Add(town, cost);
            }
            else
            {
                var costEntry = travelCosts[country][town];
                if (cost < costEntry)
                {
                    travelCosts[country][town] = cost;
                }
            }
        }
    }
}
