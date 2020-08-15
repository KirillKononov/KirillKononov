using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
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

        public void Copy()
        {
            foreach (var sourcePath in _directoryPaths.SourcePaths)
            {
                if (!Directory.Exists(sourcePath))
                {
                    _logger.Error("Source folder on this path {@string} does not exist", sourcePath);
                    continue;
                }

                _logger.Debug("Copying files from {@string} to {@string}", 
                    sourcePath,
                    _directoryPaths.TargetPath);
                Directory.CreateDirectory(_directoryPaths.TargetPath);
                var sourceFilePaths = Directory.GetFiles(sourcePath);

                foreach (var sourceFilePath in sourceFilePaths)
                {
                    var fileName = Path.GetFileName(sourceFilePath);
                    try
                    {
                        CopyFile(fileName, sourceFilePath);
                        _logger.Debug("File: {@string} successfully copied", fileName);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        _logger.Error("You don't have permissions to copy this file: {@string}", 
                            fileName);
                    }
                    catch (NotSupportedException)
                    {
                        _logger.Error("Copy operation is not supported");
                    }
                    catch (IOException ex)
                    {
                        _logger.Error(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        _logger.Error(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);
                    }
                }

                try
                {
                    CopySubDirectories(sourcePath);
                }
                catch (UnauthorizedAccessException)
                {
                    _logger.Error("You don't have permissions to copy this folder: {@string}",
                        sourcePath);
                }
                catch (SecurityException)
                {
                    _logger.Error("You don't have permissions to copy this folder: {@string}",
                        sourcePath);
                }
                catch (DirectoryNotFoundException)
                {
                    _logger.Error("Source path {@string} is invalid");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }

                _logger.Debug("Copying files from {@string} to {@string} finished",
                    sourcePath,
                    _directoryPaths.TargetPath);
            }
        }

        private void CopyFile(string fileName, string sourceFilePath)
        {
            var destFilePath = Path.Combine(_directoryPaths.TargetPath, fileName);
            File.Copy(sourceFilePath, destFilePath, true);
        }

        private void CopySubDirectories(string sourcePath)
        {
            var subDirs = new DirectoryInfo(sourcePath).GetDirectories();
            foreach (var dir in subDirs)
            {
                var targetPath = _directoryPaths.TargetPath + @"\" + dir.Name;
                var sourcePaths = new List<string> {dir.FullName};
                var subDirectoryPaths = new DirectoryPaths(targetPath, sourcePaths);

                new FileCopier(subDirectoryPaths).Copy();
            }
        }
    }
}
