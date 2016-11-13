using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2.Logic.Tests
{
    [TestFixture]
    public class TriangleTests
    {
        [TestCase(12, 24, 15)]
        [Test]
        public void Ctor_InstanceExpectedWithRadius(double a, double b, double c)
        {
            //act
            Triangle triangle = new Triangle(a, b, c);
            //assert
            Assert.LessOrEqual(Math.Abs(a - triangle.A), Shape.Epsilon,
                $"Instance first side = {triangle.A}, expected {a}");
            Assert.LessOrEqual(Math.Abs(b - triangle.B), Shape.Epsilon,
                $"Instance second side = {triangle.B}, expected {b}");
            Assert.LessOrEqual(Math.Abs(c - triangle.C), Shape.Epsilon,
                $"Instance third side = {triangle.C}, expected {c}");
        }

        [TestCase(-2, 3, 4, typeof(ArgumentOutOfRangeException))]
        [TestCase(4, 1e-9, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(4, 2, 1e-9, typeof(ArgumentOutOfRangeException))]
        [TestCase(1, 2, 3, typeof(ArgumentException))]
        [TestCase(1, 3, 2, typeof(ArgumentException))]
        [TestCase(3, 1, 2, typeof(ArgumentException))]
        [TestCase(1, 2, 4, typeof(ArgumentException))]
        [TestCase(1, 4, 2, typeof(ArgumentException))]
        [TestCase(4, 1, 2, typeof(ArgumentException))]
        [Test]
        public void Ctor_ExceptionExpected(double a, double b, double c,Type exceptionType)
        {
            //assert
            Assert.Throws(exceptionType, () => new Triangle(a, b, c));
        }

        [TestCase(12, 13, 14, 12 + 13 + 14)]
        [TestCase(0.1, 0.1, 0.05, 0.1 + 0.1 + 0.05)]
        [Test]
        public void Perimeter_ValueExpected
            (double a, double b, double c, double expectedPerimeter)
        {
            //arrange
            Triangle triangle = new Triangle(a, b, c);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedPerimeter - triangle.Perimeter), Shape.Epsilon,
                $"Instance perimeter = {triangle.Perimeter}, expected {expectedPerimeter}");
        }

        [TestCase(12, 13, 14, 72.30793524)]
        [TestCase(0.1, 0.1, 0.05, 0.0024206145)]
        [Test]
        public void Square_ValueExpected(double a, double b, double c, double expectedSquare)
        {
            //arrange
            Triangle triangle = new Triangle(a, b, c);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedSquare - triangle.Square), Shape.Epsilon,
                $"Instance perimeter = {triangle.Square}, expected {expectedSquare}");
        }
    }
}
