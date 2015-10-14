﻿using G.Util.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Student s = new Student();
            //s.SetUIValue("ID", 1);
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

            RedisRepository res = new RedisRepository();
            var list= res.GetAll<Student>();

            var s1= new Student();
            s1.SetUIValue("ID",1);
            var target = res.Find<Student>(s1);
        }
    }

    public class Student : RedisEntity
    {
        public Student()
        {
            base.TableName = "Sutdent";
        }
        [JsonIgnore]
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
        [JsonIgnore]
        public int Age
        {
            get { return GetInt32("Age"); }
            set { SetValue("Age", value); }
        }
        [JsonIgnore]
        public DateTime Time
        {
            get { return GetDateTime("Time"); }
            set { SetValue("Time", value); }
        }
        [JsonIgnore]
        public decimal Price
        {
            get { return GetDecimal("Price"); }
            set { SetValue("Price", value); }
        }
        [JsonIgnore]
        public double PriceD
        {
            get { return GetDouble("PriceD"); }
            set { SetValue("PriceD", value); }
        }
    }
}
