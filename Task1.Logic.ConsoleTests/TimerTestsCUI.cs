using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic.ConsoleTests
{
    class TimerTestsCui
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();
            TestSubscriber1 testSubscriber1 = new TestSubscriber1();
            TestSubscriber2 testSubscriber2 = new TestSubscriber2();
            do
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        goto userInputStopped;
                    case "1":
                        timer = new Timer();
                        Console.WriteLine("New timer created.\n");
                        break;
                    case "2":
                        timer.TimerOut += (sender, eventArgs) =>
                        {
                            Console.WriteLine($"Hello from anonymous method. " +
                                              $"Timer was setted to {eventArgs.Timeout}");
                        };
                        Console.WriteLine("Anonymous method subscribed.");
                        break;
                    case "3":
                        timer.TimerOut += testSubscriber1.TimerOutEventHandler;
                        Console.WriteLine("Instance method subscribed.");
                        break;
                    case "4":
                        timer.TimerOut +=
                            new EventHandler<TimerOutEventArgs>(testSubscriber2.TimerOutEventHandler);
                        Console.WriteLine("Instance method subscribed.");
                        break;
                    case "5":
                        timer.TimerOut += TimerOutEventHandler;
                        Console.WriteLine("Static method subscribed.");
                        break;
                    case "6":
                        Console.WriteLine("Enter timeout in miliseconds");
                        string stringTimeout = Console.ReadLine();
                        int timeout;
                        if (!int.TryParse(stringTimeout, out timeout))
                        {
                            Console.WriteLine("Input value had an invalid format.");
                        }
                        try
                        {
                            timer.SetTimer(timeout);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine("Attempt to set timer cause an" +
                                              $" {typeof(ArgumentOutOfRangeException)}: {ex.Message}");
                        }
                        break;
                }
            } while (true);
            userInputStopped:
            ;
        }
        
        static void PrintMenu()
        {
            Console.WriteLine("-------Menu-------\n" +
                              "\t0 - Exit\n" +
                              "\t1 - Create new Timer\n" +
                              "\t2 - Subscribe anonymous method\n" +
                              $"\t3 - Subscribe instance method of {nameof(TestSubscriber1)}\n" + 
                              $"\t4 - Subscribe instance method of {nameof(TestSubscriber2)}\n" +
                              "\t5 - Subscribe static method\n" +
                              "\t6 - Set timer\n");
        }

        static void TimerOutEventHandler(object sender, TimerOutEventArgs eventArgs)
        {
            Console.WriteLine($"Hello from static method. " +
                                 $"Timer was setted to {eventArgs.Timeout}");
        }

        private class TestSubscriber1
        {
            public void TimerOutEventHandler(object sender, TimerOutEventArgs eventArgs)
            {
                Console.WriteLine($"Hello from {nameof(TestSubscriber1)} instance method. " +
                                  $"Timer was setted to {eventArgs.Timeout}");
            }
        }

        private class TestSubscriber2
        {
            public void TimerOutEventHandler(object sender, TimerOutEventArgs eventArgs)
            {
                Console.WriteLine($"Hello from {nameof(TestSubscriber2)} instance method. " +
                                  $"Timer was setted to {eventArgs.Timeout}");
            }
        }
    }
}
