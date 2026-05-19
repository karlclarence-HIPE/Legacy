using Legacy.Framework.FileManager.Configuration;
using Legacy.Framework.FileManager.Exceptions;
using Legacy.Framework.FileManager.Model;
using Legacy.Framework.FileManager.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using System.Text.Json;


namespace Legacy.Framework.FileManager;

public class FileManager : IFileManager
{
    private FileManagerConfigurationOption _configuration;

    public FileManager(IOptionsMonitor<FileManagerConfigurationOption> configuration)
    {
        configuration.OnChange(config => _configuration = config);

        _configuration = configuration.CurrentValue;
    }

    public async Task<string> UploadAsync(IFormFile file, string moduleDirectoryName)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrEmpty(moduleDirectoryName);

        return await UploadAsync(new[] { file }, moduleDirectoryName );
    }

    public async Task<string> UploadAsync(IEnumerable<IFormFile> files, string moduleDirectoryName)
    {
        ArgumentNullException.ThrowIfNull(files);
        ArgumentException.ThrowIfNullOrEmpty(moduleDirectoryName);

        List<UploadMetadata> listOfUploadFiles = [];

        foreach (var file in files)
        {
            if (file.Length > _configuration.MaxFileSize * (1024 * 1024))
            {
                throw new FileManagerValidationException(
                    $"File to be uploaded exceeds the allowed Max Size of {_configuration.MaxFileSize} MB");
            }

            var fileExtension = Path.GetExtension(file.FileName);

            if (!_configuration.AcceptedExtensions.Any(e => e.Contains(fileExtension)))
                throw new FileManagerValidationException($"Invalid File.");

            var metadata = await ProcessFileAsync(file, moduleDirectoryName);
            listOfUploadFiles.Add(metadata);
        }

        return JsonSerializer.Serialize(listOfUploadFiles,
            options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
    }

    public async Task<string> UploadPhotoAsync(IFormFile file, string moduleDirectoryName)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrEmpty(moduleDirectoryName);

        if (file.Length > _configuration.MaxFileSize * (1024 * 1024))
            throw new FileManagerValidationException(
                $"File to be uploaded exceeds the allowed Max Size of {_configuration.MaxFileSize} MB");
    
        var fileExtension = Path.GetExtension(file.FileName);
        if (!_configuration.AcceptedImageExtensions.Any(e => e.Contains(fileExtension)))
            throw new FileManagerValidationException(
                $"Invalid File");

        var metadata = await ProcessFileAsync(file, moduleDirectoryName);

        return JsonSerializer.Serialize(metadata, 
            options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
    }

    public async Task<UploadMetadata> ProcessFileAsync(IFormFile file, string moduleDirectoryName)
    {
        var extension = Path.GetExtension(file.FileName);

        var fileRecord = new UploadMetadata(string.Concat(Guid.NewGuid(), extension), file.FileName);

        var path = DirectoryUtility.GenerateCompleteFilePath(_configuration.DirectoryName, 
            moduleDirectoryName, 
            fileRecord.Path);

        await using var stream = new FileStream(path, FileMode.Create); 
        await file.CopyToAsync(stream);

        return fileRecord;
    }

    public async Task<(byte[], string, string)> Download(string fileName, string moduleDirectoryName)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileName);
        ArgumentException.ThrowIfNullOrEmpty(moduleDirectoryName);

        var path = DirectoryUtility.GenerateCompleteFilePath(_configuration.DirectoryName,
            moduleDirectoryName,
            fileName);

        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var temp = await File.ReadAllBytesAsync(path);
        return (temp, contentType, fileName);
    }

    public void Remove(string metadata, string moduleDirectoryName)
    {
        var listOfUploadMetadata = JsonSerializer.Deserialize<List<UploadMetadata>>(metadata, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        if (listOfUploadMetadata is null) throw new ArgumentNullException(nameof(listOfUploadMetadata));

        foreach (var uploadMetadata in listOfUploadMetadata)
        {
            var path = DirectoryUtility.GenerateCompleteFilePath(_configuration.DirectoryName,
                moduleDirectoryName,
                uploadMetadata.Path);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

