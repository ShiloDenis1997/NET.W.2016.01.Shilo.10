using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task2.Logic.Tests
{
    [TestFixture]
    public class FoursquareTests
    {
        [TestCase(12)]
        [Test]
        public void Ctor_InstanceExpectedWithRadius(double a)
        {
            //act
            Foursquare foursquare = new Foursquare(a);
            //assert
            Assert.LessOrEqual(Math.Abs(a - foursquare.A), Shape.Epsilon,
                $"Instance first side = {foursquare.A}, expected {a}");
        }

        [TestCase(-2, typeof(ArgumentOutOfRangeException))]
        [TestCase(1e-9, typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Ctor_ExceptionExpected(double a, Type exceptionType)
        {
            //assert
            Assert.Throws(exceptionType, () => new Foursquare(a));
        }

        [TestCase(12, 12 * 4)]
        [TestCase(0.01, 0.01 * 4)]
        [Test]
        public void Perimeter_ValueExpected(double a, double expectedPerimeter)
        {
            //arrange
            Foursquare foursquare = new Foursquare(a);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedPerimeter - foursquare.Perimeter), Shape.Epsilon,
                $"Instance perimeter = {foursquare.Perimeter}, expected {expectedPerimeter}");
        }

        [TestCase(12, 12 * 12)]
        [TestCase(0.01, 0.01 * 0.01)]
        [Test]
        public void Square_ValueExpected(double a, double expectedSquare)
        {
            //arrange
            Foursquare foursquare = new Foursquare(a);
            //assert
            Assert.LessOrEqual(Math.Abs(expectedSquare - foursquare.Square), Shape.Epsilon,
                $"Instance perimeter = {foursquare.Square}, expected {expectedSquare}");
        }
    }
}
