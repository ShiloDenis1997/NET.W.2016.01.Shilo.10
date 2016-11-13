using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2.Logic.Tests
{
    [TestFixture]
    public class RectangleTests
    {
        [TestCase(12, 24)]
        [Test]
        public void Ctor_InstanceExpectedWithRadius(double a, double b)
        {
            //act
            Rectangle rectangle = new Rectangle(a, b);
            //assert
            Assert.LessOrEqual(Math.Abs(a - rectangle.A), Shape.Epsilon,
                $"Instance first side = {rectangle.A}, expected {a}");
            Assert.LessOrEqual(Math.Abs(b - rectangle.B), Shape.Epsilon,
                $"Instance second side = {rectangle.B}, expected {b}");
        }

        [TestCase(-2, 3, typeof(ArgumentOutOfRangeException))]
        [TestCase(4, 1e-9, typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Ctor_ExceptionExpected(double a, double b, Type exceptionType)
        {
            //assert
            Assert.Throws(exceptionType, () => new Rectangle(a, b));
        }

        [TestCase(12, 13, (12 + 13) * 2)]
        [TestCase(0.01, 0.01, (0.01 + 0.01) * 2)]
        [Test]
        public void Perimeter_ValueExpected(double a, double b, double expectedPerimeter)
        {
            //arrange
            Rectangle rectangle = new Rectangle(a, b);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedPerimeter - rectangle.Perimeter), Shape.Epsilon,
                $"Instance perimeter = {rectangle.Perimeter}, expected {expectedPerimeter}");
        }

        [TestCase(12, 13, 12 * 13)]
        [TestCase(0.01, 0.01, 0.01 * 0.01)]
        [Test]
        public void Square_ValueExpected(double a, double b, double expectedSquare)
        {
            //arrange
            Rectangle rectangle = new Rectangle(a, b);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedSquare - rectangle.Square), Shape.Epsilon,
                $"Instance perimeter = {rectangle.Square}, expected {expectedSquare}");
        }
    }
}
