using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace HentaiWorld
{
    static class SIP
    {
        static WebClient wc;
        static SIP()
        {
            wc = new WebClient();
        }
        public static string GetPublicIP()
        {
            return GetPublicIP(IPSource.ipify).ToString();
        }
        /// <summary>
        /// 2ip now is NOT WORKING, NOT CALL HIM, USE IPIFY!
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IPAddress GetPublicIP(IPSource source)
        {
            string result = "";
            switch(source){
                case IPSource.ipify:
                    result = wc.DownloadString("https://api.ipify.org/");
                    break;
                case IPSource._2ip:
                    throw new NotImplementedException();
                    break;
                case IPSource.ipifyx64:
                    result = wc.DownloadString("https://api64.ipify.org/");
                    break;
                case IPSource._2ipx64:
                    throw new NotImplementedException();
                    break;
            }
            IPAddress ip = IPAddress.Parse(result);
            return ip;
        }
    }
    public enum IPSource
    {
        _2ip,
        ipify,
        _2ipx64,
        ipifyx64
    }
}
