using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTest.Controllers
{
    public class SelectItem
    {
        public string k { get; set; }
        public string v { get; set; }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Desc { get; set; }

        public string Gender_G
        {
            get
            {
                return Gender == 1 ? "男" : "女";
            }
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetJsonData()
        {
            List<SelectItem> genders = new List<SelectItem>();
            genders.Add(new SelectItem() { k = "男", v = "1" });
            genders.Add(new SelectItem() { k = "女", v = "2" });

            return Json(new { Genders = genders });
        }

        public JsonResult GetStudents(int page, int psize)
        {
            List<Student> list = new List<Student>();
            for (var i = (page - 1) * psize; i < page * psize; ++i)
            {
                list.Add(new Student()
                {
                    ID = i,
                    Code = "Code" + i,
                    Name = "Name" + i,
                    Gender = (i % 2 == 0 ? 1 : 2),
                    Desc = "Desc" + i
                });
            }
            return Json(new { Data = list, PageCount = 5 });
        }

        public int Edit(Student s)
        {
            return 1;
        }

        public int Delete(string id)
        {
            return 1;
        }
    }
}