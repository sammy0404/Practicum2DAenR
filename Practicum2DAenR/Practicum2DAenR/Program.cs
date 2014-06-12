using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace ConsoleApplication15
{
    class Program
    {
        static StreamWriter w = new StreamWriter("test.txt");
        static void Main(string[] args)
        {
            Console.ReadLine();
            WebClient webcle = new WebClient();
            int counter = 0;
            int start = 659150;
            while (counter < 100)
            {
                try
                {
                    string s = webcle.DownloadString("http://nos.nl/artikel/" + start);
                    addToList(s);
                    counter++;
                    start--;
                }
                catch (Exception e)
                {
                    start--;
                }
            }
            w.Close();
        }
        static void addToList(string s)
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
