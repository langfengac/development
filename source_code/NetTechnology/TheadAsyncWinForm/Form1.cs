using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheadAsyncWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                tasks.Add(Task.Factory.StartNew<string>(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"这是task{i}的方法，当前线程{Thread.CurrentThread.ManagedThreadId}");
                    return $"这是task{i}的方法，当前线程{Thread.CurrentThread.ManagedThreadId}";
                }));
            }
            //Task.WaitAny(tasks.ToArray());
            Console.WriteLine("你们谁通过了，这么牛逼");
            //Task.WaitAll(tasks.ToArray());
            Console.WriteLine("你们五个打败我了");
        }
    }
}
