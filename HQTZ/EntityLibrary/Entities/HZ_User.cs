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
    public class HZ_User : EntityBase
    {
       public HZ_User()
       {
           base.TableName="HZ_User";
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
                       public String UPassword
                        {
                            get
                            {
                                return GetString("UPassword");
                            }
                            set
                            {
                                SetValue("UPassword", value);
                            }
                        }
                       [JsonIgnore]
                       public Int32 UserLevel
                        {
                            get
                            {
                                return GetInt32("UserLevel");
                            }
                            set
                            {
                                SetValue("UserLevel", value);
                            }
                        }
                       [JsonIgnore]
                       public Int32 UserType
                        {
                            get
                            {
                                return GetInt32("UserType");
                            }
                            set
                            {
                                SetValue("UserType", value);
                            }
                        }
                       [JsonIgnore]
                       public String Email
                        {
                            get
                            {
                                return GetString("Email");
                            }
                            set
                            {
                                SetValue("Email", value);
                            }
                        }
                       [JsonIgnore]
                       public String Phone
                        {
                            get
                            {
                                return GetString("Phone");
                            }
                            set
                            {
                                SetValue("Phone", value);
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
    }
}
