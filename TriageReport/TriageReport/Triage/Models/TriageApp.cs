using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Triage.Models
{
    public class TriageApp : BaseTriageEntity
    {
        [JsonIgnore]
        public int AppID
        {
            get { return GetInt32("AppID"); }
            set { SetValue("AppID", value); }
        }
        [JsonIgnore]
        public int ApplicationID
        {
            get { return GetInt32("ApplicationID"); }
            set { SetValue("ApplicationID", value); }
        }
        [JsonIgnore]
        public string AppName
        {
            get { return GetString("AppName"); }
            set { SetValue("AppName", value); }
        }
        [JsonIgnore]
        public string Language
        {
            get { return GetString("Language"); }
            set { SetValue("Language", value); }
        }
        [JsonIgnore]
        public string OSName
        {
            get { return GetString("OSName"); }
            set { SetValue("OSName", value); }
        }
        [JsonIgnore]
        public string Build
        {
            get { return GetString("Build"); }
            set { SetValue("Build", value); }
        }
        [JsonIgnore]
        public string Platform
        {
            get { return GetString("Platform"); }
            set { SetValue("Platform", value); }
        }
        [JsonIgnore]
        public int OSID
        {
            get { return GetInt32("OSID"); }
            set { SetValue("OSID", value); }
        }
        [JsonIgnore]
        public int StatusID
        {
            get { return GetInt32("StatusID"); }
            set { SetValue("StatusID", value); }
        }
        [JsonIgnore]
        public string LogPath
        {
            get { return GetString("LogPath"); }
            set { SetValue("LogPath", value); }
        }
        [JsonIgnore]
        public string ScriptStatus
        {
            get { return GetString("ScriptStatus"); }
            set { SetValue("ScriptStatus", value); }
        }
    }
}