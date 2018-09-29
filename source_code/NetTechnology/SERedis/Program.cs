using SERedis.SE.BaseDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERedis
{
    class Program
    {
        static void Main(string[] args)
        {
            Array array = new Array[5];
            Console.WriteLine("----------------String-----------");
            SEOpeater.OpeaterString();
            Console.WriteLine("----------------Hash-----------");
            SEOpeater.OpeaterHashes();
            Console.WriteLine("----------------List-----------");
            SEOpeater.OpeaterList();
            Console.WriteLine("----------------事务-----------");
            SEOpeater.TranSfer();
            Console.WriteLine("----------------订阅发布-----------");
            Console.ReadLine();
        }
    }
}
