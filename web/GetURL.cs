using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
namespace HentaiWorld
{
    static class GetURL
    {
        public static string compress_url = "";
        public async static Task<int> CompressUrl(string url)
        {
            WebRequest cl = WebRequest.Create("https://gotiny.cc/api");
            cl.ContentType = "application/json";
            cl.Method = "POST";
            string load = "{ \"input\": \""+ url+"\" }";
            byte[] b = Encoding.UTF8.GetBytes(load);
            cl.ContentLength = b.Length;
            using (Stream dataStream = cl.GetRequestStream())
            {
                dataStream.Write(b, 0, b.Length);
            }
            var responce = await cl.GetResponseAsync();
            string d = new StreamReader(responce.GetResponseStream()).ReadToEnd();
            //we stopped here
            string[] d1 = d.Split("\"code\":\"");
            string res = "";
            foreach(var q in d1[1])
            {
                if (q == '"')
                {
                    break;
                }
                res += q;
            }
            compress_url = "gotiny.cc/" + res;

            return 5;
        }
    }
}
