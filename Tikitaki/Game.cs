namespace Tikitaki
{
    internal class Game
    {
        public static int Size { get => 3; }

        char[] _arr = new char[9];

        char[] _avatars = new char[2] { 'x', 'o' };

        int _currentInputIndex = 0;

        private char CurrentAvatar => _avatars[_currentInputIndex];

        public Game()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                _arr[i] = '-';
            }
            System.Console.WriteLine("Game");
            RedrawBoard();
            Start();
        } 

        private void Start()
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
                
                if(results.Length != 2)
                {
                    System.Console.WriteLine("Wrong input");
                    continue;
                }
                int x = -1;                
                int y = -1;

                if(!int.TryParse(results[0], out x))
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }
                if(!int.TryParse(results[1], out y))
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }
                if(x < 0 || x >= Size || y < 0 || y >= Size)
                {
                    System.Console.WriteLine("Your value are out of cell size");
                    continue;
                }	

                int res = MakeATurn(x, y);

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
            System.Console.WriteLine("Game is over! Winner is: " + CurrentAvatar);
        }

/*
 * 0 1 2  3 4 5  6 7 8
 * 3 4 2  5 3 1  2 3 5
 */

        private void RotatePlayer()
        {
            _currentInputIndex++; 
            if((int)_currentInputIndex > 1)
            {
                _currentInputIndex = 0;
            }
        }

        public int MakeATurn(int x, int y) 
        {
            if(_arr[GetIndex(x,y)] != '-')
            {
                return -1; 
            }
            _arr[GetIndex(x,y)] = CurrentAvatar;


            bool winCondition = CheckWinCondition();

            RedrawBoard();

            if(winCondition)
            {
                return 1; 
            }
            
            RotatePlayer();

            return 0;
        }

        private int GetIndex(int x, int y) => y * Size + x;
        
        private int GetXFromIndex(int index) => index / Size;

        private int GetYFromIndex(int index) => index % Size;
        
        private bool CheckWinCondition()
        {
           bool win;
           for(int x = 0; x < Size; x++)
           {
                win = CheckVerticalLineWinCondition(x);
                Console.Write("X: " + x + " " + win + "; \n");
                if(win)
                {
                    return true;
                }
           }
            
           for(int y = 0; y < Size; y++)
           {
                win = CheckHorizontalLineWinCondition(y);

                Console.Write("Y: " + y + " " + win + "; \n");
                if(win)
                {
                    return true;
                }
           }

           return CheckDiagonalWinCondition();
        }

        private bool CheckVerticalLineWinCondition(int x)
        {
            int index = GetIndex(x, 0);
            if(_arr[index] == '-')
            {
                return false;
            }

            bool result = true;
            for(int y = 1; y < Size; y++)
            {  
                result &= _arr[GetIndex(x, 0)] == _arr[GetIndex(x, y)];
            }
            return result;
        }
        
        private bool CheckHorizontalLineWinCondition(int y)
        {
            int index = GetIndex(0, y);
            if(_arr[index] == '-')
            {
                return false;
            }

            bool result = true;
            for(int x = 1; x < Size; x++)
            {  
                result &= _arr[GetIndex(0, y)] == _arr[GetIndex(x, y)];
            }
            return result;
        }

        private bool CheckDiagonalWinCondition()
        {
            int index = GetIndex(0, 0);
            if(_arr[index] == '-')
            {
                return false;
            }
            bool result = true; // req: n=m
            for(int i = 1; i < Size; i++)
            {
                result &= _arr[GetIndex(0, 0)] == _arr[GetIndex(i, i)];
            }
            if(result)
            {
                Console.WriteLine("First diagonal: "  + result + "\n");
                return true;
            }
            index = GetIndex(Size - 1, 0);
            if(_arr[index] == '-')
            {
                return false;
            }
            result = true;
            for(int i = 1; i < Size; i++)
            { 
                result &= _arr[GetIndex(Size - 1, 0)] == _arr[GetIndex(Size - 1 - i, i)];
            }
            if(result)
            {
                Console.WriteLine("Second diagonal: "  + result + "\n");
                return true;
            }

            return false;
        }

        private void RedrawBoard()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                if(i != 0 && i % Size == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(_arr[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
