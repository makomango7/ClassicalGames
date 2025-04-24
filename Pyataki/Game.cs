using static Magicboard.Common.CommonExtensions;

/*
 * Next is: 
 * 1. - win conditions
 * 2. - randomizer
 */

namespace Pyataki 
{
    internal class Game
    {
        internal static int Size => 4;

        string[] _arr = new string[Game.Size * Game.Size];


        internal Game()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                _arr[i] = i.ToString(); 
            }
            RedrawBoard();
            HandleInputAndStart();
        }


        private void HandleInputAndStart()
        { 
            Console.WriteLine("Type exit to exit");
            while(true)
            {
                Console.WriteLine("Enter next turn: ");
                string inputLine = Console.ReadLine();
                if(inputLine == "exit")
                {
                    break;
                }

                string[] results = inputLine.Split(',');
                
                if(results.Length != 4)
                {
                    System.Console.WriteLine("Not enough input members");
                    continue;
                }
                int x1 = -1;                
                int y1 = -1;
                int x2 = -1;
                int y2 = -1;

                if(!int.TryParse(results[0], out x1))
                {
                    Console.WriteLine("First arg is not a number");
                    continue;
                }
                if(!int.TryParse(results[1], out y1))
                {
                    Console.WriteLine("Second arg is not a number");
                    continue;
                }
                if(!int.TryParse(results[2], out x2))
                {
                    Console.WriteLine("Third arg is not a number");
                    continue;
                }
                if(!int.TryParse(results[3], out y2))
                {
                    Console.WriteLine("Fourth arg is not a number");
                    continue;
                }
                
                if(!ValidateInputCoords(x1, y1))
                {
                    System.Console.WriteLine("Your x1 and y1 value are out of cell size");
                    continue;
                }

                if(!ValidateInputCoords(x2, y2))
                {
                    System.Console.WriteLine("Your x1 and y1 value are out of cell size");
                    continue;
                }

                int res = MakeATurn(x1, y1, x2, y2);

                if(res == -1)
                {
                    System.Console.WriteLine("Looks like, this cell is captured already");
                    continue;
                }
                else if(res == 1)
                {
                    break;
                }	

            }
        }

        private bool ValidateInputCoords(int x, int y)
        {
            bool res = (x >= 0 && x < Size && y >= 0 && y < Size);
            return res;
        }
        
        private int MakeATurn(int x1, int y1, int x2, int y2) 
        {
            // _arr[GetIndex(x, y, Size)] = CurrentAvatar;

            Swap(x1, y1, x2, y2);


            bool winCondition = CheckWinCondition();

            RedrawBoard();

            if(winCondition)
            {
                return 1; 
            }
            
            RotatePlayer();

            return 0;
        }

        private bool CheckWinCondition()
        {
            return false;
        }

        private void RotatePlayer()
        {
            
        }


        private void StartTest()
        {
            RedrawBoard();
            Swap(0, 0, 0, 1);
            RedrawBoard();
            Swap(0, 0, 0, 2);
            RedrawBoard();
        }

        private void Swap(int x1, int y1, int x2, int y2)
        {
            int xDiff = Math.Abs(x1 - x2);
            int yDiff = Math.Abs(y1 - y2);
            int sumDiff = xDiff + yDiff;

            Console.WriteLine("Sumdiff is: " + sumDiff);

            if(sumDiff != 1)
            {
                System.Console.WriteLine("You can swap only neighboors");
                return;
            }

            int index1 =  GetIndex(x1, y1, Size);
            int index2 =  GetIndex(x2, y2, Size);


            string tmp = _arr[index1];
            _arr[index1] = _arr[index2];
            _arr[index2] = tmp;
        }

        private void RedrawBoard()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                if(i != 0 && i % Size == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(_arr[i] + "\t");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
