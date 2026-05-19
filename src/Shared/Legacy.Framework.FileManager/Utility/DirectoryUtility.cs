namespace Legacy.Framework.FileManager.Utility;

public static class DirectoryUtility
{
    private static string GetCurrentDirectory => Directory.GetCurrentDirectory();

    /// <summary>
    /// Gets the current working directory.
    /// </summary>
    private static string GenerateModulePath(string baseUploadDirectoryName, string moduleDirectoryName)
    {
        var filePath = string.Concat(GetCurrentDirectory, '/', baseUploadDirectoryName, '/', moduleDirectoryName);

        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        
        return filePath;
    }

    public static string GenerateCompleteFilePath(string baseUploadDirectoryName, string moduleDirectoryName, string fileName) =>
        Path.Combine(GenerateModulePath(baseUploadDirectoryName, moduleDirectoryName), fileName);
}
