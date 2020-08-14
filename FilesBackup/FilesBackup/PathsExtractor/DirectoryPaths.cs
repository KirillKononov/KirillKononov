using System.Collections.Generic;

namespace FilesBackup.PathsExtractor
{
    public class DirectoryPaths
    {
        public DirectoryPaths(string targetPath, List<string> sourcePaths)
        {
            TargetPath = targetPath;
            SourcePaths = sourcePaths;
        }

        public string TargetPath { get; set; }

        public List<string> SourcePaths { get; set; }
    }
}
