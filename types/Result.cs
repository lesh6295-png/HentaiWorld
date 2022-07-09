using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HentaiWorld
{
    public struct Result
    {
        public string img_url;
        public List<string> tags;
        public string source_data;

        public static Result Empry
        {
            get
            {
                Result t;
                t.tags = new List<string>();
                t.img_url = "";
                t.source_data = "clear_object";
                return t;
            }
        }
    }
}
