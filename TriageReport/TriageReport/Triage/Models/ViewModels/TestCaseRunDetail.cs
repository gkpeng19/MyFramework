using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Triage.Models.ViewModels
{
    public class TestCaseRunDetail : BaseTriageEntity
    {
        [JsonIgnore]
        public int TestCaseID
        {
            get { return GetInt32("TestCaseID"); }
            set { SetValue("TestCaseID", value); }
        }
        [JsonIgnore]
        public string TestCaseName
        {
            get { return GetString("TestCaseName"); }
            set { SetValue("TestCaseName", value); }
        }
        [JsonIgnore]
        public int StatusID
        {
            get { return GetInt32("StatusID"); }
            set { SetValue("StatusID", value); }
        }
    }
}