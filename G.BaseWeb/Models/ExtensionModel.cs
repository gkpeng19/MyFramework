using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G.BaseWeb.Models
{
    [ModelBinder(typeof(EntityModelBinder))]
    [EntityChildren("Contexts")]
    public class TransferModel<T>:BaseModel where T:BaseModel
    {
        public List<T> Contexts { get; set; }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    [EntityChildren("Contexts")]
    public class ReferenceModel<T>:BaseModel where T:BaseModel
    {
        public List<T> Contexts { get; set; }
    }
}