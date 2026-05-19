namespace Legacy.Profile.Application.Configuration;

public class ModuleConfigurationOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "Profile";

    /// <summary>
    /// Module key to identify setting for generating transaction sequence no if necessary.
    /// </summary>  
    public required string ModuleKey { get; set; }

    /// <summary>
    /// Upload Directory.
    /// </summary>
    public required string UploadDirectory { get; set; }

    /// <summary>
    /// Connection string for database.
    /// </summary>
    public required string ConnectionString { get; set; }

    /// <summary>
    /// User Group Module Code
    /// </summary>
    public required string ModuleCode { get; set; }
}
