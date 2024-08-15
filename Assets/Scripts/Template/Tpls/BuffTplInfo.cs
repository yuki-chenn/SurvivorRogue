using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    public class BuffTplInfo : BaseTplInfo
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("maxStack")]
        public int MaxStack { get; set; }

        [XmlElement("duration")]
        public float Duration { get; set; }

        [XmlElement("tickTime")]
        public float TickTime { get; set; }

        [XmlElement("priority")]
        public int Priority { get; set; }

        [XmlElement("maxTickCount")]
        public int MaxTickCount { get; set; }

        [XmlElement("addType")]
        public int AddType { get; set; }

        [XmlElement("removeType")]
        public int RemoveType { get; set; }

    }
}
