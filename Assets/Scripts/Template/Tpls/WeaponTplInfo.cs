using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    public class WeaponTplInfo : BaseTplInfo
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("rank")]
        public int Rank { get; set; }

        [XmlElement("attack")]
        public float Attck { get; set; }

        [XmlElement("attackSpeed")]
        public float AttckSpeed { get; set; }

        [XmlElement("attackRange")]
        public float AttckRange { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("price")]
        public int Price { get; set; }

        [XmlElement("index")]
        public int Index { get; set; }

        [XmlElement("rankupWeaponID")]
        public int RankupWeaponID { get; set; }

        [XmlElement("initial")]
        public int Initial { get; set; }

    }
}
