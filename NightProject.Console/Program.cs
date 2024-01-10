using System.Reflection;
using NightProject.Library;
using NightProject.Library.Extensions;
using NightProject.Library.Models;

namespace NightProject.Console;

public class Program
{
    public static void Main(string[] args)
    {
        TestCase1();
        TestCase2();
        TestCase3();

        System.Console.WriteLine("All test cases are completed.");
        System.Console.ReadKey();
    }

    /// <summary>
    /// Generate XML output from CSV input, filtered by City name=’Antalya’
    /// </summary>
    static void TestCase1()
    {
        var cities = FileOperationParser.Parse<CityCsvModel>(Path.Combine("Resources", "sample_data.csv"));
        FileOperationParser.Write(Path.Combine("Outputs", "Test1", "sample_data_copy.xml"), cities.Where(w => w.CityName == "Antalya").ToList().ConvertToXmlModel());
    }

    /// <summary>
    /// Generate CSV output from CSV input, sorted by City name ascending, then District name ascending
    /// </summary>
    static void TestCase2()
    {
        var cities = FileOperationParser.Parse<CityCsvModel>(Path.Combine("Resources", "sample_data.csv"));
        FileOperationParser.Write(Path.Combine("Outputs", "Test2", "sample_data_copy.csv"), cities.OrderBy(o => o.CityName).ThenBy(o => o.DistrictName).ToList());
    }

    /// <summary>
    /// Generate CSV output from XML input, filtered by City name=’Ankara’ and sorted by Zip code descending
    /// </summary>
    static void TestCase3()
    {
        var cities = FileOperationParser.Parse<City>(Path.Combine("Resources", "sample_data.xml"));
        var result = cities.Where(w => w.Name == "Ankara").ToList().ConvertToCsvModel().OrderByDescending(o => o.ZipCode).ToList();
        FileOperationParser.Write(Path.Combine("Outputs", "Test3", "sample_data_copy.csv"), result);
    }
}

