using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HentaiWorld
{
    /// <summary>
    /// Use this class instead console
    /// </summary>
    static class HLPrint
    {
        static DateTime startupTime;
        static HLPrint()
        {
            startupTime = DateTime.Now;
        }
        public static void Print(string s, bool newLine = false)
        {
            if (newLine)
            {
                Console.WriteLine(s.ToPrint());
                return;
            }
            Console.Write(s.ToPrint());
        }
        public static void Print(string s, ConsoleColor color, bool newLine = false)
        {
            ConsoleColor last = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (newLine)
            {
                Console.WriteLine(s.ToPrint());
                return;
            }
            Console.Write(s.ToPrint());
            Console.ForegroundColor = last;
        }
        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        public static string ToPrint(this string s)
        {
            return $"[{DateTime.Now - startupTime}|{DateTime.Now.ToString("HH:mm:ss.fff")}]{s}";
        }
    }
}
