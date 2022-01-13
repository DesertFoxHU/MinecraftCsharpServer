using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpServer
{
    public static class Debug
    {
        public static bool IsDebug = false;

        public enum Mode
        {
            INFO,
            DEBUG,
            WARN,
            ERROR,
        }

        public static void Send(string s)
        {
            Send(s, Mode.INFO);
        }

        public static void Send(string s, Debug.Mode mode)
        {
            if(mode == Debug.Mode.DEBUG && !IsDebug)
            {
                return;
            }

            string finalMessage = (mode == Mode.DEBUG ? "[DEBUG] " : "") + s;

            ChangeColor(mode);
            Console.WriteLine(finalMessage);
        }

        private static void ChangeColor(Debug.Mode mode)
        {
            if(mode == Mode.INFO)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if(mode == Mode.DEBUG)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            else if(mode == Mode.WARN)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
        }
    }
}
