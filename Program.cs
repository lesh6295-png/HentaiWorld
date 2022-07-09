using System;

namespace HentaiWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Params.ProcessArgs(args);
            Server s = new Server();
            HLPrint.Print("Hentai World Server");
            s.TryToEnable();
            s.StartServer();
            HLPrint.ReadLine();
        }
    }
}
