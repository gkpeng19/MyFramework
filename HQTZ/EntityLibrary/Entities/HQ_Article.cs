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

        public string AContent_G
        {
            get
            {
                var content = System.Text.RegularExpressions.Regex.Replace(AContent, "<[^>]*>", "");
                content = System.Text.RegularExpressions.Regex.Replace(content, @"&(nbsp|#160);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                content = System.Text.RegularExpressions.Regex.Replace(content, @"　+", "");
                content = System.Text.RegularExpressions.Regex.Replace(content, @" +", "");
                if (content.Length >40)
                {
                    content = content.Substring(0, 30);
                }
                var cindex = content.LastIndexOf('<');
                if (cindex >= 0)
                {
                    content = content.Substring(0, cindex);
                }
                return content;
            }
        }

        [JsonIgnore]
        public int ACategory
        {
            get { return GetInt32("ACategory"); }
            set { SetValue("ACategory", value); }
        }
        [JsonIgnore]
        public int RefID
        {
            get { return GetInt32("RefID"); }
            set { SetValue("RefID", value); }
        }
    }
}
