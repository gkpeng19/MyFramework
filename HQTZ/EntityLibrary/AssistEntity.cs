using EntityLibrary.Entities;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntityLibrary
{
    [ModelBinder(typeof(EntityModelBinder))]
    [EntityChildren("Contexts")]
    public class EntityContextInfos : EntityBase
    {
        public List<HZ_Context> Contexts { get; set; }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    [EntityChildren("Products")]
    public class EntityProducts:EntityBase
    {
        public List<HZ_Product> Products { get; set; }
    }
}
