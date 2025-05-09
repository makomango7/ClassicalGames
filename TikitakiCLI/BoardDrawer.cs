using Magicboard.Tikitaki;

namespace TikitakiCLI
{
    internal static class BoardDrawer
    {
        internal static void RedrawSquareBoard(Game game, bool isDebug)
        {
            if(game.Arr.Length != game.Size * game.Size)
            {
                throw new System.Exception("Input game board is not square size");
            }


        #if !DEBUG
            if(!isDebug)
                Console.Clear();
        #endif

            for(int i = 0; i < game.Arr.Length; i++)
            {
                if(i != 0 && i % game.Size == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(PlayerToAvatar(game.Arr[i]).ToString() + " ");
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
        }


        internal static char PlayerToAvatar(int playerId)
        {
            switch(playerId)
            {
                case 0:
                    return '-';
                case 1: 
                    return 'x';
                case 2:
                    return 'o';
            }
            throw new System.Exception("No such a player");
        }

        internal static Dictionary<int, char> Set1 = new Dictionary<int, char>()
        {
            {0,'-' },
            {1, 'x' },
            {2, 'o' }
        };

        internal static Dictionary<int, char> Set2 = new Dictionary<int, char>()
        {
            {0, '$' },
            {1, '%' },
            {2, '^' }
        };
    }
}
