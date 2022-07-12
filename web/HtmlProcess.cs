using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace HentaiWorld
{
    public abstract class HtmlProcess
    {
        public abstract string ChangeValves(string page, int typesId, object other);
    }
    public class AboutProcess : HtmlProcess
    {
        public override string ChangeValves(string page, int typesId, object other)
        {
            if (typesId != 1)
                return "bt";
            page = page.Replace("~~~0", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.ffff zzz"));
            page = page.Replace("~~~1", HLPrint.GetUptime());
            page = page.Replace("~~~5", Environment.OSVersion.ToString());
            var q = (HttpListenerContext)other;
            page = page.Replace("~~~2", q.Request.RemoteEndPoint.ToString());
            page = page.Replace("~~~3", q.Request.UserAgent);
            page = page.Replace("~~~4", String.Concat(q.Request.UserLanguages));
            page = page.Replace("~~~6", q.Request.Headers.Get("uip"));
            return page;
        }
    }
    public class QRProcess : HtmlProcess
    {
        public override string ChangeValves(string page, int typesId, object other)
        {
            if (typesId != 2)
                return "bt";

            page = page.Replace("~~~0", GetURL.compress_url.Replace("/", "%2F"));

            return page;
        }
    }
    public class FindProcess : HtmlProcess
    {
        public LangLoader l;
        public FindProcess(LangLoader l)
        {
            this.l = l;
        }
        public override string ChangeValves(string page, int typesId, object other)
        {
            if (typesId != 3)
                return "bt";

            string targetLang = "en";
            var ls = (HttpListenerContext)other;
            targetLang = ls.Request.UserLanguages[0].Remove(2);
            string tg = ls.Request.QueryString.Get("lang");
            if (tg != null)
                targetLang = tg;

            page = page.Replace("###input_filed", l.GetKeyData("###input_filed", targetLang));
            page = page.Replace("###find_but", l.GetKeyData("###find_but", targetLang));
            page = page.Replace("###find_d", l.GetKeyData("###find_d", targetLang));
            return page;
        }
    }
}
