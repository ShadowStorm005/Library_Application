using System;
using System.Collections.Generic;

namespace LibraryApplication
{
    internal class Customer
    {
        private string _name;
        private int _id;
        private List<Book> _borrowedBooks;

        // Constructor
        internal Customer(string name, int id)
        {
            Name = name;
            ID = id;
            BorrowedBooks = new List<Book>();
        }

        // Properties
        internal string Name
        {
            get { return _name; }
            set
            {
                foreach (char c in value)
                {
                    if (!char.IsLetter(c) && c != ' ') // _name cannot contain numbers or special characters
                    {
                        throw new ArgumentException("Name cannot contain numbers or special characters.");
                    }
                }
                if (string.IsNullOrWhiteSpace(value)) // _name cannot be empty or whitespace
                {
                    throw new ArgumentException("Name cannot be empty or whitespace.");
                }
                _name = value;
            }
        }
        internal int ID
        {
            get { return _id; }
            set
            {
                if (value <= 0) // _id must be a positive integer
                {
                    throw new ArgumentException("ID must be a positive integer.");
                }
                _id = value;
            }
        }
        internal List<Book> BorrowedBooks
        {
            get { return _borrowedBooks; }
            set
            {
                if (value == null) // _borrowedBooks cannot be null
                {
                    throw new ArgumentNullException("BorrowedBooks cannot be null.");
                }
                _borrowedBooks = value;
            }
        }

        // Method to borrow a book
        internal void BorrowBook(Book book)
        {
            if (book == null) // Book cannot be null
            {
                throw new ArgumentNullException("Book cannot be null.");
            }
            if (!book.Borrow()) // Book must be available to borrow
            {
                throw new InvalidOperationException("Book is not available for borrowing.");
            }
            BorrowedBooks.Add(book);
        }
        // Method to return a book
        internal void ReturnBook(Book book)
        {
            if (book == null) // Book cannot be null
            {
                throw new ArgumentNullException("Book cannot be null.");
            }
            if (!BorrowedBooks.Contains(book)) // Book must be in the borrowed list to return
            {
                throw new InvalidOperationException("Book is not borrowed by this customer.");
            }
            book.Return(); // Return the book
            BorrowedBooks.Remove(book); // Remove the book from the borrowed list
        }
        // Method to get the list of borrowed books
        /* Side note: The method GetBorrowedBooks is not necessary in the current implementation
           as the BorrowedBooks property already provides access to the list of borrowed books.
           However, It is included for for clarity and because it will be simple to further improve
           the method in the future by adding overloading methods to filter the list of borrowed books,
           for example, by title or author. This will be useful if the customer has borrowed many books.
        */
        internal List<Book> GetBorrowedBooks()
        {
            return BorrowedBooks; // Return the list of borrowed books
        }
    }
}