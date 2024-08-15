using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    public class HeroTplInfo : BaseTplInfo
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("heroDescription")]
        public string HeroDescription { get; set; }

        [XmlElement("skillDescription")]
        public string SkillDescription { get; set; }

        [XmlElement("passiveDescription")]
        public string PassiveDescription { get; set; }

        [XmlElement("assetIndex")]
        public int Index { get; set; }

        [XmlElement("maxHp")]
        public float MaxHp { get; set; }

        [XmlElement("attackSpeed")]
        public float AttackSpeed { get; set; }

        [XmlElement("strength")]
        public float Strength { get; set; }

        [XmlElement("intelligence")]
        public float Intelligence { get; set; }

        [XmlElement("defense")]
        public float Defense { get; set; }

        [XmlElement("dodge")]
        public float Dodge { get; set; }

        [XmlElement("critical")]
        public float Critical { get; set; }

        [XmlElement("criticalDamage")]
        public float CriticalDamage { get; set; }

        [XmlElement("moveSpeed")]
        public float MoveSpeed { get; set; }

        [XmlElement("luck")]
        public float Luck { get; set; }

        [XmlElement("incMaxHp")]
        public float IncMaxHp { get; set; }

        [XmlElement("incAttackSpeed")]
        public float IncAttackSpeed { get; set; }

        [XmlElement("incStrength")]
        public float IncStrength { get; set; }

        [XmlElement("incIntelligence")]
        public float IncIntelligence { get; set; }

        [XmlElement("incDefense")]
        public float IncDefense { get; set; }

        [XmlElement("incDodge")]
        public float IncDodge { get; set; }

        [XmlElement("incCritical")]
        public float IncCritical { get; set; }

        [XmlElement("incCriticalDamage")]
        public float IncCriticalDamage { get; set; }

        [XmlElement("incMoveSpeed")]
        public float IncMoveSpeed { get; set; }

        [XmlElement("incLuck")]
        public float IncLuck { get; set; }

        [XmlElement("salary")]
        public int Salary { get; set; }

    }
}
