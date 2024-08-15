using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    [XmlRoot("root")]
    public class HeroTpl: BaseTpl<HeroTplInfo>
    {
        protected override string TplName => "tpl_hero";
    }
}
