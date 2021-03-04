using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EasyIconDownloader
{
    public class ConfigurationHelper
    {
        private static IConfigurationBuilder _instance;

        public static IConfigurationBuilder ConfigurationBuilder
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                }
                return _instance;
            }
        }

        public static IConfigurationRoot Configuration => ConfigurationBuilder.Build();

        public static IConfigurationSection GetConfigurationSection(string key)
        {
            return Configuration.GetSection(key);
        }

        public static T GetValue<T>(string key)
        {
            var section = GetConfigurationSection(key);
            //引用包：Microsoft.Extensions.Configuration.Binder
            return section.Get<T>();
        }
    }
}
