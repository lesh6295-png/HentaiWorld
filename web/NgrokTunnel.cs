using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
namespace HentaiWorld
{
    class NgrokTunnel
    {
        public string url { get; private set; }
        Process server;
        public NgrokTunnel()
        {
            HLPrint.Print("Create Ngrok tunnel...", ConsoleColor.Yellow, messageLevel: 127);
            server = new Process();
            server.StartInfo.UseShellExecute = true;
            server.StartInfo.CreateNoWindow = false;
            server.StartInfo.FileName = "cmd.exe";
            server.StartInfo.Arguments = $"/C ngrok http {Params.port}";
            server.Start();
            GetUrl();
        }
        public async void GetUrl()
        {
            await Task.Delay(Params.NgrokStartTimeout);
            HLPrint.Print("Find Ngrok URL...", ConsoleColor.Yellow, messageLevel: 127);
            WebClient loadfromdevpage = new();
            string page = loadfromdevpage.DownloadString("http://127.0.0.1:4040/api/tunnels");
            string code = page.Split("public_url\"")[1];
            string rawurl = "";
            bool write = false;
            for(int i = 0; i < code.Length; i++)
            {
                if(code[i] == '"')
                {
                    if(write == false)
                    {
                        write = true;
                        continue;
                    }
                    if (write == true)
                    {
                        break;
                    }
                    continue;
                }
                if (write)
                {
                    rawurl += code[i];
                }
            }
            url = rawurl;
            HLPrint.Print($"Ngrok URL: {url}", ConsoleColor.Yellow, messageLevel: 15);
            await GetURL.CompressUrl(url);
            HLPrint.Print($"Short URL: {GetURL.compress_url}", ConsoleColor.Yellow, messageLevel: 15);
        }
    }
}
