using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace G.Util.Account
{
    public class LoginInfo : ILoginInfo
    {
        private string _userName = string.Empty;

        public LoginInfo() { }
        public LoginInfo(string userName)
        {
            _userName = userName;
            UserName = userName;
            SystemID = string.Empty;
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
            private set { this["username"] = value; }
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

        public string SystemID
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["systemid"];
                if (cookie != null)
                {
                    return cookie.Value;
                }
                return string.Empty;
            }
            set { this["systemid"] = value; }
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

        public string Name
        {
            get { return _userName.Length == 0 ? UserName : _userName; }
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        #endregion

        public static void SetLoginToken(LoginInfo user, bool isRemember = false)
        {
            string tokenValue = "这是一些附加信息,你可以写入角色什么的";
            double expiredTime = 12 * 60d;
            if (isRemember)
            {
                expiredTime = 7 * 12 * 60d;
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                user.Name,
                DateTime.Now,
                DateTime.Now.AddMinutes(expiredTime),
                isRemember,
                tokenValue,
                FormsAuthentication.FormsCookiePath);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Path = ticket.CookiePath,
                Expires = ticket.IsPersistent ? ticket.Expiration : DateTime.MinValue,
                Domain = FormsAuthentication.CookieDomain
            };
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        public static void LoginOut()
        {
            HttpContext.Current.Response.Cookies.Clear();
            FormsAuthentication.SignOut();
        }

        public static LoginInfo Current
        {
            get
            {
                var identity = HttpContext.Current.User.Identity;
                if (identity != null)
                {
                    return identity as LoginInfo;
                }
                return null;
            }
        }
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
