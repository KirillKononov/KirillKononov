using System;
using System.IO;

namespace FilesBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new PathsExtractor.PathsExtractor(@"E:\reposC#\GitRepo\FilesBackup\FilesBackup\appsettings.json");
            var t = p.ExtractPaths();
            new FileCopier(t).CopyFiles();
        }
    }
}
