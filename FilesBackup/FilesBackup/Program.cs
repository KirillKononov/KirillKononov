using System;

namespace FilesBackup
{
    class Program
    {
        private static readonly string DateTime = @"\" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        static void Main(string[] args)
        {
            var pathsExtractor = new PathsExtractor.PathsExtractor(Environment.CurrentDirectory + @"\appsettings.json");
            var directoryPaths = pathsExtractor.ExtractPaths();
            directoryPaths.TargetPath += DateTime;
            new FileCopier(directoryPaths).CopyFiles();
        }
    }
}
