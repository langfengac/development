using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int i = 1;
                Console.WriteLine(i++);
                Console.WriteLine(++i);
                Console.WriteLine(i=20);
                Console.WriteLine(i==3?1:2);
                Assembly assembly = Assembly.Load("RefModel");
                foreach (var item in assembly.GetModules())
                {
                    Console.WriteLine(item.Name);

                }
                Assembly assembly01 = Assembly.LoadFile(@"E:\专题研究\git\development\source_code\NetTechnology\ReflectionDemo\bin\Debug\RefModel.dll");
                foreach (var item in assembly01.GetModules())
                {
                    Console.WriteLine(item.Name);

                }
                Assembly assembly02 = Assembly.LoadFrom("RefModel.dll");
                foreach (var item in assembly02.GetModules())
                {
                    Console.WriteLine(item.Name);

                }
                Console.ReadLine();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
            

        }

        public int Do(int ss)
        {
            return ss;

        }
        public int Do(ref int ss)
        {
            return ss;

        }
    }
}
