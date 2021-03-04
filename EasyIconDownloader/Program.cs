using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace EasyIconDownloader
{
    class Program
    {
        private const string APIURL = "https://www.easyicon.net/api/resizeApi.php?id={id}&size={size}";

        private static List<int> PixeList = new List<int>()
        {
            16, 24, 32, 48, 64, 72, 96, 128
        };


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
            
            //Console.WriteLine("Pixe:");
            //b = int.TryParse(Console.ReadLine(), out int pxInt);
            //if (!b)
            //{
            //    Console.WriteLine("Not a px.");
            //    return;
            //}

            //Console.WriteLine("Icon type 0-PNG, 1-ICO, 2-ICNS:");
            //b = int.TryParse(Console.ReadLine(), out int iconInt);
            //IconTypeEnum iconType = (IconTypeEnum) iconInt;
            //if (!b)
            //{
            //    Console.WriteLine("Not a icon type:");
            //    return;
            //}

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
