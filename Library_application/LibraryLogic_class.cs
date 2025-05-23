using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Library_application
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
        public List<Book> GetBooks()
        {
            return AllBooks;
        }
        // GetBooks overload method to filter books by title and/or author and/or avalibility
        public List<Book> GetBooks(string title, string author, bool onlyAvalible)
        {
            // Return a list of books that match the title and/or author
            if (title == string.Empty)
            {
                if (onlyAvalible)
                {
                    // If title is empty, and only avalible is checked, filter by author only
                    return AllBooks.FindAll(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase) && b.IsAvailable);
                }
                else
                {
                    // If title is empty, filter by author only
                    return AllBooks.FindAll(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
                }
            }
            else if (author == string.Empty)
            {
                if (onlyAvalible)
                {
                    // If author is empty, and only avalible is checked, filter by title only
                    return AllBooks.FindAll(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase) && b.IsAvailable);
                }
                else
                {
                    // If author is empty, filter by title only
                    return AllBooks.FindAll(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
                }
            }
            else if (onlyAvalible)
            {
                // If both title and author are provided, and only avalible is checked, filter by both
                return AllBooks.FindAll(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase) && 
                                            b.Author.Contains(author, StringComparison.OrdinalIgnoreCase) && 
                                            b.IsAvailable);
            }
            else
            {
                // If both title and author are provided, filter by both
                return AllBooks.FindAll(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase) && 
                                            b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            }
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
                throw new ArgumentException("Book not found, or not avalible.");
            }
        }
        // Method to borrow a book to a specified customer
        public void BorrowBook(string isbn, string customerId)
        {
            Book bookToBorrow = AllBooks.Find(b => b.ISBN == isbn);
            Customer customer = Customers.Find(c => c.ID == customerId);
            if (bookToBorrow != null && customer != null)
            {
                if (bookToBorrow.IsReserved && customer.ReservedBookISBN != bookToBorrow.ISBN)
                {
                    throw new ArgumentException("Book is reserved for another customer.");
                }
                else if (bookToBorrow.IsReserved && customer.ReservedBookISBN == bookToBorrow.ISBN)
                {
                    customer.BorrowBook(bookToBorrow); // Lend out the book to the specified customer
                    bookToBorrow.IsReserved = false; // Unreserve the book
                }
                else
                {
                    customer.BorrowBook(bookToBorrow); // Lend out the book to the specified customer
                }
            }
            else if (bookToBorrow == null)
            {
                throw new ArgumentException("Book not found.");
            }
            else if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
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
            else if (bookToReturn == null)
            {
                throw new ArgumentException("Book not found.");
            }
            else if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }
        }
        // Method to reserve a book for a specified customer
        public void ReserveBook(string isbn, string customerId)
        {
            Book bookToReserve = AllBooks.Find(b => b.ISBN == isbn);
            Customer customer = Customers.Find(c => c.ID == customerId);
            if (bookToReserve != null && !bookToReserve.IsReserved && !bookToReserve.IsAvailable && customer != null)
            {
                if (!bookToReserve.IsReserved && customer.BorrowedBooks.Contains(bookToReserve))
                {
                    throw new ArgumentException("Book is already borrowed for this customer.");
                }
                else
                {
                    customer.ReserveBook(bookToReserve); // Reserve the book for the specified customer
                }
            }
            else if (bookToReserve == null)
            {
                throw new ArgumentException("Book not found.");
            }
            else if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }
            else if (bookToReserve.IsReserved)
            {
                throw new ArgumentException("Book is already reserved.");
            }
            else if (bookToReserve.IsAvailable)
            {
                throw new ArgumentException("Book is available and cannot be reserved. You can borrow it.");
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
            else if (customerToRemove == null)
            {
                throw new ArgumentException("Customer not found.");
            }
            else if (customerToRemove.BorrowedBooks.Count > 0)
            {
                throw new ArgumentException("Customer has borrowed books and cannot be removed.");
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