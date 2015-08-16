using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntityLibrary.SearchEntities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class Sh_HZ_Context : SearchEntity
    {
        public Sh_HZ_Context()
        {
            base.SearchID = "HZ_Context";
            this.IsDelete = 0;
        }

        [Search(Operator = SearchOperator.IsNullZeroEqual)]
        public int IsDelete
        {
            set { SetValue("IsDelete", value); }
        }

        public int CType
        {
            set { SetValue("CType", value); }
        }
    }


    public class Sh_HZ_Info : SearchEntity
    {
        public Sh_HZ_Info()
        {
            base.SearchID = "uv_hz_info";
            this.IsDelete = 0;
            this["CType"] = (int)ContextTypeEnum.Info;
        }

        [Search(Operator = SearchOperator.IsNullZeroEqual)]
        public int IsDelete
        {
            set { SetValue("IsDelete", value); }
        }

        [Search(Field = "CategoryID", Operator = SearchOperator.In)]
        public string CategoryIds
        {
            set { SetValue("CategoryIds", value); }
        }
    }
}