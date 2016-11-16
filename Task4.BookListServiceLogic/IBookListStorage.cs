using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.BookLogic;

namespace Task4.BookListServiceLogic
{
    /// <summary>
    /// Interface of abstract <see cref="Book"/> storage
    /// </summary>
    public interface IBookListStorage
    {
        /// <summary>
        /// Stores <paramref name="books"/> somewhere
        /// </summary>
        void StoreBooks(IEnumerable<Book> books);
        /// <summary>
        /// Loads <see cref="Book"/>s enumerable from somewhere
        /// </summary>
        IEnumerable<Book> LoadBooks();
    }
}
