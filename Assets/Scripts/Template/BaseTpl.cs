using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Survivor.Template
{
    public class BaseTpl<T> where T : BaseTplInfo, new()
    {
        protected virtual string TplName { get; }

        public string TplPath
        {
            get
            {
                return "Template/" + TplName + ".xml";
            }
        }

        // 数据
        [XmlElement("data")]
        public List<T> List { get; set; }

        [XmlIgnore]
        public Dictionary<int, T> Dic { get; set; }

    }
}
