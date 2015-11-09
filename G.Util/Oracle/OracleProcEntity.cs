using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Oracle
{
    [System.Web.Http.ModelBinding.ModelBinder(typeof(G.Util.Net.Http.EntityModelBinder))]
    [System.Web.Mvc.ModelBinder(typeof(G.Util.Mvc.EntityModelBinder))]
    public class OracleResultProcEntity : OracleProcEntity
    {
        public OracleResultProcEntity() { }
        public OracleResultProcEntity(string procName)
            : base(procName)
        {
        }
    }

    [System.Web.Http.ModelBinding.ModelBinder(typeof(G.Util.Net.Http.EntityModelBinder))]
    [System.Web.Mvc.ModelBinder(typeof(G.Util.Mvc.EntityModelBinder))]
    [OracleOutParam("o_cursor")]
    public class OracleListProcEntity : OracleProcEntity
    {
        public OracleListProcEntity() { }
        public OracleListProcEntity(string procName)
            : base(procName)
        {
        }
    }

    [System.Web.Http.ModelBinding.ModelBinder(typeof(G.Util.Net.Http.EntityModelBinder))]
    [System.Web.Mvc.ModelBinder(typeof(G.Util.Mvc.EntityModelBinder))]
    [OracleOutParam("o_cursor", "o_count")]
    public class OraclePagerProcEntity : OracleProcEntity
    {
        public OraclePagerProcEntity() { }
        public OraclePagerProcEntity(string procName)
            : base(procName)
        {
        }
    }
}
