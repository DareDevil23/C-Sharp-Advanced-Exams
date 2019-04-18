using System;

namespace _02.ThroneConquering
{
    public class Program
    {
        private static char[][] matrix;

        private static int energy;
        private static int parisRow;
        private static int parisCol;
        private static bool setOnThrone;

        public static void Main()
        {
            energy = int.Parse(Console.ReadLine());

            int rowsCount = int.Parse(Console.ReadLine());

            FillMatrix(rowsCount);

            (parisRow, parisCol) = FindParisCoordinates();

            setOnThrone = false;
            while (energy > 0 && !setOnThrone)
            {
                string inputLine = Console.ReadLine();
                string[] tokens = inputLine.Split(' ');

                string command = tokens[0];
                int spawnRow = int.Parse(tokens[1]);
                int spawnCol = int.Parse(tokens[2]);

                matrix[spawnRow][spawnCol] = 'S';

                MoveParis(command);
            }

            if (setOnThrone)
            {
                Console.WriteLine($"Paris has successfully sat on the throne! Energy left: {energy}");
                PrintMatrix();
            }

            
        }

        private static void MoveParis(string command)
        {
            bool canMove = false;

            int newParisRow = parisRow;
            int newParisCol = parisCol;

            switch (command)
            {                    
                case "up":
                    canMove = CanMoveToCell(--newParisRow, newParisCol);
                    break;
                case "down":
                    canMove = CanMoveToCell(++newParisRow, newParisCol);
                    break;
                case "left":
                    canMove = CanMoveToCell(newParisRow, --newParisCol);
                    break;
                case "right":
                    canMove = CanMoveToCell(newParisRow, ++newParisCol);
                    break;
            }

            energy--;
            if (energy <= 0)
            {
                matrix[parisRow][parisCol] = '-';
                PrintDeath(newParisRow, newParisCol);
            }

            if (canMove)
            {
                matrix[parisRow][parisCol] = '-';

                char symbolOnCell = matrix[newParisRow][newParisCol];

                if (symbolOnCell == 'H')
                {
                    matrix[newParisRow][newParisCol] = '-'; //the throne disappear
                    setOnThrone = true;
                }
                if (symbolOnCell == 'S')
                {
                    energy -= 2;
                    if (energy <= 0)
                    {                       
                        PrintDeath(newParisRow, newParisCol);
                    }
                    else
                    {
                        SetParisToNewCell(newParisRow, newParisCol);
                    }
                }

                if (symbolOnCell == '-')
                {
                    SetParisToNewCell(newParisRow, newParisCol);
                }

                //TODO: check change the new cell to new symbol if necessery
                //TODO: check change paris row and col to new values
            }

        }

        private static void PrintDeath(int row, int col)
        {
            matrix[row][col] = 'X';
            Console.WriteLine($"Paris died at {row};{col}.");
            PrintMatrix();
            Environment.Exit(0);
        }

        private static void SetParisToNewCell(int newParisRow, int newParisCol)
        {
            matrix[newParisRow][newParisCol] = 'P';
            parisRow = newParisRow;
            parisCol = newParisCol;
        }

        private static bool CanMoveToCell(int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }

        private static (int, int) FindParisCoordinates()
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 'P')
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }

        private static void FillMatrix(int rowsCount)
        {
            matrix = new char[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                matrix[i] = Console.ReadLine().ToCharArray();
            }
        }

        private static void PrintMatrix()
        {
            foreach (var matrixLine in matrix)
            {
                Console.WriteLine(string.Join("", matrixLine));
            }
        }
    }
}
