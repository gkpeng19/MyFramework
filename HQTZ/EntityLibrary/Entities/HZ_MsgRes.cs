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
    public class HZ_MsgRes : EntityBase
    {
       public HZ_MsgRes()
       {
           base.TableName="HZ_MsgRes";
       }
                       [JsonIgnore]
                       public String Context
                        {
                            get
                            {
                                return GetString("Context");
                            }
                            set
                            {
                                SetValue("Context", value);
                            }
                        }
                       [JsonIgnore]
                       public Int32 UserID
                        {
                            get
                            {
                                return GetInt32("UserID");
                            }
                            set
                            {
                                SetValue("UserID", value);
                            }
                        }
                       [JsonIgnore]
                       public String UserName
                        {
                            get
                            {
                                return GetString("UserName");
                            }
                            set
                            {
                                SetValue("UserName", value);
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
    }
}
