using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*JSON、Json.Net学习
 * --JSON中类型基本上有三种:值类型,数组和对象
 * --Json.Net将.Net中的基本类型(int,float,string等)转换为Json的值；
 * --数组和集合转换为Json的数组,其它转换为Json对象。
 * ================================================
 * 如果你自定义了实现了数组和集合的类,并为类添加了自己的属性,该属性不会被序列化：
   public class MyArray : ArrayList
   {
        public string Name { get; set; }
   }
 * 把数组以对象的形式序列化，只要在定义的数组类的前面加上特性"JsonObject"即可。
 * [JsonObject]
    public class MyArray : ArrayList
    {
        public string Name { get; set; }
    }
 * 默认情况下,Json.Net是仅仅序列化公有成员。
 * 如果想要非公有成员也被序列化,就要在该成员上加特性"JsonProperty"
 * ============================================
 * Json.Net是支持序列化和反序列化DataTable,DataSet,Entity Framework和NHibernate。
 * ============================================
 * --Json.Net序列化的模式:OptOut 和 OptIn.
    --OptOut  默认值,类中所有公有成员会被序列化,如果不想被序列化,可以用特性JsonIgnore 
    --OptIn 默认情况下,所有的成员不会被序列化,类中的成员只有标有特性JsonProperty的才会被序列化,当类的成员很多,但客户端仅仅需要一部分数据时,很有用 
 * 更改序列化模式：[JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
 * =============================================
 * 一.空值的处理
   --NullValueHandling.Ignore     忽略为NULL的值 
   --NullValueHandling.Include       默认值,包括为NULL的值 
   eg：var jsSetting = new JsonSerializerSettings();
          jsSetting.NullValueHandling = NullValueHandling.Ignore;
          string json = JsonConvert.SerializeObject(jack,jsSetting);
 * -------------------------------------------------------------------
 * 二.默认值的处理
   --DefaultValueHandling.Ignore        序列化和反序列化时,忽略默认值 
   --DefaultValueHandling.Include        序列化和反序列化时,包含默认值 
   eg：[DefaultValue(30)]
          public int Age { get; set; }
 * --------------------------------------------------------------------
 * 三.日期处理
   --IsoDateTimeConverter 和 JavaScriptDateTimeConverter
   --是Json.Net中自带的两个处理日期的类。
   --默认是IsoDateTimeConverter ,它的格式是"yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK"
   --另一个是JavaScriptTimeConverter,它的格式是 "new Date(ticks)",其实返回的是一个JavaScript的Date对象.
   --有两种方式来应用JsonConverter,改变Json序列化和反序列化的行为.
   eg1：string json = JsonConvert.SerializeObject(jack,new JavaScriptDateTimeConverter());
   eg2：如果想要不同的日期类型成员序列化后,以不同的形式显示.
            [JsonConverter(typeof(IsoDateTimeConverter))]
            public DateTime BirthDate { get; set; }
            [JsonConverter(typeof(JavaScriptDateTimeConverter))]
            public DateTime EmploymentDate { get; set; }
   另：自定义日期格式
          IsoDateTimeConverter dtConverter = new IsoDateTimeConverter 
          {
                DateTimeFormat = "yyyy'年'MM'月'dd'日'"
          };
          string json = JsonConvert.SerializeObject(jack,dtConverter);
 * ======================================================
 * Linq to JSON：
 * Linq to JSON是用来操作JSON对象的.可以用于快速查询,修改和创建JSON对象.
 * 当JSON对象内容比较复杂,而我们仅仅需要其中的一小部分数据时,可以考虑使用Linq to JSON来读取和修改部分的数据而非反序列化全部.
 * --------------------------------------------------------------------------
    在进行Linq to JSON之前,首先要了解一下用于操作Linq to JSON的类.
    JObject     用于操作JSON对象；
    JArray      用语操作JSON数组；
    JValue      表示数组中的值；
    JProperty   表示对象中的属性,以"key/value"形式；
    JToken  用于存放Linq to JSON查询后的结果；
 * ------------------------------------------------------------
 * 1.创建JSON对象
    JObject staff = new JObject();
    staff.Add(new JProperty("Name", "Jack"));
    staff.Add(new JProperty("Age", 33));
    staff.Add(new JProperty("Department", "Personnel Department"));
    staff.Add
    (
       new JProperty
       (
           "Leader",
           new JObject
           (
              new JProperty("Name", "Tom"), 
                 new JProperty("Age", 44),
               new JProperty("Department", "Personnel Department")
           )
        )
    );
    除此之外，还可以通过一下方式来获取JObject，JArray类似。
    JObject.Parse(string json)      json含有JSON对象的字符串，返回为JObject对象 
    JObject.FromObject(object o)        o为要转化的对象，返回一个JObject对象
    JObject.Load(JsonReader reader)     reader包含着JSON对象的内容，返回一个JObject对象 
 * ----------------------------------------------------------------------------------
 * 2、创建JSON数组
        JArray arr = new JArray();
        arr.Add(new JValue(1));
        arr.Add(new JValue(2));
        arr.Add(new JValue(3));
        等同于：JArray arr = new JArray() { new JValue(1), new JValue(2), new JValue(3) };
 * -----------------------------------------------------------------------------------
 * 3、使用Linq to JSON
         首先准备Json字符串，是一个包含员工基本信息的Json：
         string json = "{\"Name\" : \"Jack\", \"Age\" : 34, \"Colleagues\" : [{\"Name\" : \"Tom\" , \"Age\":44},{\"Name\" : \"Abel\",\"Age\":29}] }";
         一、查询
            ①获取该员工的姓名
            //将json转换为JObject
            JObject jObj = JObject.Parse(json);
            //通过属性名或者索引来访问，仅仅是自己的属性名，而不是所有的
            JToken ageToken =  jObj["Age"];
            Console.WriteLine(ageToken.ToString());

            ②获取该员工同事的所有姓名
            //将json转换为JObject
            JObject jObj = JObject.Parse(json);
            var names=from staff in jObj["Colleagues"].Children()
                               select (string)staff["Name"];
            foreach (var name in names)
                Console.WriteLine(name);
         二、修改
            ①现在我们发现获取的json字符串中Jack的年龄应该为35
            //将json转换为JObject
            JObject jObj = JObject.Parse(json);
            jObj["Age"] = 35;
            Console.WriteLine(jObj.ToString());

            注意不要通过以下方式来修改：
            JObject jObj = JObject.Parse(json);
            JToken age = jObj["Age"];
            age = 35;

            ②现在我们发现Jack的同事Tom的年龄错了，应该为45
            //将json转换为JObject
            JObject jObj = JObject.Parse(json);
            JToken colleagues = jObj["Colleagues"];
            colleagues[0]["Age"] = 45;
            jObj["Colleagues"] = colleagues;//修改后，再赋给对象
            Console.WriteLine(jObj.ToString());
         三、删除
            ①现在我们想删除Jack的同事
            JObject jObj = JObject.Parse(json);
            jObj.Remove("Colleagues");//跟的是属性名称
            Console.WriteLine(jObj.ToString());

            ②现在我们发现Abel不是Jack的同事，要求从中删除
            JObject jObj = JObject.Parse(json);
            jObj["Colleagues"][1].Remove();
 
         四、添加
            ①我们发现Jack的信息中少了部门信息，要求我们必须添加在Age的后面
            //将json转换为JObject
            JObject jObj = JObject.Parse(json);
            jObj["Age"].Parent.AddAfterSelf(new JProperty("Department", "Personnel Department"));

            ②现在我们又发现，Jack公司来了一个新同事Linda
            //将json转换为JObject
            JObject jObj = JObject.Parse(json);
            JObject linda = new JObject(new JProperty("Name", "Linda"), new JProperty("Age", "23"));
            jObj["Colleagues"].Last.AddAfterSelf(linda);
 * -------------------------------------------------------------------------
 * 四、简化查询语句
          使用函数SelectToken可以简化查询语句，具体：
          ①利用SelectToken来查询名称
            JObject jObj = JObject.Parse(json);
            JToken name = jObj.SelectToken("Name");
            Console.WriteLine(name.ToString());
          ②利用SelectToken来查询所有同事的名字
            JObject jObj = JObject.Parse(json);
            var names = jObj.SelectToken("Colleagues").Select(p => p["Name"]).ToList();
            foreach (var name in names)
                Console.WriteLine(name.ToString());
          ③查询最后一名同事的年龄
            //将json转换为JObject
            JObject jObj = JObject.Parse(json);
            var age = jObj.SelectToken("Colleagues[1].Age");
            Console.WriteLine(age.ToString());
 */

namespace System.Runtime.CompilerServices
{
    public class ExtensionAttribute : Attribute { }
}

namespace G.Util
{
    /// <summary>
    /// 使用Json.Net类库
    /// </summary>
    public static class JSON
    {
        public static string Stringify(object entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public static string Stringify(object entity, JsonSerializerSettings jsSettings)
        {
            return JsonConvert.SerializeObject(entity, Formatting.None, jsSettings);
        }

        public static string Stringify(object entity, params JsonConverter[] jConverters)
        {
            return JsonConvert.SerializeObject(entity, jConverters);
        }

        public static T JsonBack<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        public static T JsonBack<T>(string jsonStr, JsonSerializerSettings jsSettings)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr, jsSettings);
        }

        public static T JsonBack<T>(string jsonStr, params JsonConverter[] jConverters)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr, jConverters);
        }

        public static object GetPropertyValue(this string jsonStr, string PropertyName)
        {
            JObject obj = JObject.Parse(jsonStr);
            return obj[PropertyName];
        }

        /*JsonConvert.SerializeObject的内部实现
         * ==============================
         * JsonSerializer js = new JsonSerializer();
         * StringBuilder sb = new StringBuilder();
         * StringWriter sw=new StringWriter(sb); 
         * JsonWriter writer= new JsonTextWriter(sw);
         * js.Serialize(writer, entity);
         * return sb.ToString();
         */
    }
}
