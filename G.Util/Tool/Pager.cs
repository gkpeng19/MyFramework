using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Tool
{
    public static class Pager
    {
        public static string InitPager(int pageindex, int pagecount, string function, int pagebutton = 15)
        {
            if (pagecount <= 1)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            if (pageindex > 1)
            {
                sb.AppendFormat("<a href='#' onclick='{0}({1})'>{2}</a>", function, pageindex - 1, "«");
            }

            if (pagecount <= pagebutton)
            {
                for (int i = 1; i <= pagecount; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                }
            }
            else
            {
                int lbutton = 0;
                int rbutton = 0;
                int mbutton = 0;
                if (pagebutton % 3 == 0)
                {
                    mbutton = pagebutton / 3;
                    lbutton = rbutton = mbutton - 1;
                }
                else
                {
                    mbutton = pagebutton / 3 + 1;
                    lbutton = (pagebutton - 2 - mbutton) / 2;
                    rbutton = pagebutton - 2 - mbutton - lbutton;
                }

                bool ignoreleft = false;
                bool ignoreright = false;

                int mloffset = (mbutton - 1) / 2;
                int mlbuttonindex = 0;//中间部分左边第一个按钮数字
                if (pageindex <= lbutton)
                {
                    mlbuttonindex = lbutton + 1;
                    ignoreleft = true;
                }
                else if (pageindex > pagecount - rbutton)
                {
                    mlbuttonindex = pagecount - rbutton - mbutton + 1;
                    ignoreright = true;
                }
                else
                {
                    if (pageindex - lbutton - mloffset <= 1)
                    {
                        mlbuttonindex = lbutton + 1;
                        ignoreleft = true;
                    }
                    else if (pagecount - rbutton - mbutton + 1 + mloffset < pageindex)
                    {
                        mlbuttonindex = pagecount - rbutton - mbutton + 1;
                        ignoreright = true;
                    }
                    else
                    {
                        mlbuttonindex = pageindex - mloffset;
                    }
                }

                for (int i = 1; i <= lbutton; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                }

                if (!ignoreleft)
                {
                    sb.Append("<a class='ignore' href='#'>...</a>");
                }

                for (int i = mlbuttonindex; i < mlbuttonindex + mbutton; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                }

                if (!ignoreright)
                {
                    sb.Append("<a class='ignore' href='#'>...</a>");
                }

                for (int i = pagecount - rbutton + 1; i <= pagecount; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='{0}({1})'>{1}</a>", function, i);
                    }
                }
            }

            if (pageindex < pagecount)
            {
                sb.AppendFormat("<a href='#' onclick='{0}({1})'>{2}</a>", function, pageindex + 1, "»");
            }

            return sb.ToString();
        }

        public static string InitLinkPager(int pageindex, int pagecount, string formId = null, int pagebutton = 15)
        {
            if (pagecount <= 1)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            if (pageindex > 1)
            {
                sb.AppendFormat("<a href='#' onclick='pageAClick({0})'>{1}</a>", pageindex - 1, "«");
            }

            if (pagecount <= pagebutton)
            {
                for (int i = 1; i <= pagecount; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                }
            }
            else
            {
                int lbutton = 0;
                int rbutton = 0;
                int mbutton = 0;
                if (pagebutton % 3 == 0)
                {
                    mbutton = pagebutton / 3;
                    lbutton = rbutton = mbutton - 1;
                }
                else
                {
                    mbutton = pagebutton / 3 + 1;
                    lbutton = (pagebutton - 2 - mbutton) / 2;
                    rbutton = pagebutton - 2 - mbutton - lbutton;
                }

                bool ignoreleft = false;
                bool ignoreright = false;

                int mloffset = (mbutton - 1) / 2;
                int mlbuttonindex = 0;//中间部分左边第一个按钮数字
                if (pageindex <= lbutton)
                {
                    mlbuttonindex = lbutton + 1;
                    ignoreleft = true;
                }
                else if (pageindex > pagecount - rbutton)
                {
                    mlbuttonindex = pagecount - rbutton - mbutton + 1;
                    ignoreright = true;
                }
                else
                {
                    if (pageindex - lbutton - mloffset <= 1)
                    {
                        mlbuttonindex = lbutton + 1;
                        ignoreleft = true;
                    }
                    else if (pagecount - rbutton - mbutton + 1 + mloffset < pageindex)
                    {
                        mlbuttonindex = pagecount - rbutton - mbutton + 1;
                        ignoreright = true;
                    }
                    else
                    {
                        mlbuttonindex = pageindex - mloffset;
                    }
                }

                for (int i = 1; i <= lbutton; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                }

                if (!ignoreleft)
                {
                    sb.Append("<a class='ignore' href='#'>...</a>");
                }

                for (int i = mlbuttonindex; i < mlbuttonindex + mbutton; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                }

                if (!ignoreright)
                {
                    sb.Append("<a class='ignore' href='#'>...</a>");
                }

                for (int i = pagecount - rbutton + 1; i <= pagecount; ++i)
                {
                    if (pageindex == i)
                    {
                        sb.AppendFormat("<a class='current' href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                    else
                    {
                        sb.AppendFormat("<a href='#' onclick='pageAClick({0})'>{0}</a>", i);
                    }
                }
            }

            if (pageindex < pagecount)
            {
                sb.AppendFormat("<a href='#' onclick='pageAClick({0})'>{1}</a>", pageindex + 1, "»");
            }

            if (formId == null)
            {
                sb.Append("<form id=\"pagerform\" style=\"display:none;\"></form>");
                formId = "pagerform";
            }

            sb.Append("<script>$(\"#" + formId + "\").append(\"<input type='hidden' id='pager_index' name='page_g' />\");function pageAClick(pindex){$(\"#pager_index\").val(pindex);$(\"#" + formId + "\")[0].submit();}</script>");
            return sb.ToString();
        }
    }
}
