using System;
using System.Collections.Generic;
using System.IO;
using Task4.BookListServiceLogic;
using Task4.BookLogic;
using Task4.LoggerInterfaces;
using Task4.LoggerProviderLogic;

namespace Task4.BookStorageLogic
{
    /// <summary>
    /// Class to store <see cref="Book"/> enumerables to binary file
    /// </summary>
    public class BinaryFileBookStorage : IBookListStorage
    {
        /// <summary>
        /// Binary file name
        /// </summary>
        private string filename;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes storage with specified <paramref name="filename"/>
        /// </summary>
        /// <exception cref="ArgumentException">Throws if <paramref name="filename"/>
        /// is null, whitespace or empty</exception>
        public BinaryFileBookStorage(string filename, ILogger logger)
        {
            this.logger = logger ?? LoggerProvider.GetLoggerForClassName
                              (nameof(BinaryFileBookStorage));
            Filename = filename;
        }

        /// <summary>
        /// Path to storage file
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Throws if setted value is null, whitespace or empty</exception>
        public string Filename
        {
            get { return filename; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException
                        ($"{nameof(filename)} is null, whitespace or empty");
                }
                filename = value;
            }
        }

        /// <summary>
        /// Stores <paramref name="books"/> in binary file
        /// </summary>
        /// <exception cref="BinaryBookStorageException">Throws if 
        /// there is some errors while writing to file</exception>
        public void StoreBooks(IEnumerable<Book> books)
        {
            try
            {
                using (FileStream fs = new FileStream
                    (filename, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        foreach (Book book in books)
                        {
                            bw.Write(book.Name);
                            bw.Write(book.Author);
                            bw.Write(book.PublishedYear);
                            bw.Write(book.Price);
                        }
                        bw.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Exception while writing to storage");
                throw new BinaryBookStorageException
                    ("Exception while writing to storage", ex);
            }
        }

        /// <summary>
        /// Returns enumerable of <see cref="Book"/>s
        /// </summary>
        /// <exception cref="BinaryBookStorageException">Throws if some errors while 
        /// reading file</exception>
        public IEnumerable<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        while (br.BaseStream.Position != br.BaseStream.Length) 
                        {
                            string name = br.ReadString();
                            string author = br.ReadString();
                            int publishedYear = br.ReadInt32();
                            decimal price = br.ReadDecimal();
                            books.Add(new Book(name, author, publishedYear, price));
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Exception while reading from storage");
                throw new BinaryBookStorageException("Error while reading from storage", ex);
            }
            return books;
        }
    }
}
