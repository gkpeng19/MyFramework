using EntityLibrary.Entities;
using G.BaseWeb.Controllers;
using G.Util.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G.Util.Mvc;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace WebSite.Controllers
{
    public class ClientBaseController : BaseController
    {
        public override void LoginOut()
        {
            LoginInfo.LoginOut();
            base.HttpContext.Response.Redirect("~/AdminPages/Login.html");
        }

        public string GetLoginUserName()
        {
            return LoginInfo.Current.UserName;
        }

        public JsonResult GetContactUs()
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.ContactUs;
            var entity = sm.LoadEntity<HQ_Article>();
            if (entity != null)
            {
                return this.JsonNet(entity);
            }
            return null;
        }

        [ValidateInput(false)]
        public long SaveContactUs(HQ_Article entity)
        {
            if (entity.ID > 0)
            {
                entity.EditBy = LoginInfo.Current.UserName;
                entity.EditOn = DateTime.Now;
            }
            else
            {
                entity.ACategory = (int)EnumArticleCategory.ContactUs;
                entity.CreateBy = LoginInfo.Current.UserName;
                entity.CreateOn = DateTime.Now;
            }

            return entity.Save();
        }

        public ActionResult ContactIndex()
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.ContactUs;
            var entity = sm.LoadEntity<HQ_Article>();
            if (entity == null)
            {
                return View(new HQ_Article() { AContent = "维护中。。。" });
            }
            return View(entity);
        }

        public JsonResult GetSilderImg()
        {
            SearchModel sm = new SearchModel("HQ_Article");
            sm["ACategory"] = (int)EnumArticleCategory.SilderImg;
            var entity = sm.LoadEntity<HQ_Article>();
            if (entity != null)
            {
                return this.JsonNet(entity);
            }
            return null;
        }

        public long SaveSilderImg(HQ_Article entity)
        {
            if (entity.ID > 0)
            {
                entity.EditBy = LoginInfo.Current.UserName;
                entity.EditOn = DateTime.Now;
            }
            else
            {
                entity.ACategory = (int)EnumArticleCategory.SilderImg;
                entity.CreateBy = LoginInfo.Current.UserName;
                entity.CreateOn = DateTime.Now;
            }

            return entity.Save();
        }

        public override ActionResult GetYzmImg()
        {
            string code = null;
            var bytes = CreateYZM(out code, 4);
            base.Session["yzm"] = code;
            return File(bytes, @"image/Jpeg");
        }

        static string[] oFontNames = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
        static Color[] oColors = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
        static char[] oCharacter = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static byte[] CreateYZM(out String code, int codeLength, int width = 90, int height = 30, int fontSize = 16)
        {
            String sCode = String.Empty;
            Random oRnd = new Random();
            Bitmap oBmp = null;
            Graphics oGraphics = null;
            int N1 = 0;
            System.Drawing.Point oPoint1 = default(System.Drawing.Point);
            System.Drawing.Point oPoint2 = default(System.Drawing.Point);
            string sFontName = null;
            Font oFont = null;
            Color oColor = default(Color);

            for (N1 = 0; N1 <= codeLength - 1; N1++)
            {
                sCode += oCharacter[oRnd.Next(oCharacter.Length)];
            }

            oBmp = new Bitmap(width, height);
            oGraphics = Graphics.FromImage(oBmp);
            oGraphics.Clear(System.Drawing.Color.White);
            try
            {
                //for (N1 = 0; N1 <= 4; N1++)
                //{
                //    //畫噪線
                //    oPoint1.X = oRnd.Next(width);
                //    oPoint1.Y = oRnd.Next(height);
                //    oPoint2.X = oRnd.Next(width);
                //    oPoint2.Y = oRnd.Next(height);
                //    oColor = oColors[oRnd.Next(oColors.Length)];
                //    oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
                //}

                float spaceWith = 0, dotX = 0, dotY = 0;
                if (codeLength != 0)
                {
                    spaceWith = (width - fontSize * codeLength - 10) / codeLength;
                }

                for (N1 = 0; N1 <= sCode.Length - 1; N1++)
                {
                    //畫驗證碼字串
                    sFontName = oFontNames[oRnd.Next(oFontNames.Length)];
                    oFont = new Font(sFontName, fontSize, FontStyle.Italic);
                    oColor = oColors[oRnd.Next(oColors.Length)];

                    dotY = (height - oFont.Height) / 2 + 2;//中心下移2像素
                    dotX = Convert.ToSingle(N1) * fontSize + (N1 + 1) * spaceWith;

                    oGraphics.DrawString(sCode[N1].ToString(), oFont, new SolidBrush(oColor), dotX, dotY);
                }

                for (int i = 0; i <= 30; i++)
                {
                    //畫噪點
                    int x = oRnd.Next(oBmp.Width);
                    int y = oRnd.Next(oBmp.Height);
                    Color clr = oColors[oRnd.Next(oColors.Length)];
                    oBmp.SetPixel(x, y, clr);
                }

                code = sCode;
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                oBmp.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                oGraphics.Dispose();
            }
        }


    }
}