using System;

namespace CsharpServer
{
    public static class Utils
    {
        public static string ToHexId(this int ID)
        {
            return "0x" + ID.ToString("X2");
        }

        public static void InvokeIfNotNull(this object obj, Action action)
        {
            if(obj != null)
            {
                action.Invoke();
            }
        }
    }
}
