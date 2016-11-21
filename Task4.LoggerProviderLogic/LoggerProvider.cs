using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Task4.LoggerProviderLogic
{
    /// <summary>
    /// Provides a factory to create an instance of logger 
    /// adapted to <see cref="ILogger"/> interface
    /// </summary>
    public static class LoggerProvider
    {
        /// <summary>
        /// Factory method. Returns instance of logger for
        /// specified classname
        /// </summary>
        /// <param name="classname">Specified classname</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws if 
        /// <paramref name="classname"/> is null</exception>
        public static ILogger GetLoggerForClassName(string classname)
        {
            if (classname == null)
                throw new ArgumentNullException
                    ($"{nameof(classname)} parameter is null");
            Logger logger = LogManager.GetLogger(classname);
            return new LoggerToILoggerAdapter(logger);
        }
    }
}
