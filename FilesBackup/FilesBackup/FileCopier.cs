using System;
using System.Collections.Generic;
using System.IO;
using FilesBackup.PathsExtractor;

namespace FilesBackup
{
    public class FileCopier
    {
        private readonly DirectoryPaths _directoryPaths;

        public FileCopier(DirectoryPaths directoryPaths)
        {
            _directoryPaths = directoryPaths;
        }

        public void CopyFiles()
        {
            foreach (var sourcePath in _directoryPaths.SourcePaths)
            {
                if (Directory.Exists(sourcePath))
                {
                    Directory.CreateDirectory(_directoryPaths.TargetPath);
                    var files = Directory.GetFiles(sourcePath);

                    foreach (string s in files)
                    {
                        var fileName = Path.GetFileName(s);
                        var destFile = Path.Combine(_directoryPaths.TargetPath, fileName);
                        File.Copy(s, destFile, true);
                    }

                    CopySubDirectories(sourcePath);
                }
                else
                {
                    Console.WriteLine("Source path does not exist!");
                }
            }
        }

        private void CopySubDirectories(string sourcePath)
        {
            var subDirs = new DirectoryInfo(sourcePath).GetDirectories();
            foreach (var dir in subDirs)
            {
                var targetPath = _directoryPaths.TargetPath + @"\" + dir.Name;
                var sourcePaths = new List<string> {dir.FullName};
                var subDirectoryPaths = new DirectoryPaths(targetPath, sourcePaths);

                new FileCopier(subDirectoryPaths).CopyFiles();
            }
        }
    }
}
