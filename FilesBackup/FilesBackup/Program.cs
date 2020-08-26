using System;
using Serilog;

namespace FilesBackup
{
    class Program
    {
        private static readonly string CurrentTime = @"\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        private static readonly string PathToSettingsFile = Environment.CurrentDirectory + @"\appsettings.json";
        private static readonly ILogger Logger = Log.Logger;

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
            
            Logger.Information("Application started");
            var pathsExtractor = new PathsExtractor.PathsExtractor(PathToSettingsFile);
            var directoryPaths = pathsExtractor.ExtractPaths();
            directoryPaths.TargetPath += CurrentTime;

            Logger.Information("Copying started");
            new FileCopier(directoryPaths).Copy();
            Logger.Information("Copying finished");
            Logger.Information("Application completed");

            Console.WriteLine("Press any button to finish");
            Console.ReadLine();
        }
    }
}
