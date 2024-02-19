using System;
using System.Collections.Immutable;

namespace LibrarySystem
{
    public class Library
    {
        private readonly List<User> _users;
        private readonly List<Book> _books;

        public Library()
        {
            _users = new List<User>();
            _books = new List<Book>();
        }

        public void LendBook(User user)
        {
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();
            Console.Write("Enter book author: ");
            string author = Console.ReadLine();

            Book book;
            if (DoesBookExist(title, author))
            {
                book = GetBookByTitleAndAuthor(title, author);
            }
            else
            {
                Console.WriteLine("Unfortunately, we do not have that book.\n");
                return;
            }

            if (book.Available)
            {
                user.BorrowedBooks.Add(book);
                book.Available = false;
                Console.WriteLine(
                    "Here's {0} by {1}. Have a good read!\n", book.Title, book.Author);
            }
            else
            {
                Console.WriteLine("The book that you want is not available.\n");
            }
        }
        public void ReceiveBook(User user)
        {
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();
            Console.Write("Enter book author: ");
            string author = Console.ReadLine();
            Book book = GetBookByTitleAndAuthor(title, author);

            if (user.BorrowedBooks.Contains(book))
            {
                user.BorrowedBooks.Remove(book);
                book.Available = true;
                Console.WriteLine(
                    "Thanks. Have a good day.\n");
            }
            else
            {
                Console.WriteLine("You haven't borrowed such book.\n");
            }
        }
        public void AddUser(User user)
        {
            _users.Add(user);
        }
        public bool AddBook(Book book)
        {
            if (!DoesBookExist(book.Title, book.Author))
            {
                book.Available = true;
                _books.Add(book);
                return true;
            }
            return false;
        }
        public bool RemoveBook(string bookTitle, string bookAuthor)
        {
            if (DoesBookExist(bookTitle, bookAuthor))
            {
                Book book = GetBookByTitleAndAuthor(bookTitle, bookAuthor);
                _books.Remove(book);
                return true;
            }
            return false;
        }
        public void DisplayAvailableBooks()
        {
            if (!_books.Any())
            {
                Console.WriteLine("- No books available -");
            }
            else
            {
                List<Book> availableBooks = _books
                    .Where(b => b.Available)         // Get all available books and
                    .OrderBy(b => b.Title).ToList(); // sort them by their titles
                Console.WriteLine("- Available Books -");
                foreach (Book book in availableBooks)
                {
                    Console.WriteLine("[] '{0}' by {1}", book.Title, book.Author);
                }
            }
            Console.WriteLine();
        }
        public void DisplayUsers()
        {
            if (!_users.Any())
            {
                Console.WriteLine("- No users -");
            }
            else
            {
                List<User> sortedUsers = _users.OrderBy(u => u.Name).ToList(); // Sort users by their names
                Console.WriteLine("- All Users -");
                foreach (User user in sortedUsers)
                {
                    Console.WriteLine("~ {0} > {1}", user.Name, user.ID);
                }
            }
            Console.WriteLine();
        }
        public bool IsIDValid(string name, int id)
        {
            User user = GetUserByName(name);
            return user.ID == id;
        }
        public User GetUserByName(string name)
        {
            return _users.Find(u => u.Name == name.Capitalize());
        }
        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            return _books.Find(
                b => (b.Title == title.Capitalize()) && (b.Author == author.Capitalize()));
        }
        public bool DoesUserExist(string name)
        {
            return _users.Any(u => u.Name == name.Capitalize());
        }
        public bool DoesBookExist(string title, string author)
        {
            return _books.Any(
                b => (b.Title == title.Capitalize()) && (b.Author == author.Capitalize()));
        }
    }
}
