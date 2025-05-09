using Magicboard.Tikitaki;

namespace TikitakiCLI;
public class BoardDrawer : IBoardDrawer
{

    internal char[] WorkingSet; 

    internal BoardDrawer()
    {
        WorkingSet = Set2;
    }
    internal BoardDrawer(char[] workingSet)
    {
        WorkingSet = workingSet;
    }

    public void RedrawSquareBoard(Game game, bool isDebug)
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


    internal char PlayerToAvatar(int playerId)
    {
        return WorkingSet[playerId];
    }

    
    internal static char[] Set1 = new char[]
    {
        '-', '0', '1'
    };

    internal static char[] Set2 = new char[]
    {
        '-', 'x', 'o'
    };

    internal static char[] Set3 = new char[]
    {
        ' ', '+', '-'
    };
    
    internal static char[] Set4 = new char[]
    {
        ' ', '$', '|'
    };
}

public interface IBoardDrawer
{
    public void RedrawSquareBoard(Game game, bool isDebug);
}
