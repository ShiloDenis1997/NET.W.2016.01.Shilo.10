using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    public abstract class Shape
    {
        public abstract double Perimeter { get; protected set; }
        public abstract double Square { get; protected set; }
    }
}
