using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    /// <summary>
    /// Represents abstract shape logic
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Default value of <see cref="Shape.Epsilon"/>
        /// </summary>
        public const double EpsilonDefaultValue = 1e-6;
        /// <summary>
        /// Epsilon for all evaluations with <see cref="Shape"/>s. Your can set
        /// it in App.config file with key="shapeEpsilon". If not specified, value will
        /// be setted to <see cref="EpsilonDefaultValue"/>
        /// </summary>
        public static double Epsilon { get; }
        
        static Shape()
        {
            try
            {
                string eps = ConfigurationManager.AppSettings["shapeEpsilon"];
                Epsilon = double.Parse(eps);
                if (Epsilon <= 0 || Epsilon >= 1)
                    throw new Exception();
            }
            catch (ConfigurationErrorsException cee)
            {
                Epsilon = EpsilonDefaultValue;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException($"shapeEpsilon in configuration file has " +
                                                       $"invalid value. It must be greater than zero" +
                                                       $" and less than 1");
            }
        }

        /// <summary>
        /// Perimeter of shape
        /// </summary>
        public abstract double Perimeter { get; }
        /// <summary>
        /// Foursquare of shape
        /// </summary>
        public abstract double Square { get; }
    }
}
