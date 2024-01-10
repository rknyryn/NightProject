using NightProject.Library.Models;

namespace NightProject.Library.Abstractions;


public interface IFileOperationService<T> where T : IFileOperationModel, new()
{
    /// <summary>
    /// Read file from path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public IList<T> Read(string path);

    /// <summary>
    /// Write file to path
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    public void Write(string path, IList<T> content);
}

