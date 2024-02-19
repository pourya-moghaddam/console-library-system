using System;
using System.Text.RegularExpressions;

namespace LibrarySystem
{
    class Program
    {
        private static Library library = new Library();
        const string Password = "password";

        static void Main(string[] args)
        {
            try
            {
                do
                {
                    Console.WriteLine("====== Library System ======");
                    Console.WriteLine("[1] Users entrance");
                    Console.WriteLine("[2] Manager entrance");
                    Console.WriteLine("[0] Exit");

                    Console.Write("Enter your choice: ");
                    int mainChoice = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    switch (mainChoice)
                    {
                        case 1:
                            DoUserActions(library);
                            break;
                        case 2:
                            DoManagerActions(library);
                            break;
                        case 0:
                            Console.WriteLine("Exiting the program... Goodbye!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Exiting the program...");
                Console.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
            }
        }
        private static void DoUserActions(Library library)
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            if ((userName.Length == 0) || userName.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid name. Try again.");
                return;
            }
            Console.Clear();

            User user;
            if (!library.DoesUserExist(userName))
            {
                user = new User(userName);
                library.AddUser(user);
                Console.WriteLine(
                    "Welcome to our library, {0}! This is your ID: {1}\n",
                    userName,
                    user.ID
                );
            }
            else
            {
                user = library.GetUserByName(userName);
                Console.WriteLine("Welcome back, {0}!\n", userName);
            }

            Console.Write("Enter your ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (!library.IsIDValid(user.Name, id))
            {
                Console.WriteLine("Invalid PIN.\n");
                return;
            }

            try
            {
                do
                {
                    Console.WriteLine("====== Library Services ======");
                    Console.WriteLine("[1] Borrow a book");
                    Console.WriteLine("[2] Return a book");
                    Console.WriteLine("[3] Donate a book");
                    Console.WriteLine("[4] View available books list");
                    Console.WriteLine("[0] Back to Main Menu");

                    Console.Write("Enter your choice: ");
                    int bankChoice = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    switch (bankChoice)
                    {
                        case 1:
                            library.LendBook(user);
                            break;
                        case 2:
                            library.ReceiveBook(user);
                            break;
                        case 3:
                            Console.Write("Enter book title: ");
                            string bookTitle = Console.ReadLine();
                            Console.Write("Enter book author: ");
                            string bookAuthor = Console.ReadLine();
                            Book book = new Book(bookTitle, bookAuthor);
                            if (library.AddBook(book))
                            {
                                Console.WriteLine("Thanks for your donation.\n");
                            }
                            else
                            {
                                Console.WriteLine(
                                    "That book already exists in our inventory. But that was kind of you.\n");
                            }
                            break;
                        case 4:
                            library.DisplayAvailableBooks();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.\n");
                            break;
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Try again.");
                Console.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
            }
        }
        private static void DoManagerActions(Library library)
        {
            // Get password without displaying input in console
            Console.Write("Enter password: ");
            string password = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if ((key.Key == ConsoleKey.Backspace) && (password.Length > 0))
                {
                    password = password.Remove(password.Length - 1);
                }
                else
                {
                    password += key.KeyChar;
                }
            }

            if (password != Password)
            {
                Console.WriteLine("Invalid password.\n");
                return;
            }

            try
            {
                do
                {
                    Console.WriteLine("====== Library Management ======");
                    Console.WriteLine("[1] Add a book");
                    Console.WriteLine("[2] Remove a book");
                    Console.WriteLine("[3] View available books list");
                    Console.WriteLine("[4] View all users");
                    Console.WriteLine("[0] Back to Main Menu");

                    Console.Write("Enter your choice: ");
                    int bankChoice = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    switch (bankChoice)
                    {
                        
                        case 1:
                            Console.Write("Enter book title: ");
                            string bookTitle = Console.ReadLine();
                            Console.Write("Enter book author: ");
                            string bookAuthor = Console.ReadLine();
                            Book book = new Book(bookTitle, bookAuthor);
                            if (library.AddBook(book))
                            {
                                Console.WriteLine("Book added successfully.\n");
                            }
                            else
                            {
                                Console.WriteLine(
                                    "That book already exists in our inventory.\n");
                            }
                            break;
                        case 2:
                            Console.Write("Enter book name: ");
                            bookTitle = Console.ReadLine();
                            Console.Write("Enter book author: ");
                            bookAuthor = Console.ReadLine();
                            if (library.RemoveBook(bookTitle, bookAuthor))
                            {
                                Console.WriteLine("Book removed successfully.\n");
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Such book doesn't exist in our inventory.\n");
                            }
                            break;
                        case 3:
                            library.DisplayAvailableBooks();
                            break;
                        case 4:
                            library.DisplayUsers();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.\n");
                            break;
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Try again.");
                Console.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
            }
        }
    }
}
