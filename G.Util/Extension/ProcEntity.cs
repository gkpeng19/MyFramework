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
    public sealed class SqlProcEntity : ProcEntity
    {
        public SqlProcEntity() { }
        public SqlProcEntity(string procName)
            : base(procName)
        {
        }
    }

    [System.Web.Http.ModelBinding.ModelBinder(typeof(G.Util.Net.Http.EntityModelBinder))]
    [System.Web.Mvc.ModelBinder(typeof(G.Util.Mvc.EntityModelBinder))]
    public class OracleProcEntity : ProcEntity
    {
        public OracleProcEntity() { }
        public OracleProcEntity(string procName)
            : base(procName)
        {
        }
    }

    [System.Web.Http.ModelBinding.ModelBinder(typeof(G.Util.Net.Http.EntityModelBinder))]
    [System.Web.Mvc.ModelBinder(typeof(G.Util.Mvc.EntityModelBinder))]
    [OracleOutParam("o_cursor")]
    public sealed class OracleListProcEntity : OracleProcEntity
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
    public sealed class OraclePagerProcEntity : OracleProcEntity
    {
        public OraclePagerProcEntity() { }
        public OraclePagerProcEntity(string procName)
            : base(procName)
        {
        }
    }
}
