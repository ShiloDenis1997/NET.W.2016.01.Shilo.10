using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.BookLogic;

namespace Task4.BookListServiceLogic
{
    public interface IBookListStorage
    {
        void StoreBooks(IEnumerable<Book> books);
        IEnumerable<Book> LoadBooks();
    }
}
