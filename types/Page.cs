using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HentaiWorld
{
    public struct Page
    {
        public string page;
        public bool canProcess;
        public int processId;
        public static Page Empry
        {
            get
            {
                Page p;
                p.page = "";
                p.processId = -1;
                p.canProcess = false;
                return p;
            }
        }
            
    }
}
