using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace EasyIconDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GetIcons();
            }
        }


        private static void GetIcons()
        {
            Console.WriteLine("Id:");
            var b = int.TryParse(Console.ReadLine(), out int id);
            if (!b)
            {
                Console.WriteLine("Not an Id.");
                return;
            }

            Console.WriteLine("File name:");
            var fileName = Console.ReadLine();
            
            List<int> PixeList = ConfigurationHelper.GetValue<List<int>>("PixeList");

            var outputDir = ConfigurationHelper.GetValue<string>("OutputDir");
            outputDir = Path.Combine(outputDir, $"{id}_{fileName}");
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            foreach (int pxInt in PixeList)
            {
                var apiUrl = ConfigurationHelper.GetValue<string>("APIURL");
                apiUrl = apiUrl.Replace("{id}", id.ToString()).Replace("{size}", pxInt.ToString());
                var web = new WebClient();
                var pngBytes = web.DownloadData(new Uri(apiUrl));
                if (pngBytes.Length > 100)
                    File.WriteAllBytes(Path.Combine(outputDir, $"{fileName}_{pxInt}px.png"), pngBytes);
            }
        }
    }
}
