using Csv;
using NightProject.Library.Abstractions;
using NightProject.Library.Models;

namespace NightProject.Library.Concretes;

public class CsvOperationService<T> : IFileOperationService<T> where T : IFileOperationModel, new()
{
    public IList<T> Read(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        var csv = File.ReadAllText(path);
        List<T> result = new List<T>();
        List<string> columnNames = GetColumnnames();

        foreach (var line in CsvReader.ReadFromText(csv))
        {
            var item = new T();
            foreach (var column in columnNames)
            {
                var value = line[column];
                var property = item.GetType().GetProperty(column);
                property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
            }

            result.Add(item);
        }

        return result;
    }

    public void Write(string path, IList<T> content)
    {
        var folder = Path.GetDirectoryName(path);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var columnNames = GetColumnnames().ToArray();
        var rows = content.Select(f => f.GetType().GetProperties().Select(p => p.GetValue(f).ToString()).ToArray()).ToArray();

        File.Open(path, FileMode.OpenOrCreate).Close();
        var csv = CsvWriter.WriteToText(columnNames, rows, ',');
        File.WriteAllText(path, csv);
    }

    private static List<string> GetColumnnames()
    {
        List<string> columnNames = new List<string>();
        typeof(T).GetProperties().ToList().ForEach(f => columnNames.Add(f.Name));
        return columnNames;
    }
}

