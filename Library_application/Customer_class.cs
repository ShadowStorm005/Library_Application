﻿using System;
using System.Collections.Generic;

namespace Library_application
{
    internal class Customer
    {
        private string _name;
        private string _id; // 6-digit number
        private List<Book> _borrowedBooks;

        // Constructor
        internal Customer(string name, string id)
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
                        throw new FormatException("Name cannot contain numbers or special characters.");
                    }
                }
                if (string.IsNullOrWhiteSpace(value)) // _name cannot be empty or whitespace
                {
                    throw new ArgumentException("Name cannot be empty or whitespace.");
                }
                _name = value;
            }
        }
        internal string ID
        {
            get { return _id; }
            set
            {
                // _id must be a 6-digit number
                if (value.Length != 6 || !uint.TryParse(value, out _) || value.StartsWith("-") || value.StartsWith("+"))
                {
                    throw new ArgumentOutOfRangeException("ID must be a 6-digit number.");
                }
                foreach (Customer customer in LibraryLogic.Instance.Customers)
                {
                    if (customer.ID == value) // Check if the ID already is in use
                    {
                        throw new ArgumentException("ID already in use.");
                    }
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
        // Method to get the customer's information
        internal List<string> GetDetails()
        {
            List<string> info = new List<string>();
            info.Add($"Name: {Name}");
            info.Add($"ID: {ID}");
            info.Add("Borrowed Books:");
            foreach (Book book in BorrowedBooks)
            {
                info.Add($"- {book.Title} by {book.Author} (ISBN: {book.ISBN})");
            }
            return info; // Return the customer's information
        }
        // Method to determine how the customer is represented as a string
        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Number Of Borrowed Books: {BorrowedBooks.Count}";
        }
    }
}