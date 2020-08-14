using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                    string[] files = Directory.GetFiles(sourcePath);

                    foreach (string s in files)
                    {
                        var fileName = Path.GetFileName(s);
                        var destFile = Path.Combine(_directoryPaths.TargetPath, fileName);
                        File.Copy(s, destFile, true);
                    }
                }
                else
                {
                    Console.WriteLine("Source path does not exist!");
                }
            }
        }
    }
}
