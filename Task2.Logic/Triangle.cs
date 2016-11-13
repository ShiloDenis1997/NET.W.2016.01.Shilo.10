﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    /// <summary>
    /// Represents behavior of triangle
    /// </summary>
    public class Triangle : Shape
    {
        /// <summary>
        /// Constructs triangle with sides <paramref name="a"/>, 
        /// <paramref name="b"/>, <paramref name="c"/>
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if one of the sides is less or
        /// equal to <see cref="Shape.Epsilon"/></exception>
        /// <exception cref="ArgumentException">Throws if there are no validate triangles with
        /// current sides set</exception>
        public Triangle(double a, double b, double c)
        {
            if (a <= Epsilon)
                throw new ArgumentOutOfRangeException($"{nameof(a)} is less" +
                                                      $" or equal to {nameof(Epsilon)}");
            if (b <= Epsilon)
                throw new ArgumentOutOfRangeException($"{nameof(b)} is less" +
                                                      $" or equal to {nameof(Epsilon)}");
            if (c <= Epsilon)
                throw new ArgumentOutOfRangeException($"{nameof(c)} is less" +
                                                      $" or equal to {nameof(Epsilon)}");
            if (!VerifySides(a, b, c))
                throw new ArgumentException("Can't construct a triangle with values: " +
                                                      $"{nameof(a)} = {a}, {nameof(b)} = {b}, " +
                                                      $"{nameof(c)} = {c}");
            SideA = a;
            SideB = b;
            SideC = c;
        }

        public double SideA { get; }
        public double SideB { get; }
        public double SideC { get; }

        public override double Perimeter => SideA + SideB + SideC;

        public override double Square
        {
            get
            {
                double p = Perimeter/2;
                return Math.Sqrt(p*(p - SideA)*(p - SideB)*(p - SideC));
            }
        }

        /// <summary>
        /// Verify if triangle with sides <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>
        /// can exist
        /// <paramref name="c"/>
        /// </summary>
        private bool VerifySides(double a, double b, double c)
        {
            if (Math.Abs(a + b - c) <= Epsilon)
                return false;
            if (Math.Abs(a + c - b) <= Epsilon)
                return false;
            if (Math.Abs(c + b - a) <= Epsilon)
                return false;
            return true;
        }
    }
}