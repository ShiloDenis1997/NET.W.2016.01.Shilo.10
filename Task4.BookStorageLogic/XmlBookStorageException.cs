using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task4.BookStorageLogic
{
    /// <summary>
    /// Provides exceptions for <see cref="XmlBookStorage"/>
    /// </summary>
    public class XmlBookStorageException : Exception
    {
        public XmlBookStorageException()
        {
        }

        public XmlBookStorageException(string message) : base(message)
        {
        }

        public XmlBookStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected XmlBookStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
