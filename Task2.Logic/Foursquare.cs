using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    /// <summary>
    /// Represents behavior of foursquare
    /// </summary>
    public class Foursquare : Shape
    {
        private double a;

        /// <summary>
        /// Constructs <see cref="Foursquare"/> with side <paramref name="a"/>
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if value is less or 
        /// equal to <see cref="Shape.Epsilon"/></exception>
        public Foursquare(double a)
        {
            A = a;
        }

        /// <summary>
        /// Side of foursquare
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if value is less or 
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
        /// Perimeter of foursquare
        /// </summary>
        public override double Perimeter => A * 4;
        /// <summary>
        /// Square of foursquare
        /// </summary>
        public override double Square => A * A;
    }
}
