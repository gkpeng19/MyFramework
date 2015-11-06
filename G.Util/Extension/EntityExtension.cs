using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G.Util.Extension
{
    [ModelBinder(typeof(EntityModelBinder))]
    [EntityChildren("List")]
    public class EntityList<T> : EntityBase where T : EntityBase, new()
    {
        public List<T> List { get; set; }
    }
}
