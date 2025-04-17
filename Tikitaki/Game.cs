namespace Tikitaki
{
    internal class Game
    {
        char[] _arr = new char[9];
        bool _win;

        char[] _avatars = new char[2] { 'x', 'o' };
        int _currentInput;

        public Game()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                _arr[i] = '-';
            }
            System.Console.WriteLine("Game");
            Out();

            _currentInput = 0;

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
                int x = int.Parse(results[0]);
                int y = int.Parse(results[1]);

                if(x < 0 || x >= 3 || y < 0 || y >= 3)
                {
                    System.Console.WriteLine("Your value are out of cell size");
                    continue;
                }	

                int res = MakeATurn(x,y, CurrentAvatar);

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


        private char CurrentAvatar => _avatars[_currentInput];

        private void RotatePlayer()
        {
            _currentInput++; 
            if((int)_currentInput > 1)
            {
                _currentInput = 0;
            }
        }

        public int MakeATurn(int x, int y, char character) 
        {
            if(_arr[GetIndex(x,y)] != '-')
            {
                return -1; 
            }
            _arr[GetIndex(x,y)] = character;


            bool winCondition = CheckWinCondition();

            Out();


            if(winCondition)
            {
                return 1; 
            }
            
            RotatePlayer();

            return 0;
        }

        private int GetIndex(int x, int y) => y * 3 + x;
        
        private int GetXFromIndex(int index) => index / 3;

        private int GetYFromIndex(int index) => index % 3;
        
        private bool CheckWinCondition()
        {
           bool win;
           for(int x = 0; x < 3; x++)
           {
                win = CheckVerticalLineWinCondition(x);
                Console.Write("X: " + x + " " + win + "; \n");
                if(win)
                {
                    return true;
                }
           }
            
           for(int y = 0; y < 3; y++)
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
            for(int y = 1; y < 3; y++)
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
            for(int x = 1; x < 3; x++)
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
            for(int i = 1; i < 3; i++)
            {
                result &= _arr[GetIndex(0, 0)] == _arr[GetIndex(i, i)];
            }
            if(result)
            {
                Console.WriteLine("First diagonal: "  + result + "\n");
                return true;
            }
            index = GetIndex(2, 0);
            if(_arr[index] == '-')
            {
                return false;
            }
            result = true;
            for(int i = 1; i < 3; i++)
            { 
                result &= _arr[GetIndex(2, 0)] == _arr[GetIndex(3 - 1 - i, i)];
            }
            if(result)
            {
                Console.WriteLine("Second diagonal: "  + result + "\n");
                return true;
            }

            return false;
        }

        private void Out()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                if(i != 0 && i % 3 == 0)
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
