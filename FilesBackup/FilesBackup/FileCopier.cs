using System;
using System.Collections.Generic;
using System.IO;
using FilesBackup.PathsExtractor;
using Serilog;

namespace FilesBackup
{
    public class FileCopier
    {
        private readonly DirectoryPaths _directoryPaths;
        private readonly ILogger _logger;

        public FileCopier(DirectoryPaths directoryPaths)
        {
            _directoryPaths = directoryPaths;
            _logger = Log.Logger;
        }

        public void CopyFiles()
        {
            _logger.Information("Copying files");
            foreach (var sourcePath in _directoryPaths.SourcePaths)
            {
                if (Directory.Exists(sourcePath))
                {
                    Directory.CreateDirectory(_directoryPaths.TargetPath);
                    var files = Directory.GetFiles(sourcePath);

                    foreach (string s in files)
                    {
                        try
                        {
                            var fileName = Path.GetFileName(s);
                            var destFile = Path.Combine(_directoryPaths.TargetPath, fileName);
                            File.Copy(s, destFile, true);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            _logger.Error("You don't have permissions to copy this file");
                        }
                    }

                    CopySubDirectories(sourcePath);
                }
                else
                {
                    _logger.Error("Folder on this path {sourcePath} does not exist");
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
