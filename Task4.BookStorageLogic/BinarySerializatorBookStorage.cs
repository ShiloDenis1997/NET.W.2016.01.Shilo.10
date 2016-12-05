using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Task4.BookListServiceLogic;
using Task4.BookLogic;
using Task4.LoggerInterfaces;

namespace Task4.BookStorageLogic
{
    public class BinarySerializatorBookStorage : IBookListStorage
    {
        /// <summary>
        /// Binary file name
        /// </summary>
        private string filename;
        private ILogger logger;

        /// <summary>
        /// Initializes storage with specified <paramref name="filename"/>
        /// </summary>
        /// <exception cref="ArgumentException">Throws if <paramref name="filename"/>
        /// is null, whitespace or empty</exception>
        /// <exception cref="ArgumentNullException">Throws 
        /// if <paramref name="logger"/> is null</exception>
        public BinarySerializatorBookStorage(string filename, ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException($"{nameof(logger)} is null");
            }

            Filename = filename;
            this.logger = logger;
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
        /// Loads books from binary file. 
        /// </summary>
        /// <exception cref="BinarySerializatorBookStorageException"> Throws
        /// if there are some errors while working with storage file</exception>
        public IEnumerable<Book> LoadBooks()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SurrogateSelector selector = new SurrogateSelector();
            selector.AddSurrogate
                (typeof(Book), new StreamingContext(StreamingContextStates.All),
                new BookSurrogate());
            formatter.SurrogateSelector = selector;
            LinkedList<Book> books = new LinkedList<Book>();
            try
            {
                using (FileStream fileStream = new FileStream(filename,
                        FileMode.Open))
                {
                    while (fileStream.Position != fileStream.Length)
                        books.AddLast((Book)formatter.Deserialize(fileStream));
                }
            }
            catch (Exception ex)
            {
                throw new BinarySerializatorBookStorageException
                    ("Exception while loading books", ex);
            }
            return books.ToArray();
        }
        
        /// <summary>
        /// Stores <paramref name="books"/> to storage file
        /// </summary>
        /// <exception cref="BinarySerializatorBookStorageException">Throws if
        /// there are some errors on serializing</exception>
        public void StoreBooks(IEnumerable<Book> books)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SurrogateSelector selector = new SurrogateSelector();
            selector.AddSurrogate
                (typeof(Book), new StreamingContext(StreamingContextStates.All),
                    new BookSurrogate());
            formatter.SurrogateSelector = selector;
            try
            {
                using (FileStream fileStream = new FileStream
                        (filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    foreach (var book in books)
                    {
                        if (book != null)
                            formatter.Serialize(fileStream, book);
                    }
                    fileStream.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new BinarySerializatorBookStorageException
                    ("Exception while storing books", ex);
            }
        }

        /// <summary>
        /// Specifies how to serialize and deserialize <see cref="Book"/> objects
        /// </summary>
        private class BookSurrogate : ISerializationSurrogate
        {
            /// <summary>
            /// Puts info about book into <paramref name="info"/>
            /// </summary>
            /// <param name="obj"><see cref="Book"/> object to serialize</param>
            /// <param name="info"></param>
            /// <param name="context"></param>
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                Book book = (Book) obj;
                info.AddValue("Name", book.Name);
                info.AddValue("Author", book.Author);
                info.AddValue("Price", book.Price);
                info.AddValue("PublishedYear", book.PublishedYear);
            }

            /// <summary>
            /// Creates new <see cref="Book"/> according to 
            /// <paramref name="info"/>
            /// </summary>
            /// <param name="obj">Do not use this parameter</param>
            /// <param name="info"></param>
            /// <param name="context"></param>
            /// <param name="selector"></param>
            /// <returns></returns>
            public object SetObjectData
                (object obj, SerializationInfo info, StreamingContext context, 
                    ISurrogateSelector selector)
            {
                string author = info.GetString("Author");
                string name = info.GetString("Name");
                int publishedYear = info.GetInt32("PublishedYear");
                decimal price = info.GetDecimal("Price");
                return new Book(name, author, publishedYear, price);
            }
        }
    }
}
