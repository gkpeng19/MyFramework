using G.Util.Extension;
using G.Util.Net.Http;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace G.Simple.Entity
{
    [ModelBinder(typeof(EntityModelBinder))]
    [OracleOutParam("DList", "DEntity")]
    public class OracleDataProcEntity : OracleProcEntity
    {
        public OracleDataProcEntity() { }
    }

    public class DataResult
    {
        public List<GreenLand> DList { get; set; }
        public S_Order DEntity { get; set; }
    }
}
