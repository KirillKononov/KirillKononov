using System.IO;
using Newtonsoft.Json;
using Serilog;

namespace FilesBackup.PathsExtractor
{
    public class PathsExtractor
    {
        private readonly string _pathToSettingsFile;
        private readonly ILogger _logger;

        public PathsExtractor(string pathToSettingsFile)
        {
            _pathToSettingsFile = pathToSettingsFile;
            _logger = Log.Logger;
        }

        public DirectoryPaths ExtractPaths()
        {
            _logger.Information("Source and target path extraction started");
            var fileData = File.ReadAllText(_pathToSettingsFile);
            var directoryPaths = JsonConvert.DeserializeObject<DirectoryPaths>(fileData);
            _logger.Information("Source and target path extraction finished");
            return directoryPaths;
        }
    }
}
