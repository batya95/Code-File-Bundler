using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myfib
{
    internal class MyOption
    {
        public string Name { get; }
        public Action Selected { get; }

        public MyOption(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}