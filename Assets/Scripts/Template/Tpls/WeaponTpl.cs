using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    [XmlRoot("root")]
    public class WeaponTpl : BaseTpl<WeaponTplInfo>
    {
        protected override string TplName => "tpl_weapon";
    }
}
