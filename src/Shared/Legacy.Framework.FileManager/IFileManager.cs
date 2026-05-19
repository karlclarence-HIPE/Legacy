using Legacy.Framework.FileManager.Configuration;
using Microsoft.AspNetCore.Http;

namespace Legacy.Framework.FileManager;

/// <summary>
/// File Upload Library
/// </summary>
public interface IFileManager
{
    /// <summary>
    /// Upload Single File
    /// </summary>
    /// <param name="file">File Object</param>
    /// <param name="moduleDirectoryName">Module Specific Directory Name where the file being uploaded will be stored.</param>
    /// <returns>JSON Formatted string metadata of the uploaded file</returns>
    Task<string> UploadAsync(IFormFile file, string moduleDirectoryName);

    /// <summary>
    /// Upload Multiple Files
    /// </summary>
    /// <param name="files">List of files to be uploaded</param>
    /// <param name="moduleDirectoryName">Module Directory Name where the file being uploaded will be stored.</param>
    /// <returns>JSON Formatted string metadata of the uploaded files</returns>
    Task<string> UploadAsync(IEnumerable<IFormFile> files, string moduleDirectoryName);

    /// <summary>
    /// Upload Photo File
    /// </summary>
    /// <param name="file">Photo to be Uploaded</param>
    /// <param name="moduleDirectoryName">Module Directory Name where the file being uploaded will be stored.</param>
    /// <returns>JSON Formatted string metadata of the uploaded files</returns>
    Task<string> UploadPhotoAsync(IFormFile file, string moduleDirectoryName);

    void Remove(string metadata, string moduleDirectoryName);

    /// <summary>
    /// Download File
    /// </summary>
    /// <param name="fileName">Filename to download</param>
    /// <param name="moduleDirectoryName">Module Directory where to find the file</param>
    /// <returns></returns>
    Task<(byte[], string, string)> Download(string fileName, string moduleDirectoryName);
}