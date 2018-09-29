using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TheadAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            ManualResetEvent mre = new ManualResetEvent(true);
            string a = "dvscd";

            byte[] by= System.Text.Encoding.UTF8.GetBytes(a);
            Array.Sort(by);
            string b = System.Text.Encoding.UTF8.GetString(by);

            mre.Reset();
            IAsyncResult result= new Action<string>((t) =>
            {
                Thread.Sleep(2000);
                Console.WriteLine(t);
                mre.Set();
            }).BeginInvoke("你好",null,null);
           
            mre.WaitOne();
            while (!result.IsCompleted)
            {
                Thread.Sleep(500);
                Console.WriteLine("我在等你回复！");
                
            }

            Console.WriteLine("你吃饭了么！");

            Thread thread = new Thread(() =>
              {
                  Console.WriteLine("我只是线程");

              });
            
           

            //mre.Reset();

            ThreadPool.QueueUserWorkItem((t) =>
            {
                Console.WriteLine("我是从线程池中走出的线程！");
                mre.Set();
            });
            mre.WaitOne();

            int s = 0;
            List<string> list = new List<string>();
            for (int i = 0; i <12; i++)
            {
                for (int f = 0; f < 60; f++)
                {
                    s = i*5+f / 12;
                    if (s == f)
                    {
                        list.Add(s/5 + ":" + f);
                        Console.WriteLine(s / 5 + ":" + f);
                    }
                }
            }

            ManualResetEvent md = new ManualResetEvent(false);
            Console.ReadLine();
        }
         
       
        [Obsolete("我们都不用了")]
        public static string Tsc()
        {
            return "2323";
        }
        
    }
    public class DeleteStatus : Attribute
    {

    }
}
