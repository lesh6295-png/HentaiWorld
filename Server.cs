using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
namespace HentaiWorld
{
    class Server
    {
        LoaderPages loaderPages;

        HttpListener http;
        public Server()
        {
            loaderPages = new LoaderPages();
            http = new HttpListener();
        }
        public void TryToEnable()
        {
            HLPrint.Print("Server trying to load components");
            loaderPages.TryLoadPages();
            loaderPages.TryLoadImages();
            loaderPages.AddPrefix(http);
            HLPrint.Print($"Public IP address is {SIP.GetPublicIP()}:{Params.port}, everyone can connect from you with this address. Check your brandmauer and open port to input connections if your cant connect to server from another device.", ConsoleColor.Yellow, messageLevel: 15);
        }
        public void StartServer()
        {
            http.Start();
            while (true)
            {
                var clb = http.BeginGetContext(new AsyncCallback(ClientProcess), http);
                clb.AsyncWaitHandle.WaitOne();
            }
            
            

        }
        void ClientProcess(IAsyncResult result)
        {
            var list = (HttpListener)result.AsyncState;
            var context = list.EndGetContext(result);
            Uri url = context.Request.Url;
            string pageToGet = url.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries).Last();
            HLPrint.Print(url.ToString(), ConsoleColor.Cyan, messageLevel:127);
            HLPrint.Print($"Client {url.UserInfo}, ip: {context.Request.RemoteEndPoint}", ConsoleColor.Cyan, messageLevel: 63);
            byte[] buffer;
            if (url.ToString().Contains("pages/img"))
            {
                HLPrint.Print("Image responce",ConsoleColor.DarkCyan, messageLevel: 127);
                buffer = (loaderPages.LoadImageInMemory(pageToGet));
            }
            else
            {
                HLPrint.Print("Page responce", ConsoleColor.DarkCyan, messageLevel: 127);
                Page page = loaderPages.GetPage(pageToGet);
                string finalPage = page.page;
                if (page.canProcess)
                    finalPage = loaderPages.ProcessPage(page.page, page.processId, context);
                buffer = Encoding.UTF8.GetBytes(finalPage);
            }


            var responce = context.Response;
            responce.ContentLength64 = buffer.Length;
            var os = responce.OutputStream;
            os.Write(buffer, 0, buffer.Length);
            os.Close();

            if (Params.BEEP && Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
#pragma warning disable CA1416
                Console.Beep(15000, 1000); //BEEP
            }
        }
    }
}
