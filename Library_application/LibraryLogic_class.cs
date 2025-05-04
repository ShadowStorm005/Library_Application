using System;
using System.Collections.Generic;

namespace LibraryApplication
{
    internal class LibraryLogic
    {
        // List of all Books and all Customers
        private List<Book> _allBooks;
        private List<Customer> _customers;

        // Singleton pattern to ensure only one instance of LibraryLogic exists
        private static LibraryLogic _instance;
        public static LibraryLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LibraryLogic();
                }
                return _instance;
            }
        }

        // Private constructor only accesed within the LibraryLogic Instance
        private LibraryLogic()
        {
            AllBooks = new List<Book>();
            Customers = new List<Customer>();
        }

        // Properties
        internal List<Book> AllBooks
        {
            get { return _allBooks; }
            set
            {
                if (value == null) // AllBooks cannot be null
                {
                    throw new ArgumentNullException("AllBooks cannot be null.");
                }
                _allBooks = value;
            }
        }
        internal List<Customer> Customers
        {
            get { return _customers; }
            set
            {
                if (value == null) // Customers cannot be null
                {
                    throw new ArgumentNullException("Customers cannot be null.");
                }
                _customers = value;
            }
        }

        // Method to get a list of all available books
        /* Side note: The method GetBooks is not necessary in the current implementation
           as the AllBooks property already provides access to the list of all books.
           However, It is included for for clarity and because it will be simple to further improve
           the method in the future by adding overloading methods to filter the list of all books,
           for example, by title or author. This will be useful if there exists many books.
        */
        public List<Book> GetBooks()
        {
            return AllBooks;
        }
        // Method to add a new book to the library
        public void AddBook(string title, string author, string isbn)
        {
            AllBooks.Add(new Book(title, author, isbn)); // Add a new book to the list of all values are valid
        }
        // Method to remove a book from the library
        public void RemoveBook(string isbn)
        {
            Book bookToRemove = AllBooks.Find(b => b.ISBN == isbn);
            if (bookToRemove != null && bookToRemove.IsAvailable)
            {
                AllBooks.Remove(bookToRemove); // Remove the book from the list of all books
            }
            else
            {
                throw new ArgumentException("Book not found.");
            }
        }
        // Method to borrow a book to a specified customer
        public void BorrowBook(string isbn, string customerId)
        {
            Book bookToBorrow = AllBooks.Find(b => b.ISBN == isbn);
            Customer customer = Customers.Find(c => c.ID == customerId);
            if (bookToBorrow != null && customer != null)
            {
                customer.BorrowBook(bookToBorrow); // Borrow the book to the specified customer
            }
            else
            {
                throw new ArgumentException("Book or Customer not found.");
            }
        }
        // Method to return a book from a specified customer
        public void ReturnBook(string isbn, string customerId)
        {
            Book bookToReturn = AllBooks.Find(b => b.ISBN == isbn);
            Customer customer = Customers.Find(c => c.ID == customerId);
            if (bookToReturn != null && customer != null)
            {
                customer.ReturnBook(bookToReturn); // Return the book from the specified customer
            }
            else
            {
                throw new ArgumentException("Book or Customer not found.");
            }
        }
        // Method to add a new customer to the library
        public void AddCustomer(string name, string id)
        {
            Customers.Add(new Customer(name, id)); // Add a new customer to the list of all values are valid
        }
        // Method to remove a customer from the library
        public void RemoveCustomer(string id)
        {
            Customer customerToRemove = Customers.Find(c => c.ID == id);
            if (customerToRemove != null && customerToRemove.BorrowedBooks.Count == 0)
            {
                Customers.Remove(customerToRemove); // Remove the customer from the list of all customers
            }
            else
            {
                throw new ArgumentException("Customer not found or has borrowed books.");
            }
        }
        // Method to get info about a specified book
        public string GetBookDetails(string isbn)
        {
            Book book = AllBooks.Find(b => b.ISBN == isbn);
            if (book != null)
            {
                return book.GetDetails();
            }
            else
            {
                throw new ArgumentException("Book not found.");
            }
        }
        // Method to get info about a specified customer
        public List<string> GetCustomerDetails(string id)
        {
            Customer customer = Customers.Find(c => c.ID == id);
            if (customer != null)
            {
                return customer.GetDetails();
            }
            else
            {
                throw new ArgumentException("Customer not found.");
            }
        }
        // Method to get a list of all borrowed books for a specified customer
        public List<Book> GetBorrowedBooks(string id)
        {
            Customer customer = Customers.Find(c => c.ID == id);
            if (customer != null)
            {
                return customer.GetBorrowedBooks(); // Return the list of borrowed books for the specified customer
            }
            else
            {
                throw new ArgumentException("Customer not found.");
            }
        }
    }
}