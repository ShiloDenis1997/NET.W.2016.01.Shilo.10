using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.BookLogic
{
    /// <summary>
    /// Represents behavior of a book
    /// </summary>
    public class Book : IEquatable<Book>, IComparable, IComparable<Book>
    {
        private string name;
        private string author;
        private int publishedYear;
        private decimal price;

        /// <summary>
        /// Books name
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Throws if value is null, empty or whitespace</exception>
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException
                        ($"value for {nameof(Name)} is null, empty or whitespace");
                name = value;
            }
        }

        /// <summary>
        /// Author of the book
        /// </summary>
        /// <exception cref="ArgumentException">Throws if value is
        /// null, empty or whitespace</exception>
        public string Author
        {
            get { return author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException
                        ($"value for {nameof(Author)} is null, empty or whitespace");
                author = value;
            }
        }

        /// <summary>
        /// Year, when book was published
        /// </summary>
        /// <exception cref="ArgumentException">Throws if value is 
        /// less or equal to zero</exception>
        public int PublishedYear
        {
            get { return publishedYear; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException
                        ($"value for {nameof(PublishedYear)}" +
                         "is less or equal to zero");
                publishedYear = value;
            }
        }

        /// <summary>
        /// Price of the book
        /// </summary>
        /// <exception cref="ArgumentException">Throws if value is 
        /// less or equal to zero</exception>
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException
                        ($"value for {nameof(Price)}" +
                         "is less or equal to zero");
                price = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException">Throws if one of the parameters is invalid</exception>
        public Book(string name, string author, int publishedYear, decimal price)
        {
            Name = name;
            Author = author;
            PublishedYear = publishedYear;
            Price = price;
        }


        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (!(other.GetType() == GetType()))
                return false;
            return Name.other.Name && Au
        }

        public int CompareTo(Book other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
                return true;
            if (ReferenceEquals(obj, null))
                return false;
            Book book = obj as Book;
            if (book == null)
                return false;
            return Equals(book);
        }

        public override string ToString()
        {
            return $"{nameof(Name)} = {Name}\n" +
                   $"{nameof(Author)} = {Author}\n" +
                   $"{nameof(Price)} = {Price}\n" +
                   $"{nameof(PublishedYear)} = {PublishedYear}";
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
