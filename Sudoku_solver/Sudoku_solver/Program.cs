using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public class UnknownPosition
    {
        public UnknownPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDeleted { get; set; }
    }

    class Program
    {
        //static List<List<int>> arr = new List<List<int>>()
        //{
        //    new List<int>(){5,3,0,0,7,0,0,0,0},
        //    new List<int>(){6,0,0,1,9,5,0,0,0},
        //    new List<int>(){0,9,8,0,0,0,0,6,0},
        //    new List<int>(){8,0,0,0,6,0,0,0,3},
        //    new List<int>(){4,0,0,8,0,3,0,0,1},
        //    new List<int>(){7,0,0,0,2,0,0,0,6},
        //    new List<int>(){0,6,0,0,0,0,2,8,0},
        //    new List<int>(){0,0,0,4,1,9,0,0,5},
        //    new List<int>(){0,0,0,0,8,0,0,7,9},
        //};

        static List<List<int>> arr = new List<List<int>>()
        {
            new List<int>(){4,0,2,0,6,5,9,3,7},
            new List<int>(){0,0,9,0,0,3,0,0,2},
            new List<int>(){5,0,7,0,2,0,0,0,0},
            new List<int>(){0,9,0,6,3,0,0,0,0},
            new List<int>(){0,4,1,0,0,0,6,8,0},
            new List<int>(){0,0,0,0,8,2,0,9,0},
            new List<int>(){0,0,0,0,9,0,3,0,6},
            new List<int>(){7,0,0,1,0,0,8,0,0},
            new List<int>(){9,6,8,3,4,0,2,0,5},
        };

        static void Main(string[] args)
        {
            Console.WriteLine("*******ПЕРВЫЙ СУДОКУ*******");
            WriteArr();

            List<UnknownPosition> unknownPositions = new List<UnknownPosition>();

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (arr[y][x] == 0)
                    {
                        var number = GetNumber(x, y);
                        if (number == null)
                        {
                            unknownPositions.Add(new UnknownPosition(x, y));
                        }
                        else
                        {
                            arr[y][x] = number.Value;
                        }
                    }
                }
            }

            while(!IsComplited(unknownPositions))
            {
                foreach (var unknownPosition in 
                    unknownPositions
                    .Where(x => !x.IsDeleted)
                    .ToList())
                {
                    var number = GetNumber(unknownPosition.X, unknownPosition.Y);
                    if (number == null)
                    {
                        continue;
                    }
                    else
                    {
                        arr[unknownPosition.Y][unknownPosition.X] = number.Value;
                        unknownPosition.IsDeleted = true;
                    }
                }
            }

            Console.WriteLine("*******РЕШЕНИЕ*******");
            WriteArr();
            Console.ReadKey();
        }

        static int? GetNumber(int x, int y)
        {
            var blockNumbers = GetBlockNumbers(x, y);

            for (int i = 0; i < 9; i++)
            {
                blockNumbers.Add(arr[y][i]);
                blockNumbers.Add(arr[i][x]);
            }

            int? result = null;

            for (int i = 1; i < 10; i++)
            {
                if (!blockNumbers.Any(x => x == i))
                {
                    if (result.HasValue)
                    {
                        return null;
                    }
                    else
                    {
                        result = i;
                    }
                }
            }

            return result;
        }

        static List<int> GetBlockNumbers(int x, int y)
        {
            var blockNumbers = new List<int>();

            var xBlock = (int)Math.Truncate((decimal)x / 3) + 1;
            var xMainPosition = GetMainPosition(xBlock);
            var yBlock = (int)Math.Truncate((decimal)y / 3) + 1;
            var yMainPosition = GetMainPosition(yBlock);

            for (int i = xMainPosition; i < xMainPosition+3; i++)
            {
                for (int j = yMainPosition; j < yMainPosition+3; j++)
                {
                    blockNumbers.Add(arr[j][i]);
                }
            }

            return blockNumbers;
        }

        static int GetMainPosition(int block)
        {
            switch (block)
            {
                case 1:
                case 0:
                    return 0;
                case 2:
                    return 3;
                case 3:
                    return 6;
            }
            return 0;
        }

        static bool IsComplited(List<UnknownPosition> unknownPositions)
        {
            if (unknownPositions.Any(x => !x.IsDeleted))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static void WriteArr()
        {
            for (int i = 0; i < 9; i++)
            {
                arr[i].ForEach(j => Console.Write(j + " | "));
                Console.WriteLine();
                Console.WriteLine("---------------------------------");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    
}
