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
    public class Sh_HZ_Product : SearchEntity
    {
        public Sh_HZ_Product()
        {
            base.SearchID = "uv_hz_product";
            this.IsDelete = 0;
        }

        [Search(Operator = SearchOperator.IsNullZeroEqual)]
        public int IsDelete
        {
            set { SetValue("IsDelete", value); }
        }

        [Search(Field = "ProductCategory", Operator = SearchOperator.In)]
        public string CategoryIds
        {
            set { SetValue("CategoryIds", value); }
        }
    }
}