using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using GOMFrameWork.DataEntity;
using G.Util.Html;
using System.Web.Http;
using System.Net.Http;
using G.Simple.Entity;
using G.Util.Net.Http;

namespace G.Simple.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoadOrders")]
        public HttpResponseMessage LoadOrders(SimpleSearchModel se)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "S_Order";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            var result = se.Load<S_Order>();
            return this.JsonMessage(result);
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoadOrderItems")]
        public HttpResponseMessage LoadOrderItems(SimpleSearchModel se)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "S_OrderItem";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            var result = se.Load<S_OrderItem>();
            return this.JsonMessage(result);
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoadOrderDescs")]
        public HttpResponseMessage LoadOrderDescs(SimpleSearchModel se)
        {
            #region 查询条件初始化 Example：se["Field"] = "value";

            se.SearchID = "S_OrderDescs";
            //.........................
            se.OrderBy("id", EnumOrderBy.Desc);

            #endregion

            var result = se.Load<S_OrderDescs>();
            return this.JsonMessage(result);
        }



        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("DeleteOrder")]
        public long DeleteOrder(S_Order entity)
        {
            return entity.Delete();
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("DeleteOrderItem")]
        public long DeleteOrderItem(S_OrderItem entity)
        {
            return entity.Delete();
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("DeleteOrderDesc")]
        public long DeleteOrderDesc(S_OrderDescs entity)
        {
            return entity.Delete();
        }


        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("SaveOrderItem")]
        public HttpResponseMessage SaveOrderItem(S_OrderItem entity)
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
                return this.JsonMessage(entity);
            }
            return this.JsonMessage(new { ID = 0 });
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("SaveOrder")]
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

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        [Route("LoadOrder")]
        public HttpResponseMessage LoadOrder(int oid)
        {
            SimpleSearchModel se = new SimpleSearchModel("S_Order");
            se["ID"] = oid;
            var result = se.LoadEntity<S_Order>();
            return this.JsonMessage(new { Order = result });
        }
    }
}