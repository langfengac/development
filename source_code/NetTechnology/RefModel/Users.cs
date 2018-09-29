using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefModel
{
    public class Users
    {
        public int ID { get; set; }
        public string Name { get => name; set => name = value; }

        private string name;



    }
}
