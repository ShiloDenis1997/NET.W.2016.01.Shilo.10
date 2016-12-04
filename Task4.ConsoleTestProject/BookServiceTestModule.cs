using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.BookListServiceLogic;
using Task4.BookLogic;
using Task4.BookStorageLogic;
using Task4.LoggerProviderLogic;

namespace Task4.ConsoleTestProject
{
    class BookServiceTestModule
    {
        private static ILogger logger = 
            LoggerProvider.GetLoggerForClassName(nameof(BookServiceTestModule));

        private static ILogger bookStorageLogger 
            = LoggerProvider.GetLoggerForClassName(nameof(BinaryFileBookStorage));
        private static ILogger bookListServiceLogger 
            = LoggerProvider.GetLoggerForClassName(nameof(BookListService));
        private static IBookListStorage storage;
        private static BookListService service;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            string filepath = "storage.bin";
            storage = new BinaryFileBookStorage(filepath, bookStorageLogger);
            service = new BookListService(bookListServiceLogger);

            do
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                logger.Debug("user selected menu item {0}.", choice);
                switch (choice)
                {
                    case "0":
                        goto userInputStopped;
                    case "1":
                        CreateBookListService();
                        break;
                    case "2":
                        ShowAllBooksFromService();
                        break;
                    case "3":
                        AddNewBookToService();
                        break;
                    case "4":
                        RemoveBookFromService();
                        break;
                    case "5":
                        SortBooksByDefault();
                        break;
                    case "6":
                        SortBooksByPrice();
                        break;
                    case "7":
                        SortBooksByAuthor();
                        break;
                    case "8":
                        FindBookWithPrice();
                        break;
                    case "9":
                        FindBookWithAuthor();
                        break;
                    case "10":
                        SetStorage();
                        break;
                    case "11":
                        StoreBooks();
                        break;
                    case "12":
                        LoadBooks();
                        break;
                }
            } while (true);
            userInputStopped:
            ;
            LoggerProvider.Flush();
        }

        static void OnUnhandledException
            (object sender, UnhandledExceptionEventArgs e)
        {
            logger.Fatal("Unhandled exception {0}", e.ExceptionObject);
            LoggerProvider.Flush();
        }

        static void PrintMenu()
        {
            Console.WriteLine("-------Menu-------\n" +
                              "\t0 - Exit\n" +
                              "\t1 - Create new BookListService\n" +
                              "\t2 - Show all books from BookListService\n" +
                              "\t3 - Add new book to BookListService\n" +
                              "\t4 - Remove book from BookListService\n" +
                              "\t5 - Sort books by default\n" +
                              "\t6 - Sort books by Price\n" +
                              "\t7 - Sort books by Author\n" +
                              "\t8 - Find book with price\n" +
                              "\t9 - Find book with author\n" +
                              "\t10 - Set storage\n" +
                              "\t11 - Store books\n" +
                              "\t12 - Load books\n");
        }

        static void CreateBookListService()
        {
            service = new BookListService(bookListServiceLogger);
        }

        static void ShowAllBooksFromService()
        {
            Console.WriteLine("Books from service:");
            foreach (Book book in service.GetListOfBooks())
            {
                Console.WriteLine(book);
            }
        }

        static void AddNewBookToService()
        {
            try
            {
                service.AddBook(GetBook());
                Console.WriteLine("Book added");
            }
            catch (BookListException ex)
            {
                logger.Warn(ex, "exception while adding book");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "users input data is incorrect");
                Console.WriteLine("Some input data is incorrect");
            }
        }

        static void RemoveBookFromService()
        {
            try
            {
                service.RemoveBook(GetBook());
                Console.WriteLine("Book removed");
            }
            catch (BookListException ex)
            {
                logger.Warn(ex, "exception while removing book");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "user input is incorrect");
                Console.WriteLine("Some input data is incorrect");
            }
        }

        static void SortBooksByDefault()
            => service.SortBooksByTag((b1, b2) => b1.CompareTo(b2));
        

        static void SortBooksByPrice()
            => service.SortBooksByTag((b1, b2) => b1.Price.CompareTo(b2.Price));
        

        static void SortBooksByAuthor()
            => service.SortBooksByTag((b1, b2) => string.Compare
                                (b1.Author, b2.Author, StringComparison.InvariantCulture));


        static void FindBookWithPrice()
        {
            try
            {
                Console.Write("Enter price: ");
                decimal d = decimal.Parse(Console.ReadLine());
                Book b = service.FindBookWithTag(book => book.Price == d);
                if (b == null)
                    Console.WriteLine("There are no books with such price");
                else
                {
                    Console.WriteLine("Book:\n" + b);
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "user's price input is incorrect");
                Console.WriteLine("Wrong price input");
            }
        }

        static void FindBookWithAuthor()
        {
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Book bk = service.FindBookWithTag(book =>
                    string.Compare(book.Author, author,
                    StringComparison.InvariantCulture) == 0);
            if (bk == null)
                Console.WriteLine("There are no books with such author");
            else
            {
                Console.WriteLine("Book:\n" + bk);
            }
        }

        static void SetStorage()
        {
            Console.WriteLine("Select storage type: \n" +
                              "\t1 - Binary storage via BinaryReader/Writer\n" +
                              "\t2 - Binary stroage via binary serializer\n" +
                              "\t3 - XML-storage\n" +
                              "\tOther - Do not specify storage");
            string ans = Console.ReadLine();
            int numAns;
            switch (ans)
            {
                case "1":
                    numAns = 1;
                    break;
                case "2":
                    numAns = 2;
                    break;
                case "3":
                    numAns = 3;
                    break;
                default:
                    return;
            }
            Console.Write("Enter filename: ");
            string filepath = Console.ReadLine();
            switch (numAns)
            {
                case 1:
                    storage = new BinaryFileBookStorage
                        (filepath, bookStorageLogger);
                    break;
                case 2:
                    storage = new BinarySerializatorBookStorage
                        (filepath, bookStorageLogger);
                    break;
                case 3:
                    storage = new XmlBookStorage
                        (filepath, bookStorageLogger);
                    break;
            }
        }

        static void StoreBooks()
        {
            try
            {
                service.StoreBooks(storage);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "exception while storing books");
                Console.WriteLine(ex.Message);
            }
        }

        static void LoadBooks()
        {
            try
            {
                service.LoadBooks(storage);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "exception while loading books");
                Console.WriteLine(ex.Message);
            }
        }

        static Book GetBook()
        {
            Console.Write("Enter book name: ");
            string name = Console.ReadLine();
            Console.Write("Enter book author: ");
            string author = Console.ReadLine();
            Console.Write("Enter book published year: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Enter book price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            return new Book(name, author, year, price);
        }
    }
}



