using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2.Logic.Tests
{
    [TestFixture]
    public class CircleTests
    {
        [TestCase(12)]
        [Test]
        public void Ctor_InstanceExpectedWithRadius(double radius)
        {
            //act
            Circle circle = new Circle(radius);
            //assert
            Assert.LessOrEqual(Math.Abs(radius - circle.Radius), Shape.Epsilon, 
                $"Instance radius = {circle.Radius}, expected {radius}");
        }

        [TestCase(-2, typeof(ArgumentOutOfRangeException))]
        [TestCase(1e-9, typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Ctor_ExceptionExpected(double radius, Type exceptionType)
        {
            //assert
            Assert.Throws(exceptionType, () => new Circle(radius));
        }

        [TestCase(12, 12 * Math.PI * 2)]
        [TestCase(0.01, 0.01 * Math.PI * 2)]
        [TestCase(100, 100 * Math.PI * 2)]
        [TestCase(0.1, 0.1 * Math.PI * 2)]
        [Test]
        public void Perimeter_ValueExpected(double radius, double expectedPerimeter)
        {
            //arrange
            Circle circle = new Circle(radius);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedPerimeter - circle.Perimeter), Shape.Epsilon,
                $"Instance perimeter = {circle.Perimeter}, expected {expectedPerimeter}");
        }

        [TestCase(12, 12 * 12 * Math.PI / 2)]
        [TestCase(0.01, 0.01 * 0.01 * Math.PI / 2)]
        [TestCase(100, 100 * 100 * Math.PI / 2)]
        [TestCase(0.1, 0.1 * 0.1 * Math.PI / 2)]
        [Test]
        public void Square_ValueExpected(double radius, double expectedSquare)
        {
            //arrange
            Circle circle = new Circle(radius);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedSquare - circle.Square), Shape.Epsilon,
                $"Instance perimeter = {circle.Square}, expected {expectedSquare}");
        }
    }
}
