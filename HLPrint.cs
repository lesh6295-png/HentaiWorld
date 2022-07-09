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
        public static byte LogLevel { get; private set; }


        static HLPrint()
        {
            startupTime = DateTime.Now;
            LogLevel = 63;
        }
        /// <summary>
        /// DONT CALL THIS METHOD, HE USING ONLY ONE TIMES IN Params CLASS
        /// </summary>
        /// <param name="level"></param>
        public static void SetLogLevel(byte level)
        {
            LogLevel = level;
        }
        public static void Print(string s, bool newLine = true, byte messageLevel = 63)
        {
            if (messageLevel > LogLevel)
                return;
            if (newLine)
            {
                Console.WriteLine(s.ToPrint());
                return;
            }
            Console.Write(s.ToPrint());
        }
        public static void Print(string s, ConsoleColor color, bool newLine = true, byte messageLevel = 63)
        {
            if (messageLevel > LogLevel)
                return;

            ConsoleColor last = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (newLine)
            {
                Console.WriteLine(s.ToPrint());
                Console.ForegroundColor = last;
                return;
            }
            Console.Write(s.ToPrint());
            Console.ForegroundColor = last;
        }
        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        public static string GetUptime()
        {
            return (DateTime.Now - startupTime).ToString();
        }
        public static string ToPrint(this string s)
        {
            return $"[{DateTime.Now - startupTime}|{DateTime.Now.ToString("HH:mm:ss.fff")}]{s}";
        }
    }
}
