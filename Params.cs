using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HentaiWorld
{
    static class Params
    {
        public static int port { get; private set; }
        public static string ipPrefix { get; private set; }
        public static bool BEEP { get; private set; }
        public static bool NgrokTunnel { get; private set; }
        /// <summary>
        /// In milliseconds
        /// </summary>
        public static int NgrokStartTimeout { get; private set; }
        static Params()
        {
            port = 29478;
            NgrokStartTimeout = 5000;
            ipPrefix = "*";
            BEEP = false;
            HLPrint.Print($"Selected port: {port}");
        }
        public static void ProcessArgs(string[] args)
        {
            for(int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-port":
                        port = Convert.ToInt32(args[i + 1]);
                        i++;
                        HLPrint.Print($"Selected port: {port}", messageLevel: 63);
                        break;
                    case "-ipPrefix":
                        ipPrefix = args[i + 1];
                        i++;
                        HLPrint.Print($"Ip to input conections: {ipPrefix}", ConsoleColor.Yellow, messageLevel: 15);
                        break;
                    case "-logLevel":
                        HLPrint.SetLogLevel(Convert.ToByte(args[i + 1]));
                        i++;
                        HLPrint.Print($"Log level will be change to {HLPrint.LogLevel}", messageLevel:127);
                        break;
                    case "-BEEP":
                        BEEP = true;
                        HLPrint.Print("BEEP", ConsoleColor.Red, messageLevel: 127);
                        break;
                    case "-enableNgrok":
                        NgrokTunnel = true;
                        HLPrint.Print("Enable Ngrok tunnel.", ConsoleColor.Yellow, messageLevel: 15);
                        break;
                    case "-NgrokStartTimeout":
                        NgrokStartTimeout = Convert.ToInt32(args[i + 1]);
                        i++;
                        HLPrint.Print($"Time to start ngrok: {NgrokStartTimeout}. If ngrok not started, application will be stopped.", ConsoleColor.Yellow, messageLevel: 15);
                        break;
                }
            }
        }
    }
}
