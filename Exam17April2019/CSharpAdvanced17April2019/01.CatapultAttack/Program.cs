using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.CatapultAttack
{
    public class Program
    {
        public static void Main()
        {
            int pilesAmount = int.Parse(Console.ReadLine());
            int[] wallsValues = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            Queue<int> walls = new Queue<int>(wallsValues);

            int wave = 0;
            bool shouldTakeNewWall = false;
            int bonusWall;
            Stack<int> rocks = null;

            for (int i = 0; i < pilesAmount; i++)
            {               
                
                int[] rocksValues = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                rocks = new Stack<int>(rocksValues);
                wave++;

                if (wave - 3 == 0)
                {
                    bonusWall = int.Parse(Console.ReadLine());
                    walls.Enqueue(bonusWall);
                    wave = 0;
                }


                while (rocks.Any() && walls.Any())
                {
                    int currentRock = rocks.Peek();
                    int currentWall = walls.Peek();

                    if (currentRock > currentWall)
                    {
                        walls.Dequeue();

                        currentRock -= currentWall;
                        rocks.Pop();
                        rocks.Push(currentRock);
                    }
                    else if (currentWall > currentRock)
                    {
                        rocks.Pop();

                        currentWall -= currentRock;

                        walls.Dequeue();

                        List<int> newWallsValues = new List<int> {currentWall};
                        newWallsValues.AddRange(walls.ToArray());

                        walls = new Queue<int>(newWallsValues);
                    }
                    else
                    {
                        rocks.Pop();
                        walls.Dequeue();
                    }
                }

                if (i == pilesAmount - 1 && wave - 3 == 0)
                {
                    bonusWall = int.Parse(Console.ReadLine());
                    walls.Enqueue(bonusWall);
                }

                if (!walls.Any())
                {
                    break;
                }
            }

            string output = walls.Any()
                ? "Walls left: " + string.Join(", ", walls)
                : "Rocks left: " + string.Join(", ", rocks);

            Console.WriteLine(output);
        }
    }
}
