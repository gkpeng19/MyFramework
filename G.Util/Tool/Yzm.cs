﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Tool
{
    public static class Yzm
    {
        static string[] oFontNames = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
        static Color[] oColors = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
        static char[] oCharacter = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="code">输出的随机码</param>
        /// <param name="codeLength">验证码的长度</param>
        /// <param name="width">验证码图片的宽度</param>
        /// <param name="height">验证码图片的高度</param>
        /// <param name="fontSize">字号</param>
        /// <returns>验证码图片的字节数组,return File(bytes, @"image/Jpeg")</returns>
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
                for (N1 = 0; N1 <= 4; N1++)
                {
                    //畫噪線
                    oPoint1.X = oRnd.Next(width);
                    oPoint1.Y = oRnd.Next(height);
                    oPoint2.X = oRnd.Next(width);
                    oPoint2.Y = oRnd.Next(height);
                    oColor = oColors[oRnd.Next(oColors.Length)];
                    oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
                }

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
