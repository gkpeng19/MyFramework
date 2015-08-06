using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triage.Models
{
    public class BaseLrpEntity : EntityBase { }
    public class BaseLrpSearchEntity : SearchEntity { }

    [ModelBinder(typeof(EntityModelBinder))]
    public class BaseTriageEntity : EntityBase { }

    [ModelBinder(typeof(EntityModelBinder))]
    public class BaseTriageSearchEntity : SearchEntity
    {
        public BaseTriageSearchEntity() { }
        public BaseTriageSearchEntity(string searchID) : base(searchID) { }
    }

    [ModelBinder(typeof(EntityModelBinder))]
    public class BaseTriageProcEntity : ProcEntity
    {
        public BaseTriageProcEntity() { }
        public BaseTriageProcEntity(string procName) : base(procName) { }
    }
}