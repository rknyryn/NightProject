using NightProject.Library.Abstractions;
using NightProject.Library.Concretes;
using NightProject.Library.Models;

namespace NightProject.Library;

public class FileOperationParser
{
    /// <summary>
    /// Parse file to List<T> with given file extension. If fileOperationService is null, generate instance with file extension.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileOperationService"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> Parse<T>(string path, IFileOperationService<T>? fileOperationService = null) where T : IFileOperationModel, new()
    {
        if (fileOperationService == null)
        {
            var fileExtension = Path.GetExtension(path);
            fileOperationService = GenerateFileOperationInstance<T>(fileExtension);
        }

        return (List<T>)fileOperationService.Read(path);
    }

    /// <summary>
    /// Write List<T> to file with given file extension. If fileOperationService is null, generate instance with file extension.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="data"></param>
    /// <param name="fileOperationService"></param>
    /// <typeparam name="T"></typeparam>
    public static void Write<T>(string path, List<T> data, IFileOperationService<T>? fileOperationService = null) where T : IFileOperationModel, new()
    {
        if (fileOperationService == null)
        {
            var fileExtension = Path.GetExtension(path);
            fileOperationService = GenerateFileOperationInstance<T>(fileExtension);
        }

        fileOperationService.Write(path, data);
    }

    private static IFileOperationService<T> GenerateFileOperationInstance<T>(string fileExtension) where T : IFileOperationModel, new()
    {
        return fileExtension switch
        {
            ".csv" => new CsvOperationService<T>(),
            ".xml" => new XmlOperationService<T>(),
            _ => throw new Exception("File extension not supported.")
        };
    }
}

