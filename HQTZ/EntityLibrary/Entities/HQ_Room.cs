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
    public class HQ_Room : CommonModel
    {
        public HQ_Room()
        {
            base.TableName = "HQ_Room";
        }

        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
        [JsonIgnore]
        public decimal Price
        {
            get { return GetDecimal("Price"); }
            set { SetValue("Price", value); }
        }
        [JsonIgnore]
        public int RCount
        {
            get { return GetInt32("RCount"); }
            set { SetValue("RCount", value); }
        }

        [JsonIgnore]
        public string Description
        {
            get { return GetString("Description"); }
            set { SetValue("Description", value); }
        }

        [JsonIgnore]
        public int VillageID
        {
            get { return GetInt32("VillageID"); }
            set { SetValue("VillageID", value); }
        }

        [JsonIgnore]
        public string Village_G
        {
            get { return GetString("Village_G"); }
            set { SetValue("Village_G", value); }
        }
    }
}
