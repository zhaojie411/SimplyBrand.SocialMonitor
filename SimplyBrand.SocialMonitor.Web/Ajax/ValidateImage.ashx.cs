using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace SimplyBrand.SocialMonitor.Web.Ajax
{
    /// <summary>
    /// ValidateImage 的摘要说明
    /// </summary>
    public class ValidateImage : BaseAjax
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/gif";
            Bitmap b = new Bitmap(200, 60);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(new SolidBrush(Color.YellowGreen), 0, 0, 200, 60);
            Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);
            Random r = new Random();
            string strLetters = "abcdefghjkmnopqrstuvwxyzABCDEFGHIJKLMNPRSTUVWXYZ23456789";
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < vCodeLength; i++)
            {
                s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
                g.DrawString(s[s.Length - 1].ToString(), font, new SolidBrush(Color.Blue), i * 38, r.Next(0, 15));
            }
            Pen pen = new Pen(new SolidBrush(Color.Blue), 2);
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(pen, new Point(r.Next(0, 199), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
            }
            b.Save(context.Response.OutputStream, ImageFormat.Gif);
            context.Session[Identify] = s.ToString();
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}