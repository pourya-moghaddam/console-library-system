using System;

namespace LibrarySystem
{
    public class Book
    {
        private string _title;
        private string _author;

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public string Title {
            get
            {
                return _title;
            }
            private set
            {
                _title = value.Capitalize();
            }
        }
        public string Author
        {
            get
            {
                return _author;
            }
            private set
            {
                _author = value.Capitalize();
            }
        }
        public bool Available { get; set; }
    }
}
