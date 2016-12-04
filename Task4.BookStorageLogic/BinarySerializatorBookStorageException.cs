using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task4.BookStorageLogic
{
    /// <summary>
    /// Provides exceptions for <see cref="BinarySerializatorBookStorage"/>
    /// </summary>
    public class BinarySerializatorBookStorageException : Exception
    {
        public BinarySerializatorBookStorageException()
        {
        }

        public BinarySerializatorBookStorageException(string message) : base(message)
        {
        }

        public BinarySerializatorBookStorageException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected BinarySerializatorBookStorageException
            (SerializationInfo info, StreamingContext context) 
                : base(info, context)
        {
        }
    }
}
