using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    public class DropTplInfo : BaseTplInfo
    {
        [XmlElement("money")]
        public int Money { get; set; }

        [XmlElement("healthPotionRate")]
        public float HealthPotionRate { get; set; }

        [XmlElement("rareChestRate")]
        public float RareChestRate { get; set; }

        [XmlElement("epicChestRate")]
        public float EpicChestRate { get; set; }

        [XmlElement("legendChestRate")]
        public float LegendChestRate { get; set; }
    }
}
