using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace HentaiWorld
{
    class LoaderPages
    {
        string folderWithPages = "pages/";
        Dictionary<string, Page> pages = new Dictionary<string, Page>();

        Dictionary<string, string> images = new Dictionary<string, string>();

        List<HtmlProcess> pageProcess = new List<HtmlProcess>();
        public LoaderPages()
        {
            pageProcess.Add(new AboutProcess());
        }
        public string ProcessPage(string page, int id, object other)
        {
            foreach(HtmlProcess hp in pageProcess)
            {
                string outp = hp.ChangeValves(page, id, other);
                if (outp != "bt")
                    return outp;
            }
            HLPrint.Print($"Falied process page: HtmlProcess id:{id}", ConsoleColor.Red, messageLevel: 7);
            return "!falied";
        }
        public void TryLoadPages()
        {
            HLPrint.Print("Try load pages from drive...", messageLevel:63);
            try
            {
                string[] pagesLib = File.ReadAllLines($"{folderWithPages}pages");
                foreach(string line in pagesLib)
                {
                    string[] words = line.Split(' ');

                    //check if line a comment
                    if(words[0][0] == '~')
                    {
                        HLPrint.Print("comment in page file", messageLevel: 255);
                        continue;
                    }
                    Page p = Page.Empry;
                    p.page = File.ReadAllText(folderWithPages + words[1]);
                    p.canProcess = Convert.ToBoolean(words[2]);
                    if(p.canProcess == true)
                        p.processId = Convert.ToInt32(words[3]);
                    pages.Add(words[0], p);

                    HLPrint.Print($"{words[0]} | {words[1]} - page pair", messageLevel: 127);
                }
                HLPrint.Print("Pages loaded sussesful!", ConsoleColor.Green, messageLevel:63);
            }
            catch( Exception ex)
            {
                HLPrint.Print($"Load pages falied: {ex.Message}", ConsoleColor.Red, messageLevel:7);
            }
        }
        public void TryLoadImages()
        {
            HLPrint.Print("Try check images...", messageLevel: 63);
            try
            {
                string[] pagesLib = File.ReadAllLines($"{folderWithPages}\\img\\image");
                foreach (string line in pagesLib)
                {
                    string[] words = line.Split(' ');

                    //check if line a comment
                    if (words[0][0] == '~')
                    {
                        HLPrint.Print("comment in image file",  messageLevel: 255);
                        continue;
                    }
                    images.Add(words[0], words[1]);
                    HLPrint.Print($"{words[0]} | {words[1]} - image pair", messageLevel: 127);
                }
                HLPrint.Print("Images checking sussesful!", ConsoleColor.Green, messageLevel: 63);
            }
            catch (Exception ex)
            {
                HLPrint.Print($"Check images falied: {ex.Message}", ConsoleColor.Red, messageLevel:7);
            }
        }
        public Page GetPage(string pageKey)
        {
            return pages.GetValueOrDefault(pageKey);
        }
        public void AddPrefix(System.Net.HttpListener hl)
        {
            HLPrint.Print("Start registring prefix", messageLevel: 127);
            //add pages
            foreach(var q in pages.Keys)
            {
                hl.Prefixes.Add($"http://{Params.ipPrefix}:{Params.port}/pages/{q}/");
            }

            // add image prefix
            hl.Prefixes.Add($"http://{Params.ipPrefix}:{Params.port}/pages/img/");
            
        }
        public byte[] LoadImageInMemory(string imageKey)
        {
            return File.ReadAllBytes($"{gaptpf()}/pages/img/{images.GetValueOrDefault(imageKey)}");
        }
        string gaptpf()
        {
            return Path.GetDirectoryName(this.GetType().Assembly.Location);
        }
    }
}
