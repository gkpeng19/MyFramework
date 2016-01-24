using G.Util.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityLibrary.Entities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HQ_DisplayContent : CommonModel
    {
        public HQ_DisplayContent()
        {
            base.TableName = "HQ_DisplayContent";
        }

        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }

        [JsonIgnore]
        public string DContent
        {
            get { return GetString("DContent"); }
            set { SetValue("DContent", value); }
        }

        [JsonIgnore]
        public int DPanelID
        {
            get { return GetInt32("DPanelID"); }
            set { SetValue("DPanelID", value); }
        }

        [JsonIgnore]
        public string DPanel_G
        {
            get { return GetString("DPanel_G"); }
            set { SetValue("DPanel_G", value); }
        }
        [JsonIgnore]
        public string ImgName_G
        {
            get { return GetString("ImgName_G"); }
            set { SetValue("ImgName_G", value); }
        }
        [JsonIgnore]
        public string Images
        {
            get { return GetString("Images"); }
            set { SetValue("Images", value); }
        }

        [JsonIgnore]
        public int DaysBeforePay
        {
            get { return GetInt32("DaysBeforePay"); }
            set { SetValue("DaysBeforePay", value); }
        }

        [JsonIgnore]
        public int DType
        {
            get { return GetInt32("DType"); }
            set { SetValue("DType", value); }
        }

        public string DType_G
        {
            get
            {
                if (DType == 1)
                {
                    return "度假村";
                }
                else if (DType == 2)
                {
                    return "养老院";
                }
                return "";
            }
        }
    }
}