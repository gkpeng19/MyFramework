using GOMFrameWork;
using GOMFrameWork.DataEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace G.Util
{
    public class Verifier
    {
        public static CommonResult ValidateEntity(EntityBase entity)
        {
            CommonResult result = new CommonResult() { ResultID = 1 };
            var ps = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo p in ps)
            {
                if (p.GetCustomAttributes(typeof(ValidationAttribute), true).Length > 0)
                {
                    ValidationContext vContext = new ValidationContext(entity, null, null) { MemberName = p.Name };
                    try
                    {
                        Validator.ValidateProperty(entity[p.Name], vContext);
                    }
                    catch (Exception ex)
                    {
                        result.ResultID = 0;
                        result.Message = ex.Message;
                        return result;
                    }
                }
            }

            return result;
        }
    }
}
