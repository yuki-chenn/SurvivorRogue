using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    [XmlRoot("root")]
    public class EnemyTpl : BaseTpl<EnemyTplInfo>
    {
        protected override string TplName => "tpl_enemy";
    }
}
