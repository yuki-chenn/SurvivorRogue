using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    public class ItemTplInfo : BaseTplInfo
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("rank")]
        public int Rank { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("price")]
        public int Price { get; set; }

        [XmlElement("index")]
        public int Index { get; set; }

        [XmlElement("initial")]
        public int Initial { get; set; }

        [XmlElement("maxOwnCount")]
        public int MaxOwnCount { get; set; }

    }
}
