using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Task4.BookListServiceLogic;
using Task4.BookLogic;
using Task4.LoggerProviderLogic;

namespace Task4.BookStorageLogic
{
    public class XmlBookStorage : IBookListStorage
    {
        /// <summary>
        /// XML file name
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
        public XmlBookStorage(string filename, ILogger logger)
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
        /// Loads books from xml-file
        /// </summary>
        /// <exception cref="XmlBookStorageException">Throws if
        /// cannot parse data from xml-file</exception>
        public IEnumerable<Book> LoadBooks()
        {
            XDocument storage = XDocument.Load(filename);
            IEnumerable<Book> books;
            try
            {
                books = storage.Descendants("Book")
                    .Select(el =>
                        new Book(el.Attribute("Name").Value,
                            el.Attribute("Author").Value,
                            int.Parse(el.Attribute("PublishedYear").Value),
                            decimal.Parse(el.Attribute("Price").Value)));
            }
            catch (Exception ex)
            {
                throw new XmlBookStorageException
                    ("Invalid data in XML document", ex);
            }
            return books.ToArray();
        }

        /// <summary>
        /// Stores books in xml-file
        /// </summary>
        /// <param name="books"></param>
        public void StoreBooks(IEnumerable<Book> books)
        {
            XDocument storage = new XDocument(
                new XElement("Books",
                    books.Where(book => !ReferenceEquals(null, book))
                        .Select(book => new XElement("Book", 
                        new XAttribute("Name", book.Name),
                        new XAttribute("Author", book.Author),
                        new XAttribute("Price", book.Price),
                        new XAttribute("PublishedYear", book.PublishedYear)))));
            storage.Save(filename);
        }
    }
}
