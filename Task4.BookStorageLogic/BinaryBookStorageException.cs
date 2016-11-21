using System;

namespace Task4.BookStorageLogic
{
    public class BinaryBookStorageException : Exception
    {
        public BinaryBookStorageException() { }
        public BinaryBookStorageException(string message) : base(message) { }
        public BinaryBookStorageException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
