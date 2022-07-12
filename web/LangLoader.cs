using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace HentaiWorld
{
    public class LangLoader
    {
        public List<string> used_langs = new List<string>();
        public List<KeyWithTranslates> keys = new List<KeyWithTranslates>();
        public void LoadLangs(string pathToLocales)
        {
            HLPrint.Print("Start load locales...", messageLevel: 127);
            string[] file = File.ReadAllLines(pathToLocales);
            foreach(string line in file)
            {
                if (line.StartsWith('~') || line == string.Empty)
                    continue;
                string[] par = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if(par[0] == "USE_LANGS")
                {
                    for(int i = 1; i < par.Length; i++)
                    {
                        used_langs.Add(par[i]);
                    }
                }
                if (par[0].StartsWith("###"))
                {
                    var q = KeyWithTranslates.Empry;
                    q.Key = par[0];
                    string lang = "", val = "";
                    bool wik = false;
                    for(int i = 1; i < par.Length; i++)
                    {
                        if (!wik)
                        {
                            lang = par[i].Split(':')[0];
                            val = par[i].Split(':')[1];
                            if (!used_langs.Contains(lang))
                            {
                                HLPrint.Print($"Lang code not registring: {lang}", ConsoleColor.Red, messageLevel: 127);
                                throw new Exception();
                            }
                            else
                            {
                                wik = true;
                            }
                        }
                        else
                        {
                            if(par[i] != "@@@")
                            {
                                val += " " + par[i];
                            }
                            else
                            {
                                q.Translates.Add(lang, val);
                                wik = false;
                            }
                        }
                    }
                    keys.Add(q);
                }
            }
            HLPrint.Print($"Locale loaded: {used_langs.Count} language used, {keys.Count} count of keys", ConsoleColor.Green);
        }
        public string GetKeyData(string key, string lang)
        {
            foreach(var k in keys)
            {
                if(k.Key == key)
                {
                    return k.Translates.GetValueOrDefault(lang);
                }
            }
            return "no_key|no_valve";
        }
    }
    public struct KeyWithTranslates
    {
        public string Key;
        public Dictionary<string, string> Translates;
        public static KeyWithTranslates Empry
        {
            get
            {
                KeyWithTranslates k;
                k.Key = "no_key";
                k.Translates = new();
                return k;
            }
        }
    }
}
