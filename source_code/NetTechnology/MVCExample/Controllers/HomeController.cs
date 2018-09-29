using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //多线程异常是否能捕获
            List<Task> tasks = new List<Task>();
            for (int i = 0; i <20; i++)
            {
                var k = i;
                tasks.Add(
                    Task.Run(() =>
                    {
                        try
                        {
                            if (k == 10)
                            {
                                throw new Exception("数据异常啊");
                            }
                            else if (k==11)
                            {
                                throw new KeyNotFoundException("数据异常啊");
                            }
                            else
                            {
                                Console.WriteLine($"这个线程正常，当前线程为{Thread.CurrentThread.ManagedThreadId}");
                            }
                        }
                        catch (Exception e)
                        {

                            throw new Exception(e.ToString()) ;
                        }
                       
                    })
                );
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException e)
            {

                throw e;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}