using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    public class Triangle : Shape
    {
        private double sideA;
        private double sideB;
        private double sideC;

        public Triangle(double a, double b, double c)
        {
            
        }

        
        public override double Perimeter { get; }
        public override double Square { get; }

        private bool VerifySides(double a, double b, double c)
        {
            return true;
        }
    }
}
