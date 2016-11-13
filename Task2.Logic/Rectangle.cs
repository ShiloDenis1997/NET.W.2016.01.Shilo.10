using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    /// <summary>
    /// Represents behavior of rectangle
    /// </summary>
    public class Rectangle : Shape
    {
        private double a;
        private double b;

        /// <summary>
        /// Constructs <see cref="Rectangle"/> with sides <paramref name="a"/>, <paramref name="b"/>
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="a"/> 
        /// or <paramref name="b"/> is less or equal to <see cref="Shape.Epsilon"/></exception>
        public Rectangle(double a, double b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Side of rectangle
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if setted value is less or 
        /// equal to <see cref="Shape.Epsilon"/></exception>
        public double A
        {
            get { return a; }
            set
            {
                if (value <= Epsilon)
                    throw new ArgumentOutOfRangeException($"{nameof(value)} is less" +
                                                          $" or equal to {nameof(Epsilon)}");
                a = value;
            }
        }

        /// <summary>
        /// Side of rectangle
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if setted value is less or 
        /// equal to <see cref="Shape.Epsilon"/></exception>
        public double B
        {
            get { return b; }
            set
            {
                if (value <= Epsilon)
                    throw new ArgumentOutOfRangeException($"{nameof(value)} is less" +
                                                          $" or equal to {nameof(Epsilon)}");
                b = value;
            }
        }

        /// <summary>
        /// Perimeter of rectangle
        /// </summary>
        public override double Perimeter => 2 * (B + A);

        /// <summary>
        /// Square of rectangle
        /// </summary>
        public override double Square => B * A;
    }
}
