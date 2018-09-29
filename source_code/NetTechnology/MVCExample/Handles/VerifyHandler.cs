using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace MVCExample.Handles
{
    public class VerifyHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参阅以下链接: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //在此处写入您的处理程序实现。
            //string code = "";
            //Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            //context.Session["CheckCode"] = code;
            //bitmap.Save(context.Response.OutputStream, ImageFormat.Gif);
            //context.Response.ContentType = "image/gif";
            context.Response.Write("我只量个没景区");
        }

        #endregion
    }

    public class VerifyCodeHelper
    {
        public static Bitmap CreateVerifyCode(out string code)
        {
            //建立Bitmap对象，绘图
            Bitmap bitmap = new Bitmap(200, 60);
            Graphics graph = Graphics.FromImage(bitmap);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
            Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);
            Random r = new Random();
            string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";

            StringBuilder sb = new StringBuilder();

            //添加随机的五个字母
            for (int x = 0; x < 5; x++)
            {
                string letter = letters.Substring(r.Next(0, letters.Length - 1), 1);
                sb.Append(letter);
                graph.DrawString(letter, font, new SolidBrush(Color.Black), x * 38, r.Next(0, 15));
            }
            code = sb.ToString();

            //混淆背景
            Pen linePen = new Pen(new SolidBrush(Color.Black), 2);
            for (int x = 0; x < 6; x++)
                graph.DrawLine(linePen, new Point(r.Next(0, 199), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
            return bitmap;
        }
    }

}
