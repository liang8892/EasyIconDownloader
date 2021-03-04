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
        private const string APIURL = "https://www.easyicon.net/api/resizeApi.php?id={id}&size={size}";

        // private static List<int> PixeList = new List<int>()
        // {
        //     16, 24, 32, 48, 64, 72, 96, 128, 256, 512
        // };


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
                Console.WriteLine("Not a Id.");
                return;
            }

            Console.WriteLine("File name:");
            var fileName = Console.ReadLine();
            
            List<int> PixeList = ConfigurationHelper.GetValue<List<int>>("PixeList");
            
            var dir = $"d:\\EasyIcon\\{id}_{fileName}\\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            foreach (int pxInt in PixeList)
            {
                var apiUrl = APIURL.Replace("{id}", id.ToString()).Replace("{size}", pxInt.ToString());
                var web = new WebClient();
                var pngBytes = web.DownloadData(new Uri(apiUrl));
                File.WriteAllBytes(Path.Combine(dir, $"{fileName}_{pxInt}px.png"), pngBytes);
            }
        }
    }
}
