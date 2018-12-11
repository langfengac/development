using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutoface.ThreadT
{
    public  class TaskTry
    {
        public static FileStream LifeStatck = new FileStream(System.IO.Directory.GetCurrentDirectory()+"/1.txt",FileMode.Create);
        public static async Task<string> GetEFContext()
        {
           
            var result = await Task.Factory.StartNew(() => { return GetOrderNO(); });

            return "100";
        }
        public static int  GetOrderNO()
        {
            System.Threading.Thread.Sleep(10000);
            LifeStatck.WriteAsync(System.Text.Encoding.UTF8.GetBytes("你好呀"), 0, System.Text.Encoding.UTF8.GetBytes("你好呀").Length);
           
            return 100;
        }
    }
}
