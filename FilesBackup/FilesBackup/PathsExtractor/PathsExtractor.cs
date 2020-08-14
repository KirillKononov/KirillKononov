using System.IO;
using Newtonsoft.Json;

namespace FilesBackup.PathsExtractor
{
    public class PathsExtractor
    {
        private readonly string _pathToSettingsFile;

        public PathsExtractor(string pathToSettingsFile)
        {
            _pathToSettingsFile = pathToSettingsFile;
        }

        public DirectoryPaths ExtractPaths()
        {
            var fileData = File.ReadAllText(_pathToSettingsFile);
            return JsonConvert.DeserializeObject<DirectoryPaths>(fileData);
        }
    }
}
