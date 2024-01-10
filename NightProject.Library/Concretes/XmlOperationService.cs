using NightProject.Library.Abstractions;
using System.Xml;
using System.Xml.Serialization;
using NightProject.Library.Models;

namespace NightProject.Library.Concretes;

public class XmlOperationService<T> : IFileOperationService<T> where T : IFileOperationModel, new()
{
    public IList<T> Read(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        using XmlReader reader = XmlReader.Create(path);
        var name = typeof(T).Name;
        reader.ReadToFollowing(name);
        var values = new List<T>();

        do
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                if (reader.Name == name)
                {
                    var model = new T();
                    var serializer = new XmlSerializer(typeof(T));
                    model = (T)serializer.Deserialize(reader);
                    values.Add(model);
                }
            }
        } while (reader.Read());

        return values;
    }

    public void Write(string path, IList<T> content)
    {
        var folder = Path.GetDirectoryName(path);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var xmlSerializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute("AddressInfo"));
        using var writer = XmlWriter.Create(path);
        xmlSerializer.Serialize(writer, content);
    }
}

