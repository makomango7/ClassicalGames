namespace Magicboard.Common
{
    public static class CommonExtensions 
    {

        public static int GetIndex(int x, int y, int size) => y * size + x;
        
        public static int GetXFromIndex(int index, int size) => index / size;

        public static int GetYFromIndex(int index, int size) => index % size;

    }
}
