using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.BookListServiceLogic;
using Task4.BookLogic;
using Task4.LoggerProviderLogic;

namespace Task4.BookStorageLogic
{
    public class XmlBookStorage : IBookListStorage
    {
        private string filepath;
        private ILogger logger;

        public XmlBookStorage(string filepath, ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException($"{nameof(logger)} is null");
            }
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentException($"{nameof(filepath)} is null, whitespace or empty");
            }
            this.filepath = filepath;
            this.logger = logger;
        }

        public IEnumerable<Book> LoadBooks()
        {
            throw new NotImplementedException();
        }

        public void StoreBooks(IEnumerable<Book> books)
        {
            throw new NotImplementedException();
        }
    }
}
