﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.BookListServiceLogic
{
    /// <summary>
    /// Exception that can be raised while working with <see cref="BookListService"/>
    /// </summary>
    public class BookListException : Exception
    {
        public BookListException() { }
        public BookListException(string message) : base(message) { }
        public BookListException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}