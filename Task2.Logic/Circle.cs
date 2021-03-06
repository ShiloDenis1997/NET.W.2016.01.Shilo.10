﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    /// <summary>
    /// Class represents behavior of circle
    /// </summary>
    public class Circle : Shape
    {
        private double radius;

        /// <summary>
        /// Constructes a circle object with setted <paramref name="radius"/>
        /// </summary>
        /// <param name="radius">Circle's radius</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="radius"/>
        /// is less or equal to zero</exception>
        public Circle(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Circle's radius
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if setted value
        ///  is less or equal to <see cref="Shape.Epsilon"/></exception>
        public double Radius {
            get { return radius; }
            set
            {
                if (value <= Epsilon)
                    throw new ArgumentOutOfRangeException($"{nameof(value)} is less" +
                                                          $" or equal to {nameof(Epsilon)}");
                radius = value;
            }
        }

        /// <summary>
        /// Perimeter of circle
        /// </summary>
        public override double Perimeter => 2*Math.PI*radius;
        /// <summary>
        /// Square of circle
        /// </summary>
        public override double Square => Math.PI/2.0*radius*radius;
    }
}
