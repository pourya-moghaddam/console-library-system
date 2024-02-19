using System;

namespace LibrarySystem
{
    public class User
    {
        Random random = new Random();
        
        private string _name;
        private static readonly HashSet<int> _allUserIDs = new HashSet<int>();

        public User(string name)
        {
            Name = name;
            ID = GetRandomID();
            _allUserIDs.Add(ID);
            BorrowedBooks = new List<Book>();
        }

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value.Capitalize();
            }
        }
        public int ID { get; private set; }
        public List<Book> BorrowedBooks { get; set; }

        private int GetRandomID()
        {
            // Generate a random integer between 1000 and 10000
            IEnumerable<int> range = Enumerable.Range(1000, 9000)
                .Where(i => !_allUserIDs.Contains(i));
            int index = random.Next(1000, 10000 - _allUserIDs.Count);
            return range.ElementAt(index);
        }
    }
}
