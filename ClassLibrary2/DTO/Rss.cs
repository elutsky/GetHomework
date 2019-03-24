namespace HW.Infrastructure.DTO
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "rss")]
    public class Rss
    {
        [XmlElement(ElementName = "channel")]
        public Channel Channel { get; set; }
        [XmlAttribute(AttributeName = "a10", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string A10 { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

}