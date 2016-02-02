using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntityLibrary.Entities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_FLink : EntityBase
    {
        public HQ_FLink()
        {
            base.TableName = "HQ_FLink";
        }
        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
        [JsonIgnore]
        public string Link
        {
            get { return GetString("Link"); }
            set { SetValue("Link", value); }
        }
        [JsonIgnore]
        public string Img
        {
            get { return GetString("Img"); }
            set { SetValue("Img", value); }
        }
        [JsonIgnore]
        public int LinkType
        {
            get { return GetInt32("LinkType"); }
            set { SetValue("LinkType", value); }
        }
    }
}
