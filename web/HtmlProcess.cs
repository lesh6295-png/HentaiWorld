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
            return page;
        }
    }
}
