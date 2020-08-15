using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FilesBackup
{
    public static class ConfigurationProvider
    {
        public static IConfiguration Configuration { get; set; }

        public static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
    }
}
