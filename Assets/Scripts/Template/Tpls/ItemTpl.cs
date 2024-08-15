using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Survivor.Template
{
    [XmlRoot("root")]
    public class ItemTpl : BaseTpl<ItemTplInfo>
    {
        protected override string TplName => "tpl_item";
    }
}
