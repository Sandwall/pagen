public static class Utility
{
    // Check if a path is a directory
    public static bool IsDirectory(String dirName)
    {
        FileAttributes attr = File.GetAttributes(dirName);
        return attr.HasFlag(FileAttributes.Directory);
    }

    // Explores a directory recursively and returns the path to all .md files
    public static HashSet<string> GetFilesRecursively(string dir)
    {
        HashSet<string> filePaths = new HashSet<string>();

        Stack<string> dirs = new Stack<string>();
        dirs.Push(dir);

        while (dirs.Count > 0)
        {
            string currentDir = dirs.Pop();

            foreach (string file in Directory.GetFiles(currentDir))
            {
                if (Path.GetExtension(file).Equals(".md", StringComparison.OrdinalIgnoreCase))
                {
                    filePaths.Add(file);
                }
            }

            foreach (string subDir in Directory.GetDirectories(currentDir))
            {
                dirs.Push(subDir);
            }
        }

        return filePaths;
    }

    // Converts "path" to a path relative to the output directory
    public static string GetOutputPath(string path, string inputDir, string outputDir)
    {
        string relativePath = path.Substring(inputDir.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        string outputPath = Path.Combine(outputDir, relativePath);
        return outputPath;
    }
}