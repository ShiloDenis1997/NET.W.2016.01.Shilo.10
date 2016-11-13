using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    public abstract class Shape
    {
        public const double EpsilonDefaultValue = 1e-6;
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

        public abstract double Perimeter { get; }
        public abstract double Square { get; }
    }
}
