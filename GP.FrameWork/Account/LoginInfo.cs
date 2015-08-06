using GP.FrameWork.Extension;
using System;
using System.Security.Principal;
using System.Web;

namespace GP.FrameWork.Account
{
    public class LoginInfo : ILoginInfo, IIdentity
    {
        private string _name = string.Empty;
        public LoginInfo(string userName)
        {
            _name = userName;
            UserName = userName;
        }
        public long UserID
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["userid"];
                if (cookie != null)
                {
                    return long.Parse(cookie.Value.ToString());
                }
                return 0;
            }
            set
            {
                this["userid"] = value;
            }
        }

        public string UserName
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["username"];
                if (cookie != null)
                {
                    return cookie.Value;
                }
                return string.Empty;
            }
            set { this["username"] = value; }
        }

        public int UserType
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["usertype"];
                if (cookie != null)
                {
                    return int.Parse(cookie.Value.ToString());
                }
                return 0;
            }
            set { this["usertype"] = value; }
        }

        public int UserRole
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["userrole"];
                if (cookie != null)
                {
                    return int.Parse(cookie.Value.ToString());
                }
                return 0;
            }
            set
            {
                this["userrole"] = value;
            }
        }

        public object this[string key]
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
                if (cookie != null)
                {
                    return cookie.Value;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    HttpCookie cookie = new HttpCookie(key, value.ToString());
                    cookie.Expires = DateTime.Now.AddDays(14);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }

        #region IIdentity接口成员

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return _name; }
        }

        #endregion
    }

    public class CustomPrincipal : IPrincipal
    {
        private LoginInfo _identity = null;
        public CustomPrincipal(LoginInfo identity)
        {
            _identity = identity;
        }
        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }

    #region Global.asax需要添加的内容

    //void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
    //{
    //    IPrincipal user = HttpContext.Current.User;
    //    if (user.Identity.IsAuthenticated
    //        && user.Identity.AuthenticationType == "Forms")
    //    {
    //        LoginInfo identity = new LoginInfo();
    //        CustomPrincipal principal = new CustomPrincipal(identity);
    //        HttpContext.Current.User = principal;
    //        Thread.CurrentPrincipal = principal;
    //    }
    //}

    #endregion
}
