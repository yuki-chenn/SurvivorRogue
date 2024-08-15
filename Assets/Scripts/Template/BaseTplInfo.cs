using System.Xml.Serialization;

namespace Survivor.Template
{
    public class BaseTplInfo
    {
        [XmlElement("ID")]
        public int ID { get; set; }
    }
}
