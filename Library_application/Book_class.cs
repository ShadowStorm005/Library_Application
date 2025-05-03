using System;

namespace LibraryApplication
{
    internal class  Book
    {
        private string _title;
        private string _author;
        private string _isbn; // ISBN 13 number without any number separators
        private bool _isAvailable;

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
                if (string.IsNullOrWhiteSpace(value))
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
                if (string.IsNullOrWhiteSpace(value))
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
                if (string.IsNullOrWhiteSpace(value) || value.Length != 13 || !int.TryParse(value, out _))
                {
                    throw new ArgumentException("ISBN must be a 13-digit number without any separators.");
                }
                _isbn = value;
            }
        }
        internal bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }

        // Method to loan the book
        internal void Loan()
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException("Book is not available to loan.");
            }
            IsAvailable = false;
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
        // Method to get book details
        internal string GetDetails()
        {
            return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Available: {IsAvailable}";
        }
    }
}