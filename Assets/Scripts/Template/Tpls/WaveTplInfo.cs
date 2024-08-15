using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    public class WaveTplInfo : BaseTplInfo
    {
        [XmlElement("wave")]
        public int Wave { get; set; }

        [XmlElement("enemyID")]
        public int EnemyID { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("recycleCount")]
        public int RecycleCount { get; set; }

        [XmlElement("recycleInterval")]
        public float RecycleInterval { get; set; }

        [XmlElement("delaySpawnTime")]
        public float DelaySpawnTime { get; set; }

        [XmlElement("spawnType")]
        public int SpawnType { get; set; }

        [XmlElement("spawnInterval")]
        public float SpawnInterval { get; set; }

        [XmlElement("generatePositionX")]
        public float GeneratePositionX { get; set; }

        [XmlElement("generatePositionY")]
        public float GeneratePositionY { get; set; }

        [XmlElement("randomPosition")]
        public int RandomPosition { get; set; }

        [XmlElement("horizentalInterval")]
        public float HorizentalInterval { get; set; }

        [XmlElement("verticalInterval")]
        public float VerticalInterval { get; set; }

        [XmlElement("radius")]
        public float Radius { get; set; }

        [XmlElement("halfWidth")]
        public float HalfWidth { get; set; }

        [XmlElement("halfHeight")]
        public float HalfHeight { get; set; }
    }
}
