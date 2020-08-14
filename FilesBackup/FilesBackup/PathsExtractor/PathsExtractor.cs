using System;
using System.IO;
using Newtonsoft.Json;

namespace FilesBackup.PathsExtractor
{
    public class PathsExtractor
    {
        private readonly string _pathToSettingsFile;
        private readonly string _dateTime = @"\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        public PathsExtractor(string pathToSettingsFile)
        {
            _pathToSettingsFile = pathToSettingsFile;
        }

        public DirectoryPaths ExtractPaths()
        {
            var fileData = File.ReadAllText(_pathToSettingsFile);
            var directoryPaths = JsonConvert.DeserializeObject<DirectoryPaths>(fileData);
            directoryPaths.TargetPath += _dateTime;
            return directoryPaths;
        }
    }
}
