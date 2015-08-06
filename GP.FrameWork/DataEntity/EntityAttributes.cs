using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.FrameWork.DataEntity
{
    public class EntityDetailsAttribute : Attribute
    {
        public string FTableName { get; set; }
        public string FKey { get; set; }
    }

    public class SearchAttribute : Attribute
    {
        public SearchAttribute() { }

        public String Field { get; set; }
        public SearchOperator Operator { get; set; }
    }

    public class UIValueZeroNotEqualNullAttribute : Attribute { }
}
