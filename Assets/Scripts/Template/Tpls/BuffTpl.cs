using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    [XmlRoot("root")]
    public class BuffTpl : BaseTpl<BuffTplInfo>
    {
        protected override string TplName => "tpl_buff";
    }
}
