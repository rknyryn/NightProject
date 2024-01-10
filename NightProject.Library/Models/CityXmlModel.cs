using System;
using System.Xml.Serialization;

namespace NightProject.Library.Models;

[XmlRoot(ElementName = "Zip")]
public class Zip
{

    [XmlAttribute(AttributeName = "code")]
    public int Code { get; set; }
}

[XmlRoot(ElementName = "District")]
public class District
{

    [XmlElement(ElementName = "Zip")]
    public List<Zip> Zip { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "City")]
public class City : IFileOperationModel
{

    [XmlElement(ElementName = "District")]
    public List<District> District { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "code")]
    public int Code { get; set; }
}

