using static Magicboard.Common.CommonExtensions;

namespace Magicboard.Tikitaki
{
    public class Game
    {
        char[] _arr = new char[9];
        char[] _avatars = new char[3] { '-', 'x', 'o' };
        int _currentInputIndex = 1;
        
        public int GameRes { get; private set; }
        public int Size { get => 3; }
        public char CurrentAvatar { get => _avatars[_currentInputIndex]; }
        public char[] Arr { get => _arr; }

        public Game()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                _arr[i] = '-';
            }
            System.Console.WriteLine("Game");
        } 

/*
 * Все просто, как только происходит один из
 * вид-кондишинов - игра тут же завершается.
 * Статусные поля переводятся в соответствующие состояния.
 */
        
        public TurnState MakeATurn(int x, int y) 
        {
            if(_arr[GetIndex(x, y, Size)] != '-')
            {
                return TurnState.WrongInput;  // cell should be not captured! 
            }
            _arr[GetIndex(x, y, Size)] = CurrentAvatar;

            if(CheckWinCondition())
            {
                return TurnState.GameFinshed; // game is won by somebody 
            }
            if(CheckNoEmptyCellsCondition())
            {
                _currentInputIndex = 0;
                return TurnState.GameFinshed; 
            }

            RotatePlayer();
            
            return TurnState.NormalTurn; // normal day, normal turn 
        }

/*
 * 0 1 2  3 4 5  6 7 8
 * 3 4 2  5 3 1  2 3 5
 */

        private void RotatePlayer()
        {
            _currentInputIndex++; 
            if((int)_currentInputIndex > 2)
            {
                _currentInputIndex = 1;
            }
        }

        #region Check Conditions 

        private bool CheckNoEmptyCellsCondition()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                if(_arr[i] == '-')
                { 
                    return false;        // there is still an '-' cell!
                }
            }
            return true;
        }

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
            int index = GetIndex(x, 0, Size);
            if(_arr[index] == '-')
            {
                return false;
            }

            bool result = true;
            for(int y = 1; y < Size; y++)
            {  
                result &= _arr[GetIndex(x, 0, Size)] == _arr[GetIndex(x, y, Size)];
            }
            return result;
        }
        
        private bool CheckHorizontalLineWinCondition(int y)
        {
            int index = GetIndex(0, y, Size);
            if(_arr[index] == '-')
            {
                return false;
            }

            bool result = true;
            for(int x = 1; x < Size; x++)
            {  
                result &= _arr[GetIndex(0, y, Size)] == _arr[GetIndex(x, y, Size)];
            }
            return result;
        }

        private bool CheckDiagonalWinCondition()
        {
            int index = GetIndex(0, 0, Size);
            if(_arr[index] == '-')
            {
                return false;
            }
            bool result = true; // req: n=m
            for(int i = 1; i < Size; i++)
            {
                result &= _arr[GetIndex(0, 0, Size)] == _arr[GetIndex(i, i, Size)];
            }
            if(result)
            {
                Console.WriteLine("First diagonal: "  + result + "\n");
                return true;
            }
            index = GetIndex(Size - 1, 0, Size);
            if(_arr[index] == '-')
            {
                return false;
            }
            result = true;
            for(int i = 1; i < Size; i++)
            { 
                result &= _arr[GetIndex(Size - 1, 0, Size)] == _arr[GetIndex(Size - 1 - i, i, Size)];
            }
            if(result)
            {
                Console.WriteLine("Second diagonal: "  + result + "\n");
                return true;
            }

            return false;
        }

        #endregion

    }

    public enum TurnState
    {
        WrongInput = -1,
        NormalTurn = 0,
        GameFinshed = 1
    }
}
