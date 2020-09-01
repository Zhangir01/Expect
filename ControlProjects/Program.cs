using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace ControlProjects
{
    
    
    class Program
    {
        private static List<string> allHtmlCode;
        private static List<string> code;
        private static List<int> grodus;
        private static Dictionary<string, int> weather;
        static class Html
        {
            public static string HtmlToText(string htmlText)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlText);
                var textString = doc.DocumentNode.InnerText;
                string code = Regex.Replace(textString, @"<(.|n)*?>", string.Empty).Replace("&nbsp", "");
                return code;
            }
        }
        private static void Fill() 
        {
            allHtmlCode = new List<string>();
            using (StreamReader sr = new StreamReader("htmlCode.txt", Encoding.UTF8, false)) 
            {
                while (sr.ReadLine() != null)
                    allHtmlCode.Add(sr.ReadLine());
            }
           

        }
        private static string Find(string htmlElement)
        {
           
            int element = allHtmlCode.IndexOf(htmlElement);

            string ce = Html.HtmlToText(allHtmlCode[element + 1]);

            return ce;
        }
        private static void ShowGradus() 
        {
            foreach (var item in weather.Values) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(item+"°");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1001);
            }
            
        }
        private static void ShowCity() 
        {
            foreach (var item in weather.Keys) 
            {
                Console.Write(item + " - ");
                Thread.Sleep(1000);
            }
        }
        static void Main(string[] args)
        {
            Random random = new Random();
            Fill();
            code = new List<string>();
            for (int i = 0; i < allHtmlCode.Count; i++)
            {
                if (allHtmlCode[i] == "<tr>")
                {
                    code.Add(allHtmlCode[i + 1]);
                }
            }
            for (int i = 0; i < code.Count; i++)
            {
                code[i] = Html.HtmlToText(code[i]);
            }
            grodus = new List<int>();
            for (int i = 0; i < code.Count; i++)
            {
                grodus.Add(random.Next(15, 30));
            }
            code.Remove("К");
           
            weather = new Dictionary<string, int>();
            for (int i = 0; i < code.Count; i++)
            {
                code.Remove("");
                weather.Add(code[i], grodus[i]);
            }
            Thread thread = new Thread(ShowGradus);
            thread.Start();
            ShowCity();
           
            Console.ReadLine();
        }
     
    }
}
