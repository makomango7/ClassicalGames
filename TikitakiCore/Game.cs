using static Magicboard.Common.CommonExtensions;

namespace Magicboard.Tikitaki
{
    public class Game
    {
        public static readonly int PlayerNULL = 0;
        public static readonly int PlayerONE = 1;

        private int _configurationPlayersCount = 2;
        private int _configurationSize = 3;

        int[] _arr = new int[9];
        int _currentInputIndex = 1;
        private bool _isFinished;
        
        public int GameRes { get; private set; }
        public int Size { get => _configurationSize; }
        public int CurrentPlayer { get => _currentInputIndex; }
        public int[] Arr { get => _arr; }

        public bool IsFinished { get => _isFinished; }

        public Game()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                _arr[i] = PlayerNULL;
            }
        } 

/*
 * Все просто, как только происходит один из
 * вид-кондишинов - игра тут же завершается.
 * Статусные поля переводятся в соответствующие состояния.
 */
        
        public TurnState MakeATurn(int x, int y) 
        {
            if(_arr[GetIndex(x, y, Size)] != PlayerNULL)
            {
                return TurnState.WrongInput;  // cell should be not captured! 
            }
            _arr[GetIndex(x, y, Size)] = _currentInputIndex;

            if(CheckWinCondition())
            {
                _isFinished = true;
                return TurnState.GameFinshed; // game is won by somebody 
            }
            if(CheckNoEmptyCellsCondition())
            {
                _currentInputIndex = 0;
                _isFinished = true;
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
            if((int)_currentInputIndex > _configurationPlayersCount)
            {
                _currentInputIndex = PlayerONE;
            }
        }

        #region Check Conditions 

        private bool CheckNoEmptyCellsCondition()
        {
            for(int i = 0; i < _arr.Length; i++)
            {
                if(_arr[i] == PlayerNULL)
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
                if(win)
                {
                    return true;
                }
           }
            
           for(int y = 0; y < Size; y++)
           {
                win = CheckHorizontalLineWinCondition(y);
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
            if(_arr[index] == PlayerNULL)
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
            if(_arr[index] == PlayerNULL)
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
            if(_arr[index] == PlayerNULL)
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
                return true;
            }
            index = GetIndex(Size - 1, 0, Size);
            if(_arr[index] == PlayerNULL)
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