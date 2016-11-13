using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    public abstract class Shape
    {
        public const double EpsilonDefaultValue = 1e-6;
        public static double Epsilon { get; }
        
        static Shape()
        {
            try
            {
                string eps = ConfigurationManager.AppSettings["epsilon"];
                Epsilon = double.Parse(eps);
            }
            catch (Exception)
            {
                Epsilon = EpsilonDefaultValue;
            }
        }

        public abstract double Perimeter { get; }
        public abstract double Square { get; }
    }
}
