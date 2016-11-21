using System;
using System.Collections.Generic;
using System.Linq;
using Task4.BookLogic;
using Task4.LoggerProviderLogic;

namespace Task4.BookListServiceLogic
{
    /// <summary>
    /// Provides functionality to work with a list of books
    /// </summary>
    public class BookListService
    {
        private readonly static ILogger logger = 
            LoggerProvider.GetLoggerForClassName(nameof(BookListService));
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
                throw new ArgumentNullException($"{nameof(comparer)} is null");
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
                throw new ArgumentNullException($"{nameof(books)} parameter is null");
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
                throw new ArgumentNullException($"{nameof(comparer)} is null");
            }
            if (books == null)
            {
                throw new ArgumentNullException($"{nameof(books)} parameter is null");
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
                throw new BookListException
                    ($"{nameof(book)} is already in the book list");
            }
        }

        /// <summary>
        /// Returns an enumeration of <see cref="Book"/>s in service
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetListOfBooks()
        {
            logger.Debug("returning enumeration of books");
            return bookSet.ToArray();
        } 
        /// <summary>
        /// Removes a book from the book list
        /// </summary>
        /// <exception cref="BookListException">Throws if the book is already removed</exception>
        public void RemoveBook(Book book)
        {
            logger.Debug("starts removing book");
            bool isRemoved = bookSet.Remove(book);
            if (!isRemoved)
            {
                throw new BookListException($"{nameof(book)} is already removed" +
                                            "from the book list");
            }
        }

        /// <summary>
        /// Finds a book that gives true from <paramref name="predicate"/>.
        /// Do not change the instance by returned reference!
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/>
        /// is null</exception>
        public Book FindBookWithTag(Predicate<Book> predicate)
        {
            logger.Debug("starts finding book by tag");
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} is null");
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
                throw new ArgumentNullException($"{nameof(comparer)} is null");
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
                throw new ArgumentNullException($"{nameof(comparison)} is null");
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
                throw new ArgumentNullException($"{nameof(storage)} is null");
            }
            try
            {
                storage.StoreBooks(GetListOfBooks());
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "exception while storing books");
                throw  new BookListException($"Error while storing books in {nameof(storage)}", ex);
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
                throw new ArgumentNullException($"{nameof(storage)} is null");
            }
            IEnumerable<Book> books;
            try
            {
                books = storage.LoadBooks();
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Exception while loading books");
                throw new BookListException($"Cannot load books from {nameof(storage)}", ex);
            }
            if (books == null)
            {
                throw new BookListException($"{nameof(storage.LoadBooks)} returned null");
            }
            bookSet = new SortedSet<Book>(books);
        }

        /// <summary>
        /// Adapter class to adapt <see cref="Comparison{T}"/> to <see cref="IComparer{T}"/>
        /// </summary>
        internal class ComparisonToIComparerAdapter<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;

            public ComparisonToIComparerAdapter(Comparison<T> comparison)
            {
                if (comparison == null)
                {
                    throw new ArgumentNullException($"{nameof(comparison)} is null");
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
