using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace NOS
{
    class Program
    {
        static StreamWriter w;
        static int tellerpwnd = 1;
        
        static void Main(string[] args)
        {
            PWNED();
          //  NOS();
            
        }

        static void PWNED()
        {
            WebClient webclePWNED = new WebClient();
            int counterPWNED = 0;
            string[] links = new string[100];
            HtmlDocument d = new HtmlDocument();
            string u = webclePWNED.DownloadString("http://www.powned.tv/archief/2014/06/16-week/");
            d.LoadHtml(u);
            HtmlNode p = d.GetElementbyId("maincol-large");
            List<HtmlNode> ul = p.SelectNodes("ul").ToList();
            List<HtmlNode> li = new List<HtmlNode>();
            foreach (HtmlNode l in ul)
            {
                List<HtmlNode> temp = l.SelectNodes("li").ToList();
                foreach (HtmlNode t in temp)
                {
                    if (li.Count < 100)
                    {
                        li.Add(t);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            foreach (HtmlNode q in li)
            {
                w = new StreamWriter(tellerpwnd + "pwnd.txt");
                w.WriteLine("<?xml version=\"1.0\"?>");
                w.WriteLine("<content>");
                w.WriteLine("<site>PWNED</site>");
                w.WriteLine("<article>");
                
                addToListPWNED(q);
                w.WriteLine("</article>");
                w.WriteLine("</content>");
                w.Close();
                Console.WriteLine(tellerpwnd);
                tellerpwnd++;
            }
        }
        static void addToListPWNED(HtmlNode node)
        {
            HtmlNode h = node.SelectSingleNode("a");
            string link = h.Attributes["href"].Value;
            WebClient wb = new WebClient();
            string s = wb.DownloadString(link);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(s);
            HtmlNode intro = doc.GetElementbyId("maincol-med").SelectSingleNode("//div[@class='artikel-intro']");
            HtmlNode main = doc.GetElementbyId("maincol-med").SelectSingleNode("//div[@class='artikel-main']");
            List<HtmlNode> ps = new List<HtmlNode>();
            ps.Add(intro);
            ps.Add(main);
            List<HtmlNode> lijssie = new List<HtmlNode>();
            foreach (HtmlNode p in ps)
            {
                lijssie.AddRange(p.SelectNodes("p").ToList());
            }
            string tekst = "";
            foreach(HtmlNode gerben in lijssie)
            {
                tekst += gerben.InnerText;
            }
            w.WriteLine(tekst);
        }
        static void NOS()
        {
            WebClient webcleNOS = new WebClient();
            int counterNOS = 0;
            int startNOS = 659150;
            while (counterNOS < 100)
            {
                try
                {
                    string s = webcleNOS.DownloadString("http://nos.nl/artikel/" + startNOS);
                    addToListNOS(s);
                    counterNOS++;
                    startNOS--;
                }
                catch (Exception e)
                {
                    startNOS--;
                }
            }
            w.Close();
        }
        static void addToListNOS(string s)
        {
            HtmlDocument d = new HtmlDocument();
            d.LoadHtml(s);
            HtmlNode n = d.GetElementbyId("article");
            List<HtmlNode> ns = n.SelectSingleNode("div").SelectNodes("p").ToList();
            string tekst = "";
            foreach (HtmlNode node in ns)
            {
                tekst += " " + node.InnerText;
            }
            w.WriteLine(tekst);


        }
    }
}
