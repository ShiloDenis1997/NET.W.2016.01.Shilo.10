using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.BookLogic;

namespace Task4.BookListServiceLogic
{
    public class BookListService : IEnumerable<Book>
    {
        private SortedSet<Book> bookSet;

        public BookListService()
        {
            bookSet = new SortedSet<Book>();
        }

        public BookListService(IComparer<Book> comparer)
        {
            if (comparer == null)
                throw new BookListException($"{nameof(comparer)} is null");
            bookSet = new SortedSet<Book>(comparer);
        }

        public BookListService(IEnumerable<Book> books)
        {
            if (books == null)
                throw new BookListException($"{nameof(books)} parameter is null");
            bookSet = new SortedSet<Book>(books);
        }

        public BookListService(IEnumerable<Book> books, IComparer<Book> comparer)
        {
            if (comparer == null)
                throw new BookListException($"{nameof(comparer)} is null");
            if (books == null)
                throw new BookListException($"{nameof(books)} parameter is null");
            bookSet = new SortedSet<Book>(books, comparer);
        }

        public void AddBook(Book book)
        {
            bool isWasAdded = bookSet.Add(book);
            if (!isWasAdded)
                throw new BookListException
                    ($"{nameof(book)} is already in the book list");
        }

        public void RemoveBook(Book book)
        {
            bool isWasRemoved = bookSet.Remove(book);
            if(!isWasRemoved)
                throw new BookListException($"{nameof(book)} is already removed" +
                                            "from the book list");
        }

        public Book FindBookWithTag(Predicate<Book> predicate)
        {
            foreach (Book b in bookSet)
            {
                if (predicate(b))
                {
                    return new Book(b.Name, b.Author, b.PublishedYear, b.Price);
                }
            }
            return null;
        }

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            bookSet = new SortedSet<Book>(bookSet, comparer);
        }

        public void SortBooksByTag(Comparison<Book> comparison)
        {
            SortBooksByTag(new ComparisonToIComparerAdapter<Book>(comparison));
        }

        public void StoreBooks(IBookListStorage storage)
        {
            if (storage == null)
                throw new BookListException($"{nameof(storage)} is null");
            storage.StoreBooks(this);
        }

        public void LoadBooks(IBookListStorage storage)
        {
            if (storage == null)
                throw new BookListException($"{nameof(storage)} is null");
            bookSet = new SortedSet<Book>(storage.LoadBooks(), bookSet.Comparer);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return bookSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal class ComparisonToIComparerAdapter<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;

            public ComparisonToIComparerAdapter(Comparison<T> comparison)
            {
                this.comparison = comparison;
            }

            public int Compare(T x, T y)
            {
                return comparison(x, y);
            }
        }
    }
}
