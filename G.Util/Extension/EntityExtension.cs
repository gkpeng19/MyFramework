using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace G.Util.Extension
{
    [System.Web.Http.ModelBinding.ModelBinder(typeof(G.Util.Net.Http.EntityModelBinder))]
    [System.Web.Mvc.ModelBinder(typeof(G.Util.Mvc.EntityModelBinder))]
    [EntityChildren("List")]
    public class EntityList<T> : EntityBase where T : EntityBase, new()
    {
        public List<T> List { get; set; }
    }
}
