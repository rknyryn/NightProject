using NightProject.Library.Models;

namespace NightProject.Library.Extensions
{
	public static class Converter
{
    public static List<CityCsvModel> ConvertToCsvModel(this IList<City> cities)
    {
        List<CityCsvModel> cityCsvModels = new List<CityCsvModel>();

        foreach (var city in cities)
        {
            foreach (var district in city.District)
            {
                foreach (var zip in district.Zip)
                {
                    cityCsvModels.Add(new CityCsvModel
                    {
                        CityCode = city.Code,
                        CityName = city.Name,
                        DistrictName = district.Name,
                        ZipCode = zip.Code
                    });
                }
            }
        }

        return cityCsvModels;
    }

    public static List<City> ConvertToXmlModel(this IList<CityCsvModel> cities)
    {
        List<City> cityXmlModels = new List<City>();

        foreach (var city in cities)
        {
            var cityXmlModel = cityXmlModels.FirstOrDefault(f => f.Code == city.CityCode);

            if (cityXmlModel == null)
            {
                cityXmlModel = new City
                {
                    Code = city.CityCode,
                    Name = city.CityName,
                    District = new List<District>()
                };

                cityXmlModels.Add(cityXmlModel);
            }

            var districtXmlModel = cityXmlModel.District.FirstOrDefault(f => f.Name == city.DistrictName);

            if (districtXmlModel == null)
            {
                districtXmlModel = new District
                {
                    Name = city.DistrictName,
                    Zip = new List<Zip>()
                };

                cityXmlModel.District.Add(districtXmlModel);
            }

            var zipXmlModel = districtXmlModel.Zip.FirstOrDefault(f => f.Code == city.ZipCode);

            if (zipXmlModel == null)
            {
                zipXmlModel = new Zip
                {
                    Code = city.ZipCode
                };

                districtXmlModel.Zip.Add(zipXmlModel);
            }
        }

        return cityXmlModels;
    }
}
}

