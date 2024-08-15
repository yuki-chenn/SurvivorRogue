using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    [XmlRoot("root")]
    public class DropTpl : BaseTpl<DropTplInfo>
    {
        protected override string TplName => "tpl_drop";
    }
}
