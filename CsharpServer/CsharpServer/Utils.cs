namespace CsharpServer
{
    public static class Utils
    {
        public static string ToHexId(this int ID)
        {
            return "0x" + ID.ToString("X2");
        }
    }
}
