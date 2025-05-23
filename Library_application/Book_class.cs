using System;

namespace Library_application
{
    internal class Book
    {
        private string _title;
        private string _author;
        private string _isbn; // ISBN 13 number without any number separators
        private bool _isAvailable;
        private bool _isReserved; // Reserved books are not available for borrowing

        // Constructor
        internal Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsAvailable = true; // New books are available by default
        }

        // Properties
        internal string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) // _title cannot be empty or whitespace
                {
                    throw new ArgumentException("Title cannot be empty or whitespace.");
                }
                _title = value;
            }
        }
        internal string Author
        {
            get { return _author; }
            set
            {
                foreach (char c in value)
                {
                    if (!char.IsLetter(c) && c != ' ' && c != '.') // _author cannot contain numbers or special characters
                    {
                        throw new FormatException("Author cannot contain numbers or special characters.");
                    }
                }
                if (string.IsNullOrWhiteSpace(value)) // _author cannot be empty or whitespace
                {
                    throw new ArgumentException("Author cannot be empty or whitespace.");
                }
                _author = value;
            }
        }
        internal string ISBN
        {
            get { return _isbn; }
            set
            {
                // ISBN must be a 13-digit number without any separators
                if (value.Length != 13 || !ulong.TryParse(value, out _) || value.StartsWith("-") || value.StartsWith("+"))
                {
                    throw new ArgumentOutOfRangeException("ISBN must be a 13-digit number without any separators.");
                }
                foreach (Book book in LibraryLogic.Instance.AllBooks) // Check if the ISBN already is in use
                {
                    if (book.ISBN == value)
                    {
                        throw new ArgumentException("ISBN already exists.");
                    }
                }
                _isbn = value;
            }
        }
        internal bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }
        internal bool IsReserved // Property to check if the book is reserved
        {
            get { return _isReserved; }
            set { _isReserved = value; }
        }
        public string Details // Property to get book details when displaying the book in a list
        {
            get { return GetDetails(); }
        }

        // Method to loan the book
        internal bool Borrow()
        {
            if (!IsAvailable)
            {
                return false; // Book is not available for borrowing
            }
            else
            {
                IsAvailable = false;
                return true; // Book is successfully borrowed
            }
        }
        // Method to return the book
        internal void Return()
        {
            if (IsAvailable)
            {
                throw new InvalidOperationException("Book is already available.");
            }
            IsAvailable = true;
        }
        // Method to reserve the book
        internal bool Reserve()
        {
            if (IsReserved)
            {
                return false; // Book is already reserved
            }
            else
            {
                IsReserved = true;
                return true; // Book is successfully reserved
            }
        }
        // Method to get book details
        internal string GetDetails()
        {
            return $"{Title} | {Author} | {ISBN} | {IsAvailable} | {IsReserved}";
        }
        // Method to determine how the book is represented as a string
        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}";
        }
    }
}