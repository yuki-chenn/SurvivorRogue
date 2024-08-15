using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    public class EnemyTplInfo : BaseTplInfo
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("maxHp")]
        public float MaxHp { get; set; }

        [XmlElement("defense")]
        public float Defense { get; set; }

        [XmlElement("moveSpeed")]
        public float MoveSpeed { get; set; }

        [XmlElement("attack")]
        public float Attack { get; set; }

        [XmlElement("gainExp")]
        public int GainExp { get; set; }

        [XmlElement("isBoss")]
        public int IsBoss { get; set; }

        [XmlElement("index")]
        public int Index { get; set; }

        [XmlElement("dropId")]
        public int DropId { get; set; }
    }
}
