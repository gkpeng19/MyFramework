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
    public class HQ_Article : CommonModel
    {
        public HQ_Article()
        {
            base.TableName = "HQ_Article";
        }

        [JsonIgnore]
        public string Title
        {
            get { return GetString("Title"); }
            set { SetValue("Title", value); }
        }
        [JsonIgnore]
        public string TitleImg
        {
            get { return GetString("TitleImg"); }
            set { SetValue("TitleImg", value); }
        }
        [JsonIgnore]
        public string AContent
        {
            get { return GetString("AContent"); }
            set { SetValue("AContent", value); }
        }
        [JsonIgnore]
        public int ACategory
        {
            get { return GetInt32("ACategory"); }
            set { SetValue("ACategory", value); }
        }
    }
}
