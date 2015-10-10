using G.Util.Mvc;
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
    public class HQ_Agenter:CommonModel
    {
        public HQ_Agenter()
        {
            base.TableName = "HQ_Agenter";
        }

        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }

        [JsonIgnore]
        public string AContent
        {
            get { return GetString("AContent"); }
            set { SetValue("AContent", value); }
        }
    }
}
