using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Triage.Models.ViewModels
{
    public class Language : BaseTriageEntity
    {
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
    }
    public class OSName : BaseTriageEntity
    {
        public string Name
        {
            get { return GetString("Name"); }
            set { SetValue("Name", value); }
        }
    }

    public class LanguageAndOSName
    {
        public List<Language> LanguageList { get; set; }
        public List<OSName> OSNameList { get; set; }
    }
}