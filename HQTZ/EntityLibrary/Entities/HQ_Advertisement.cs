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
    public class HQ_Advertisement : EntityBase
    {
        public HQ_Advertisement()
        {
            base.TableName = "HQ_Advertisement";
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
        public string ImgPath
        {
            get { return GetString("ImgPath"); }
            set { SetValue("ImgPath", value); }
        }
        [JsonIgnore]
        public string Description
        {
            get { return GetString("Description"); }
            set { SetValue("Description", value); }
        }
    }
}
