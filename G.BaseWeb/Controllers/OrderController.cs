using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Mvc;
using G.BaseWeb.Models;

namespace Project1.Controllers
{
    public class OrderController : Controller
    {
        public JsonResult LoadOrders(SimpleSearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "S_Order";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<S_Order>();
            return ExController.JsonNet(result);
        }


        public JsonResult LoadOrderItems(SimpleSearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "S_OrderItem";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;
            var result = se.Load<S_OrderItem>();
            return ExController.JsonNet(result);
        }

        public JsonResult LoadOrderDescs(SimpleSearchModel se, int page_g, int psize_g)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "S_OrderDescs";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            se.PageIndex = page_g;
            se.PageSize = psize_g;

            var result = se.Load<S_OrderDescs>();
            return ExController.JsonNet(result);
        }




        public long DeleteOrder(S_Order entity)
        {
            return entity.Delete();
        }


        public long DeleteOrderItem(S_OrderItem entity)
        {
            return entity.Delete();
        }


        public long DeleteOrderDesc(S_OrderDescs entity)
        {
            return entity.Delete();
        }



        public JsonResult SaveOrderItem(S_OrderItem entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                #endregion
            }
            else
            {
                #region 维护修改数据

                #endregion
            }

            var r = entity.Save();
            if (r > 0)
            {
                return ExController.JsonNet(entity);
            }
            return ExController.JsonNet(new { ID = 0 });
        }


        public long SaveOrder(S_Order entity)
        {
            if (entity.ID == 0)
            {
                #region 维护新增数据

                #endregion
            }
            else
            {
                #region 维护修改数据

                #endregion
            }

            var r = entity.Save();
            return r;
        }

        public JsonResult LoadOrder(int oid)
        {
            SimpleSearchModel se = new SimpleSearchModel("S_Order");
            se["ID"] = oid;
            var result = se.LoadEntity<S_Order>();
            return this.JsonNet(new { Order = result });
        }
    }
}