using Magicboard.Tikitaki;

namespace TikitakiCLI
{
    internal static class BoardDrawer
    {
        internal static void RedrawBoard(Game game, bool isDebug)
        {
            if(!isDebug)
                Console.Clear();
            for(int i = 0; i < game.Arr.Length; i++)
            {
                if(i != 0 && i % game.Size == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(game.Arr[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
