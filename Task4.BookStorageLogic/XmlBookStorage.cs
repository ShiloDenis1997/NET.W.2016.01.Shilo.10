using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.BookListServiceLogic;
using Task4.BookLogic;

namespace Task4.BookStorageLogic
{
    public class XmlBookStorage : IBookListStorage
    {
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
