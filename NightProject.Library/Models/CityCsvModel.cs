namespace NightProject.Library.Models;

public class CityCsvModel : IFileOperationModel
{
    public string CityName { get; set; }
    public string DistrictName { get; set; }
    public int CityCode { get; set; }
    public int ZipCode { get; set; }

    public override string ToString()
    {
        return $"CityName: {CityName}, DistrictName: {DistrictName}, CityCode: {CityCode}, ZipCode: {ZipCode}";
    }
}

