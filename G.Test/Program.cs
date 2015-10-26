using G.Util.Redis;
using G.Util.Tool;
using GOMFrameWork;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace G.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Student s = new Student();
            //s.SetUIValue("ID"/ 1);
            //s.Name = "gkpeng";
            //s.Age = 18;
            //s.Time = DateTime.Now;
            //s.Price = 12.88M;
            //s.PriceD = 12.88;
            //s.Save();

            //s.Age = 20;
            //s.PriceD = 20.20;
            //s.Price = 20.20M;
            //s.Time = DateTime.Now.AddDays(5);
            //s.Name = "kiven";
            //s.Save();

            //RedisClient master = RedisManager.GetMaster();
            //RedisClient selve = RedisManager.GetClient();

            //master.Set<string>("test"/ "aa");
            //var v = selve.Get<string>("test");
            //System.Diagnostics.Debug.WriteLine(v);

            //RedisRepository res = new RedisRepository();
            //var v = res.Exist<Student>();
            //Console.WriteLine(v);
            //var list= res.GetAll<Student>();

            //var s1= new Student();
            //s1.SetUIValue("ID"/1);
            //var target = res.Find<Student>(s1);

            //var list= RedisRepository.Default.GetAll<Student>();

            string pagehtml = Pager.InitLinkPager(2, 15);

            return;

            DbContext.InitContext<Park>("ParkConnection");

            List<string> headers = new List<string>{
                "ID","Name","COMPLETETIME",
                "OPENDATE","PARKGRADE","HIGHQUALITYPARK","FREEPARK","PROPERTY","PEAKLOWSEASON","ONOFFTIMEPEAK","ONOFFLOW","OWEDDISTINCE","OWEDSTREET",
                "DETAILADDRESS","ZIPCODE","TRAFFICWAY","INTRODUCT","FEATURESPOT","TOTALAREA","WATERAREA","LANDAREA","BUILDAREA","ANCIENTBUILDAREA",
                "EXTEBUILDAREA","ADMINBUILDAREA","LANDSCAPEAREA","MANAGEBUILDAREA","GREENSPACEAREA","MATAREA","LENPAVEROAD","SQUAREPAVEAREA","GREENRATE","GREENCOVERAGE",
                "RATEGREENCOVERAGE","TURFAREA","FLOWGRASSAREA","FLOWVAR","TREESNUM","RBORNUM","SHURBNUM","ANCIENTTREENUM","SIGNALBOARDNUM","MULSIGBOARD",
                "CHAIRNUM","GARBAGENUM","LAMPNUM","FIRSTWCNUM","SECONDWCNUM","OTHERWCNUM","TOTALWCNUM","URINALNUM","MALEWCNUM","FEMALEWCNUM",
                "PARK","INNERPARKNUM","BESIDEPARKNUM","BROADFAC","BIKEPARK","BIKEPARKNUM","BULLECTINBOARDNUM","BULLECTINSCREENNUM","BODYBUILDEQUNUM","ATHLETICGROUND",
                "ATHLETICGROUNDNUM","BODYBUILDWALKLEN","SOLARLAMPNUM","VOLTAICPLANT","ANNALLPOWERPLANT","SOLARLAMPANNUSEPOWER","TOTALANNUALPOWER","COLLECTRAINWAY","COLLECTRAINAREA","ANNUALWATERSUM",
                "RECLAIMEDWATER","RECLAIMEDWATERAPP","ANNUSECOUNTREWATER","REWATERHADLEFAC","REWATERHANDLEFACINTR","SPRINKAREA","DRIPAREA","GEOTHERMAL","GEOTHERMALUSEINTR","OTHERSAVEENERWAYINTR",
                "SPECIALWICKETNUM","SPECIALPARKNUM","WHEELCHAIRNUM","RAMPNUM","RAMPLEN","LOWPOOLNUM","UNSEXWCNUM","ACCWCNUM","LOWTELNUM",
                "OTHERACCFACINTR","INDEPTADMIOFFICE","ADMOFFICENAME","ADMAGENCYLEVEL","SUPERADMDEPT","ONSTAFFNUM","TECPERSONNUM","INDEPTLEGALENTITY","CHARGEPERSONNAME","CONTRACTPERSONNAME",
                "FIXEDPHONE","MOBILETEL","CONSULTPHONE","FAX","Email","WEBSET","LANDBELOGN","ANNCONSINVE","RELCATIONCOST","COMULATIVEINVE",
                "CONSINVESSOURCE","OVERALLPLAN","PLANAPPROVEDEPT","OPENTYPE","MONICAMERNUM","EMERSHELTER","EMERSHELTERCAPA","BUFFETNUM","BUFFETTOTALAREA","RESTAURANTNUMBER",
                "RESTAURANTAREA","HOTELNUMBER","HOTELAREA","HOTELFLOORAREA","OLDBRAND","OLDBRANDINTRO","BOATNUM","ACTIVITYSPACENUM","CABLECAR","CABLEWAYLEN",
                "SLIDEWAY","SLIDEWAYLEN","AMUSEFAC","AMUSEFACINTR","ICEACTIVITY","ENTRANCETICKETPRICE","OFFSEASONTICKETPRIC","PEAKSEASONTICKETPRICE","COUPONPRICE","MONTHTICKETPRICE",
                "SEASONTICKETPRIC","ANNUALTICKETPRICE","SPEPERSONDISCOUNTINTR","GARDENNUM","GARDENNAME","GARDENTICKETPRICE","ENTRANCETICKETREVENUE","OFFSEASONTICKETREVENUE","PEAKSEASONTICKETREVENUE","COUPONREVENUE",
                "MONTHTICKETREVENUE","SEASONTICKETREVENUE","ANNTICKETREVENUE","SPEPERSONDISCOUNTNUMINTRO","GARDENTICKETREVENUE","IMPORHOLIDAYTOURISTNUM","GARDENTOURISTNUM","BOATTYPE","BOATTICKETPRICE","HOLDICEACTIVITYANN",
                "PROJECTNAME","PROJECTTYPE","PROJECTINVE"
            };

            for (var i = headers.Count - 1; i >= 0; --i)
            {
                var key = headers[i];
                for (var j = i - 1; j >= 0; --j)
                {
                    if (headers[j] == key)
                    {
                        headers.RemoveAt(i);
                        break;
                    }
                }
            }

            var path = @"C:\Users\gkpeng\Desktop\parks.xlsx";
            var parkList = ExcelHelper.Read<Park>(path, headers.ToArray(), 1);
        }
    }

    public class Park : GOMFrameWork.DataEntity.EntityBase
    {
        public Park()
        {
            base.TableName = "tab_registerpark";
            base.PrimaryKey = "iid";
        }

        protected override bool IsNullUIValue(string key, object value)
        {
            string v = value.ToString();
            if (v.Length == 0)
            {
                return true;
            }
            return false;
        }

        public override void SetUIValue(string key, object value)
        {
            var newvalue = value.ToString();
            var item = Collection[key];
            if (item == null)
            {
                if (!IsNullUIValue(key, newvalue))
                {
                    Collection[key] = new EntityItem() { Key = key, Value = newvalue };
                }
            }
            else
            {
                if (IsNullUIValue(key, newvalue))
                {
                    if (ID > 0)
                    {
                        //清空数据库字段中的值
                        (item as EntityItem).Value = DBNull.Value;
                    }
                    else
                    {
                        Collection.Remove(key);
                    }
                }
                else
                {
                    (item as EntityItem).Value = newvalue;
                }
            }
        }
    }
}
