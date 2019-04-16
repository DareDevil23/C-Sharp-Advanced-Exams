using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.Crossroads
{
    public class Program
    {
        public static void Main()
        {
            int greenLightDuration = int.Parse(Console.ReadLine());
            int freeWindowDuration = int.Parse(Console.ReadLine());

            int greenLightSecondsLeft = greenLightDuration;
            int freeWindowsSecondsLeft = freeWindowDuration;

            Queue<string> carsWaiting = new Queue<string>();
            int carsPassed = 0;

            string inputLine = string.Empty;
            while ((inputLine = Console.ReadLine()) != "END")
            {
                if (inputLine != "green")
                {
                    carsWaiting.Enqueue(inputLine);
                    continue;
                }

                greenLightSecondsLeft = greenLightDuration;

                while (greenLightSecondsLeft > 0 && carsWaiting.Any())
                {
                    string currentCar = carsWaiting.Dequeue();

                    if (currentCar.Length <= greenLightSecondsLeft)
                    {
                        greenLightSecondsLeft -= currentCar.Length;
                        carsPassed++;
                    }
                    else
                    {
                        int notPassedSymbols = currentCar.Length - greenLightSecondsLeft;
                        greenLightSecondsLeft = 0;

                        char[] carSymbolsInCrossroad = currentCar.ToCharArray().Skip(currentCar.Length - notPassedSymbols).ToArray();

                        Queue<char> carSymbolsLeft = new Queue<char>(carSymbolsInCrossroad);

                        while (carSymbolsLeft.Any())
                        {
                            if (freeWindowsSecondsLeft == 0 && carSymbolsLeft.Any())
                            {
                                Console.WriteLine($"A crash happened!{Environment.NewLine}{currentCar} was hit at {carSymbolsLeft.First()}.");
                                Environment.Exit(0);
                            }

                            carSymbolsLeft.Dequeue();
                            freeWindowsSecondsLeft--;
                        }

                        carsPassed++;
                    }

                }
            }

            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{carsPassed} total cars passed the crossroads.");
        }
    }
}
