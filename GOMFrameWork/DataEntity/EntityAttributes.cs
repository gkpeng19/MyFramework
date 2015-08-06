using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOMFrameWork.DataEntity
{
    public class EntityChildAttribute : Attribute
    {
        internal string[] PropertyNames { get; private set; }
        public EntityChildAttribute(params string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
        }
    }

    public class EntityChildrenAttribute : EntityChildAttribute
    {
        public EntityChildrenAttribute(params string[] propertyNames)
            : base(propertyNames)
        {
        }
    }

    public class FKeyAttribute : Attribute
    {
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
