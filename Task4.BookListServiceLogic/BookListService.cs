using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Task4.BookLogic;

namespace Task4.BookListServiceLogic
{
    /// <summary>
    /// Provides functionality to work with a list of books
    /// </summary>
    public class BookListService : IEnumerable<Book>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Books are stored here
        /// </summary>
        private SortedSet<Book> bookSet;

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/> class
        /// </summary>
        public BookListService()
        {
            logger.Debug("{0} constructor started", nameof(BookListService));
            bookSet = new SortedSet<Book>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/> class that
        /// uses a specified comparer
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if 
        /// <paramref name="comparer"/> is null</exception>
        public BookListService(IComparer<Book> comparer)
        {
            logger.Debug("{0} constructor started", nameof(BookListService));
            if (comparer == null)
            {
                var ane = new ArgumentNullException($"{nameof(comparer)} is null");
                logger.Warn(ane, "{0} is null", nameof(comparer));
                throw ane;
            }
            bookSet = new SortedSet<Book>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/> class that
        /// uses a specified comparison rule
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if 
        /// <paramref name="comparison"/> is null</exception>
        public BookListService(Comparison<Book> comparison)
            : this(new ComparisonToIComparerAdapter<Book>(comparison))
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/> class that contains 
        /// elements copied from a specified enumerable collection
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="books"/>
        ///  is null</exception>
        public BookListService(IEnumerable<Book> books)
        {
            logger.Debug("{0} constructor started", nameof(BookListService));
            if (books == null)
            {
                var ane = new ArgumentNullException($"{nameof(books)} parameter is null");
                logger.Warn(ane, "{0} parameter is null", nameof(books));
                throw ane;
            }
            bookSet = new SortedSet<Book>(books);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/> class that contains 
        /// elements copied from a specified enumerable collection and that uses a specified
        /// comparer
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="books"/> or
        /// <paramref name="comparer"/> is null</exception>
        public BookListService(IEnumerable<Book> books, IComparer<Book> comparer)
        {
            logger.Debug("{0} constructor started", nameof(BookListService));
            if (comparer == null)
            {
                var ane = new ArgumentNullException($"{nameof(comparer)} is null");
                logger.Warn(ane, "{0} is null", nameof(comparer));
                throw ane;
            }
            if (books == null)
            {
                var ane = new ArgumentNullException($"{nameof(books)} parameter is null");
                logger.Warn(ane, "{0} parameter is null", nameof(books));
                throw ane;
            }
            bookSet = new SortedSet<Book>(books, comparer);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/> class that contains 
        /// elements copied from a specified enumerable collection and that uses a specified
        /// comparison rule
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="books"/> or
        /// <paramref name="comparison"/> is null</exception>
        public BookListService(IEnumerable<Book> books, Comparison<Book> comparison)
            : this(books, new ComparisonToIComparerAdapter<Book>(comparison))
        {
        }

        /// <summary>
        /// Adds a book to the book list
        /// </summary>
        /// <exception cref="BookListException">Throws if <paramref name="book"/> is already in
        /// the book list</exception>
        public void AddBook(Book book)
        {
            logger.Debug("starts adding book");
            bool isWasAdded = bookSet.Add(book);
            if (!isWasAdded)
            {
                var ble = new BookListException
                    ($"{nameof(book)} is already in the book list");
                logger.Warn(ble, "book is already added");
                throw ble;
            }
        }

        /// <summary>
        /// Removes a book from the book list
        /// </summary>
        /// <exception cref="BookListException">Throws if the book is already removed</exception>
        public void RemoveBook(Book book)
        {
            logger.Debug("starts removing book");
            bool isWasRemoved = bookSet.Remove(book);
            if (!isWasRemoved)
            {
                var ble = new BookListException($"{nameof(book)} is already removed" +
                                            "from the book list");
                logger.Warn(ble, "book is already removed");
                throw ble;
            }
        }

        /// <summary>
        /// Finds a book that gives true from <paramref name="predicate"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/>
        /// is null</exception>
        public Book FindBookWithTag(Predicate<Book> predicate)
        {
            logger.Debug("starts finding book by tag");
            if (predicate == null)
            {
                var ane = new ArgumentNullException($"{nameof(predicate)} is null");
                logger.Warn(ane, "{0} parameter is null", nameof(predicate));
                throw ane;
            }
            foreach (Book b in bookSet)
            {
                if (predicate(b))
                {
                    return new Book(b.Name, b.Author, b.PublishedYear, b.Price);
                }
            }
            return null;
        }

        /// <summary>
        /// Sorts books in the book list according to <paramref name="comparer"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparer"/>
        /// is null</exception>
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            logger.Debug("starts sorting books by compare parameter");
            if (comparer == null)
            {
                var ane = new ArgumentNullException($"{nameof(comparer)} is null");
                logger.Warn(ane, "{0} parameter is null", nameof(comparer));
                throw ane;
            }
            bookSet = new SortedSet<Book>(bookSet, comparer);
        }

        /// <summary>
        /// Sorts books in the book list according to <paramref name="comparison"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="comparison"/>
        /// is null</exception>
        public void SortBooksByTag(Comparison<Book> comparison)
        {
            logger.Debug("starts sorting books by comparison parameter");
            if (comparison == null)
            {
                var ane = new ArgumentNullException($"{nameof(comparison)} is null");
                logger.Warn(ane, "{0} parameter if null", nameof(comparison));
                throw ane;
            }
            SortBooksByTag(new ComparisonToIComparerAdapter<Book>(comparison));
        }

        /// <summary>
        /// Stores books to <paramref name="storage"/>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="storage"/>
        ///  is null</exception>
        /// <exception cref="BookListException">Throws if some errors while storing books
        /// if <paramref name="storage"/></exception>
        public void StoreBooks(IBookListStorage storage)
        {
            logger.Debug("starts storing books");
            if (storage == null)
            {
                var ane =  new ArgumentNullException($"{nameof(storage)} is null");
                logger.Warn(ane, "{0} parameter is null", nameof(storage));
                throw ane;
            }
            try
            {
                storage.StoreBooks(this);
            }
            catch (Exception ex)
            {
                var ble = new BookListException($"Error while storing books in {nameof(storage)}", ex);
                logger.Warn(ble, "exception while storing books");
                throw ble;
            }
        }

        /// <summary>
        /// Loads books from <paramref name="storage"/>. All duplicationes will be deleted
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="storage"/>
        /// is null</exception>
        /// <exception cref="BookListException">Throws if some errors while
        /// loading books from <paramref name="storage"/></exception>
        public void LoadBooks(IBookListStorage storage)
        {
            logger.Debug("starts loading books");
            if (storage == null)
            {
                var ane = new ArgumentNullException($"{nameof(storage)} is null");
                logger.Warn(ane, "{0} parameter is null", nameof(storage));
                throw ane;
            }
            IEnumerable<Book> books;
            try
            {
                books = storage.LoadBooks();
            }
            catch (Exception ex)
            {
                var ble = new BookListException($"Cannot load books from {nameof(storage)}", ex);
                logger.Warn(ble, "Exception while loading books");
                throw ble;
            }
            if (books == null)
            {
                var ble = new BookListException($"{nameof(storage.LoadBooks)} returned null");
                logger.Warn("storage returned null");
                throw ble;
            }
            bookSet.Clear();
            foreach (Book b in books)
            {
                try
                {
                    AddBook(b);
                }
                catch (BookListException)
                {
                    logger.Warn("Storage contains duplicate: {0}", b);
                }
            }
        }

        /// <summary>
        /// Returns enumerator of the book list
        /// </summary>
        public IEnumerator<Book> GetEnumerator()
        {
            logger.Debug("getting book list service enumerator");
            return bookSet.GetEnumerator();
        }

        /// <summary>
        /// Returns enumerator of the book list
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adapter class to adapt <see cref="Comparison{T}"/> to <see cref="IComparer{T}"/>
        /// </summary>
        internal class ComparisonToIComparerAdapter<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;

            public ComparisonToIComparerAdapter(Comparison<T> comparison)
            {
                logger.Debug("{0} constructor started",
                    nameof(ComparisonToIComparerAdapter<T>));
                if (comparison == null)
                {
                    var ane = new ArgumentNullException($"{nameof(comparison)} is null");
                    logger.Warn(ane, "{0} parameter is null", nameof(comparison));
                    throw ane;
                }
                this.comparison = comparison;
            }

            public int Compare(T x, T y)
            {
                return comparison(x, y);
            }
        }
    }
}
