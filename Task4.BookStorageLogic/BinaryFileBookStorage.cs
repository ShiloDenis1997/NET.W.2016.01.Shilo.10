using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Task4.BookListServiceLogic;
using Task4.BookLogic;

namespace Task4.BookStorageLogic
{
    /// <summary>
    /// Class to store <see cref="Book"/> enumerables to binary file
    /// </summary>
    public class BinaryFileBookStorage : IBookListStorage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Path to the storage binary file
        /// </summary>
        private readonly string filepath;

        /// <summary>
        /// Initialized storage with specified <paramref name="filepath"/>
        /// </summary>
        /// <exception cref="ArgumentException">Throws if <paramref name="filepath"/>
        /// is null, whitespace or empty</exception>
        public BinaryFileBookStorage(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentException($"{nameof(filepath)} is null, whitespace or empty");
            }
            this.filepath = filepath;
        }

        /// <summary>
        /// Stores <paramref name="books"/> in binary file
        /// </summary>
        /// <exception cref="BinaryBookStorageException">Throws if 
        /// some there is some errors while writing to file</exception>
        public void StoreBooks(IEnumerable<Book> books)
        {
            try
            {
                using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
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
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Exception while writing to storage");
                throw new BinaryBookStorageException("Exception while writing to storage", ex);
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
                using (FileStream fs = new FileStream(filepath, FileMode.Open))
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
