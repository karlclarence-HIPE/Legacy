namespace Legacy.Framework.FileManager.Configuration;

/// <summary>
/// File Upload / Download related configuration options.
/// </summary>
public class FileManagerConfigurationOption
{
    public const string SectionName = "FileManager";

    /// <summary>
    /// The name of the directory where files will be stored.
    /// </summary>
    public required string DirectoryName { get; set; } = default!;

    /// <summary>
    /// List of allowed file extensions for upload. If empty, all file types are allowed. 
    /// </summary>
    public required IEnumerable<string> AcceptedExtensions { get; set; } = [];

    /// <summary>
    /// List of Accepted Image Extensions
    /// </summary>
    public required IEnumerable<string> AcceptedImageExtensions { get; set; } = [];

    /// <summary>
    /// Max FIle Size that will be accepted by the system in Megabytes.
    /// </summary>
    public required int MaxFileSize { get; set; }
}
