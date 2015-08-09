using G.Util.Mvc;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace EntityLibrary.Entities
{
    [ModelBinder(typeof(EntityModelBinder))]
    public class HZ_ImageBook : EntityBase
    {
       public HZ_ImageBook()
       {
           base.TableName="HZ_ImageBook";
       }
                       [JsonIgnore]
                       public String Code
                        {
                            get
                            {
                                return GetString("Code");
                            }
                            set
                            {
                                SetValue("Code", value);
                            }
                        }
                       [JsonIgnore]
                       public String Name
                        {
                            get
                            {
                                return GetString("Name");
                            }
                            set
                            {
                                SetValue("Name", value);
                            }
                        }
                       [JsonIgnore]
                       public Int32 Type
                        {
                            get
                            {
                                return GetInt32("Type");
                            }
                            set
                            {
                                SetValue("Type", value);
                            }
                        }
                       [JsonIgnore]
                       public Int32 IsDelete
                        {
                            get
                            {
                                return GetInt32("IsDelete");
                            }
                            set
                            {
                                SetValue("IsDelete", value);
                            }
                        }
                       [JsonIgnore]
                       public String Remark
                        {
                            get
                            {
                                return GetString("Remark");
                            }
                            set
                            {
                                SetValue("Remark", value);
                            }
                        }
                       [JsonIgnore]
                       public DateTime CreateOn
                        {
                            get
                            {
                                return GetDateTime("CreateOn");
                            }
                            set
                            {
                                SetValue("CreateOn", value);
                            }
                        }
                        public string CreateOn_G
                        {
                            get
                            {
                                if (CreateOn == DateTime.MinValue)
                                {
                                    return string.Empty;
                                }
                                return CreateOn.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                       [JsonIgnore]
                       public String CreateBy
                        {
                            get
                            {
                                return GetString("CreateBy");
                            }
                            set
                            {
                                SetValue("CreateBy", value);
                            }
                        }
    }
}
