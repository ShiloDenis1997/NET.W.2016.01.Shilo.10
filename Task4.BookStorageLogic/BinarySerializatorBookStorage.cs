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
using Task4.LoggerProviderLogic;

namespace Task4.BookStorageLogic
{
    public class BinarySerializatorBookStorage : IBookListStorage
    {
        private string filepath;
        private ILogger logger;

        public BinarySerializatorBookStorage(string filepath, ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException($"{nameof(logger)} is null");
            }
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentException
                    ($"{nameof(filepath)} is null, whitespace or empty");
            }
            this.filepath = filepath;
            this.logger = logger;
        }

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
                using (FileStream fileStream = new FileStream(filepath,
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
                        (filepath, FileMode.Create, FileAccess.Write, FileShare.None))
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
