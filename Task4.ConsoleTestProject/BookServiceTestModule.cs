using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Task4.BookListServiceLogic;
using Task4.BookLogic;
using Task4.BookStorageLogic;

namespace Task4.ConsoleTestProject
{
    class BookServiceTestModule
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void OnUnhandledException
            (object sender, UnhandledExceptionEventArgs e)
        {
            logger.Fatal("Unhandled exception {0}", e.ExceptionObject);
            LogManager.Flush();
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, eventArgs) => LogManager.Flush();
            string filepath = "storage.bin";
            BinaryFileBookStorage storage = new BinaryFileBookStorage(filepath);
            BookListService service = new BookListService();

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
                        service = new BookListService();
                        break;
                    case "2":
                        Console.WriteLine("Books from service:");
                        foreach (Book book in service.GetListOfBooks())
                        {
                            Console.WriteLine(book);
                        }
                        break;
                    case "3":
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
                        break;
                    case "4":
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
                        break;
                    case "5":
                        break;
                    case "6":
                        service.SortBooksByTag((b1, b2) => b1.Price.CompareTo(b2.Price));
                        break;
                    case "7":
                        service.SortBooksByTag(
                            (b1, b2) => string.Compare
                                (b1.Author, b2.Author, StringComparison.InvariantCulture));
                        break;
                    case "8":
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
                        break;
                    case "9":
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
                        break;
                    case "10":
                        Console.Write("Enter filename");
                        filepath = Console.ReadLine();
                        storage = new BinaryFileBookStorage(filepath);
                        break;
                    case "11":
                        try
                        {
                            service.StoreBooks(storage);
                        }
                        catch (Exception ex)
                        {
                            logger.Warn(ex, "exception while storing books");
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "12":
                        try
                        {
                            service.LoadBooks(storage);
                        }
                        catch (Exception ex)
                        {
                            logger.Warn(ex, "exception while loading books");
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
            } while (true);
            userInputStopped:
            ;
            LogManager.Flush();
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
                              "\t10 - Set storage filename\n" +
                              "\t11 - Store books\n" +
                              "\t12 - Load books\n");
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



