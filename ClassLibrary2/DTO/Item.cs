namespace HW.Infrastructure.DTO
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "item")]
    public class Item
    {
        [XmlElement(ElementName = "guid")]
        public Guid Guid { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "pubDate")]
        public string PubDate { get; set; }
    }
}