using System;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace FilesBackup
{
    class Program
    {
        private static readonly string DateTime = @"\" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        private static readonly string PathToSettingsFile = Environment.CurrentDirectory + @"\appsettings.json";

        private static void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(ConfigurationProvider.Configuration)
                .CreateLogger();
        }

        static void Main(string[] args)
        {
            ConfigurationProvider.BuildConfiguration();
            CreateLogger();

            var pathsExtractor = new PathsExtractor.PathsExtractor(PathToSettingsFile);
            var directoryPaths = pathsExtractor.ExtractPaths();
            directoryPaths.TargetPath += DateTime;

            new FileCopier(directoryPaths).CopyFiles();
            
        }
    }
}
