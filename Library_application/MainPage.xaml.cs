using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Library_application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly LibraryLogic libraryLogic = LibraryLogic.Instance;
        public MainPage()
        {
            this.InitializeComponent();
            // Preload some sample books and customers for testing purposes
            libraryLogic.AllBooks.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565"));
            libraryLogic.AllBooks.Add(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
            libraryLogic.AllBooks.Add(new Book("1984", "George Orwell", "9780451524935"));
            libraryLogic.AllBooks.Add(new Book("Pineapple Express", "James Franco", "9781452170523"));
            libraryLogic.AllBooks.Add(new Book("The Catcher in the Rye", "J.D. Salinger", "9780316769488"));
            libraryLogic.AllBooks.Add(new Book("The Hobbit", "J.R.R. Tolkien", "9780547928227"));
            libraryLogic.AllBooks.Add(new Book("The Lord of the Rings", "J.R.R. Tolkien", "9780544003415"));
            libraryLogic.AllBooks.Add(new Book("The Alchemist", "Paulo Coelho", "9780062315007"));
            libraryLogic.AllBooks.Add(new Book("The Da Vinci Code", "Dan Brown", "9780307474278"));
            libraryLogic.AllBooks.Add(new Book("The Hunger Games", "Suzanne Collins", "9780439023528"));
            libraryLogic.Customers.Add(new Customer("John Doe", "123456"));
            libraryLogic.Customers.Add(new Customer("Jane Smith", "654321"));
            libraryLogic.Customers.Add(new Customer("Alice Johnson", "111111"));
            libraryLogic.Customers.Add(new Customer("Bob Brown", "222222"));
            libraryLogic.Customers.Add(new Customer("Charlie Green", "333333"));
            libraryLogic.BorrowBook("9780743273565", "123456"); // John Doe borrows The Great Gatsby
            libraryLogic.BorrowBook("9780061120084", "654321"); // Jane Smith borrows To Kill a Mockingbird
            libraryLogic.BorrowBook("9780451524935", "111111"); // Alice Johnson borrows 1984
        }

        // Method to toggle the visibility of a given grid and make sure the other grids are hidden
        private void ToggleGridVisibility(Grid grid, object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the specified grid
            if (grid.Visibility == Visibility.Visible)
            {
                grid.Visibility = Visibility.Collapsed;
            }
            else // Make sure that only one grid is visible at a time
            {
                // Hide any other visible grids
                if (RegisterNewBookGrid.Visibility == Visibility.Visible)
                {
                    RegisterNewBookButton_Click(sender, e);
                }
                if (RemoveBookGrid.Visibility == Visibility.Visible)
                {
                    RemoveBookButton_Click(sender, e);
                }
                if (RegisterNewCustomerGrid.Visibility == Visibility.Visible)
                {
                    RegisterNewCustomerButton_Click(sender, e);
                }
                if (RemoveCustomerGrid.Visibility == Visibility.Visible)
                {
                    RemoveCustomerButton_Click(sender, e);
                }
                if (LendBookToCustomerGrid.Visibility == Visibility.Visible)
                {
                    LendBookToCustomerButton_Click(sender, e);
                }
                if (ReturnBookGrid.Visibility == Visibility.Visible)
                {
                    ReturnBookFromCustomerButton_Click(sender, e);
                }
                if (ShowInformationOnCustomersGrid.Visibility == Visibility.Visible)
                {
                    ShowInformationOnCustomersButton_Click(sender, e);
                }
                if (SeeAllBooksGrid.Visibility == Visibility.Visible)
                {
                    SeeAllBooksButton_Click(sender, e);
                }
                // Show the specified grid
                grid.Visibility = Visibility.Visible;
            }
        }

        // RegisterNewBookButton_Click event handler
        private void RegisterNewBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the RegisterNewBookGrid
            ToggleGridVisibility(RegisterNewBookGrid, sender, e);
            // Clear the input fields
            NewBookTitleTextBox.Text = string.Empty;
            NewBookAuthorTextBox.Text = string.Empty;
            NewBookISBNTextBox.Text = string.Empty;
        }
        // AddNewBookToLibraryButton_Click event handler
        private void AddNewBookToLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryLogic.AddBook(
                    NewBookTitleTextBox.Text, 
                    NewBookAuthorTextBox.Text, 
                    NewBookISBNTextBox.Text);
                // Dislpay a success message
                var successMessage = new Windows.UI.Popups.MessageDialog("Book added successfully!", "Success");
                _ = successMessage.ShowAsync();
                // Hide the RegisterNewBookGrid and clear the input fields
                RegisterNewBookButton_Click(sender, e);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Display an error message if the error raising value is out of range
                var error = new Windows.UI.Popups.MessageDialog(ex.ParamName, "Error");
                _ = error.ShowAsync();
            }
            catch (ArgumentException ex)
            {
                // Display an error message if the error raising value is invalid
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (FormatException ex)
            {
                // Display an error message if the format of the input is incorrect
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (Exception ex)
            {
                // Display a generic error message for any other exceptions
                var error = new Windows.UI.Popups.MessageDialog("An unexpected error occurred: " + ex.Message, "Error");
                _ = error.ShowAsync();
            }
        }

        // RemoveBookButton_Click event handler
        private void RemoveBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the RemoveBookGrid
            ToggleGridVisibility(RemoveBookGrid, sender, e);
            // Clear the input fields
            RemoveBookISBNTextBox.Text = string.Empty;
        }
        // RemoveBookFromLibraryButton_Click event handler
        private void RemoveBookFromLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryLogic.RemoveBook(RemoveBookISBNTextBox.Text);
                // Display a success message
                var successMessage = new Windows.UI.Popups.MessageDialog("Book removed successfully!", "Success");
                _ = successMessage.ShowAsync();
                // Hide the RemoveBookGrid and clear the input fields
                RemoveBookButton_Click(sender, e);
            }
            catch (ArgumentException ex)
            {
                // Display an error message if the error raising value is invalid
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (Exception ex)
            {
                // Display a generic error message for any other exceptions
                var error = new Windows.UI.Popups.MessageDialog("An unexpected error occurred: " + ex.Message, "Error");
                _ = error.ShowAsync();
            }
        }

        // RegisterNewCustomerButton_Click event handler
        private void RegisterNewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the RegisterNewCustomerGrid
            ToggleGridVisibility(RegisterNewCustomerGrid, sender, e);
            // Clear the input fields
            NewCustomerNameTextBox.Text = string.Empty;
            NewCustomerIDTextBox.Text = string.Empty;
        }
        // AddNewCustomerToLibraryButton_Click event handler
        private void AddNewCustomerToLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryLogic.AddCustomer(
                    NewCustomerNameTextBox.Text,
                    NewCustomerIDTextBox.Text);
                // Display a success message
                var successMessage = new Windows.UI.Popups.MessageDialog("Customer added successfully!", "Success");
                _ = successMessage.ShowAsync();
                // Hide the RegisterNewCustomerGrid and clear the input fields
                RegisterNewCustomerButton_Click(sender, e);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Display an error message if the error raising value is out of range
                var error = new Windows.UI.Popups.MessageDialog(ex.ParamName, "Error");
                _ = error.ShowAsync();
            }
            catch (ArgumentException ex)
            {
                // Display an error message if the error raising value is invalid
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (FormatException ex)
            {
                // Display an error message if the format of the input is incorrect
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (Exception ex)
            {
                // Display a generic error message for any other exceptions
                var error = new Windows.UI.Popups.MessageDialog("An unexpected error occurred: " + ex.Message, "Error");
                _ = error.ShowAsync();
            }
        }

        // RemoveCustomerButton_Click event handler
        private void RemoveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the RemoveCustomerGrid
            ToggleGridVisibility(RemoveCustomerGrid, sender, e);
            // Clear the input fields
            RemoveCustomerIDTextBox.Text = string.Empty;
        }
        // RemoveCustomerFromLibraryButton_Click event handler
        private void RemoveCustomerFromLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryLogic.RemoveCustomer(RemoveCustomerIDTextBox.Text);
                // Display a success message
                var successMessage = new Windows.UI.Popups.MessageDialog("Customer removed successfully!", "Success");
                _ = successMessage.ShowAsync();
                // Hide the RemoveCustomerGrid and clear the input fields
                RemoveCustomerButton_Click(sender, e);
            }
            catch (ArgumentException ex)
            {
                // Display an error message if the error raising value is invalid
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (Exception ex)
            {
                // Display a generic error message for any other exceptions
                var error = new Windows.UI.Popups.MessageDialog("An unexpected error occurred: " + ex.Message, "Error");
                _ = error.ShowAsync();
            }
        }

        // LendBookToCustomerButton_Click event handler
        private void LendBookToCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the LendBookToCustomerGrid
            ToggleGridVisibility(LendBookToCustomerGrid, sender, e);
            // Clear the input fields
            LendOutSearchedBooksListView.ItemsSource = null; // Reset the list view
            LendOutSearchedBooksListView.ItemsSource = libraryLogic.GetBooks(); // Set the list view source to the list of all books
            LendOutSearchedBooksListView.SelectedIndex = -1; // Deselect any selected item
            LendOutSearchBookTitleTextBox.Text = string.Empty; // Clear the title search text box
            LendOutSearchBookAuthorTextBox.Text = string.Empty; // Clear the author search text box
            LendOutSearchAvalibleCheckBox.IsChecked = false; // Uncheck the available books checkbox
            LendOutBookCustomerIDTextBox.Text = string.Empty; // Clear the customer ID text box
        }
        // BorrowBookButton_Click event handler
        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a book is selected in the list view
            if (LendOutSearchedBooksListView.SelectedItem == null)
            {
                // Display an error message if no book is selected
                var error = new Windows.UI.Popups.MessageDialog("Please select a book to borrow.", "Error");
                _ = error.ShowAsync();
                return;
            }
            // Check if the customer ID is provided
            if (string.IsNullOrWhiteSpace(LendOutBookCustomerIDTextBox.Text))
            {
                // Display an error message if the customer ID is empty
                var error = new Windows.UI.Popups.MessageDialog("Please enter a customer ID.", "Error");
                _ = error.ShowAsync();
                return;
            }
            try
            {
                Book selectedBook = (Book)LendOutSearchedBooksListView.SelectedItem;
                libraryLogic.BorrowBook(
                    selectedBook.ISBN,
                    LendOutBookCustomerIDTextBox.Text);
                // Display a success message
                var successMessage = new Windows.UI.Popups.MessageDialog("Book was borrowed successfully!", "Success");
                _ = successMessage.ShowAsync();
                // Hide the LendBookToCustomerGrid and clear the input fields
                LendBookToCustomerButton_Click(sender, e);
            }
            catch (ArgumentException ex)
            {
                // Display an error message if the input value is incorrect
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (InvalidOperationException ex)
            {
                // Display an error message if the book is not available for borrowing
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (Exception ex)
            {
                // Display a generic error message for any other exceptions
                var error = new Windows.UI.Popups.MessageDialog("An unexpected error occurred: " + ex.Message, "Error");
                _ = error.ShowAsync();
            }
        }
        // LendOutSearchBookTitleTextBox_TextChanged event handler
        private void LendOutSearchedBooksListView_FilterUpdate(object sender, TextChangedEventArgs e)
        {
            // Update the list to filter the books by title and author
            LendOutSearchedBooksListView.ItemsSource = libraryLogic.GetBooks(LendOutSearchBookTitleTextBox.Text, 
                                                                           LendOutSearchBookAuthorTextBox.Text, 
                                                                           (bool)LendOutSearchAvalibleCheckBox.IsChecked);
        }
        // LendOutSearchAvalibleCheckBox_Toggle event handler
        private void LendOutSearchAvalibleCheckBox_Toggle(object sender, RoutedEventArgs e)
        {
            // Update the list to filter the books by availability
            LendOutSearchedBooksListView.ItemsSource = libraryLogic.GetBooks(LendOutSearchBookTitleTextBox.Text, 
                                                                           LendOutSearchBookAuthorTextBox.Text, 
                                                                           (bool)LendOutSearchAvalibleCheckBox.IsChecked);
        }

        // ReturnBookFromCustomerButton_Click event handler
        private void ReturnBookFromCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the ReturnBookFromCustomerGrid
            ToggleGridVisibility(ReturnBookGrid, sender, e);
            // Clear the input fields
            ReturnBookISBNTextBox.Text = string.Empty;
            ReturnBookCustomerIDTextBox.Text = string.Empty;
        }
        // ReturnBookButton_Click event handler
        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                libraryLogic.ReturnBook(
                    ReturnBookISBNTextBox.Text,
                    ReturnBookCustomerIDTextBox.Text);
                // Display a success message
                var successMessage = new Windows.UI.Popups.MessageDialog("Book was returned successfully!", "Success");
                _ = successMessage.ShowAsync();
                // Hide the ReturnBookFromCustomerGrid and clear the input fields
                ReturnBookFromCustomerButton_Click(sender, e);
            }
            catch (ArgumentException ex)
            {
                // Display an error message if the input value is incorrect
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (InvalidOperationException ex)
            {
                // Display an error message if the book is not borrowed by the customer
                var error = new Windows.UI.Popups.MessageDialog(ex.Message, "Error");
                _ = error.ShowAsync();
            }
            catch (Exception ex)
            {
                // Display a generic error message for any other exceptions
                var error = new Windows.UI.Popups.MessageDialog("An unexpected error occurred: " + ex.Message, "Error");
                _ = error.ShowAsync();
            }
        }

        // ShowInformationOnCustomersButton_Click event handler
        private void ShowInformationOnCustomersButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the ShowInformationOnCustomersGrid
            ToggleGridVisibility(ShowInformationOnCustomersGrid, sender, e);
            // Update the lists
            CustomerInformationListView.ItemsSource = null; // Reset the list view
            CustomerInformationListView.ItemsSource = libraryLogic.Customers; // Set the list view source to the list of customers
            CustomerInformationListView.SelectedIndex = -1; // Deselect any selected item
            CustomersBorrowedBooksListView.ItemsSource = null; // Reset the borrowed books list view
        }
        // CustomerInformationListView_ItemClick event handler
        private void CustomerInformationListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Display the selected customer's borrowed books in the CustomersBorrowedBooksListView
            var selectedCustomer = (Customer)e.ClickedItem;
            CustomersBorrowedBooksListView.ItemsSource = null; // Reset the list view
            // Set the list view source to the selected customer's borrowed books
            CustomersBorrowedBooksListView.ItemsSource = selectedCustomer.BorrowedBooks;
        }

        // SeeAllBooksButton_Click event handler
        private void SeeAllBooksButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the SeeAllBooksGrid
            ToggleGridVisibility(SeeAllBooksGrid, sender, e);
            // Update the list of all books and clear the search text boxes
            SearchedBooksListView.ItemsSource = null; // Reset the list view
            SearchedBooksListView.ItemsSource = libraryLogic.GetBooks(); // Set the list view source to the list of all books
            SearchedBooksListView.SelectedIndex = -1; // Deselect any selected item
            SearchBookTitleTextBox.Text = string.Empty; // Clear the title search text box
            SearchBookAuthorTextBox.Text = string.Empty; // Clear the author search text box
            SearchAvalibleCheckBox.IsChecked = false; // Uncheck the available books checkbox
        }
        // SearchedBooksListView_FilterUpdate event handler
        private void SearchedBooksListView_FilterUpdate(object sender, TextChangedEventArgs e)
        {
            // Update the list to filter the books by title and author
            SearchedBooksListView.ItemsSource = libraryLogic.GetBooks(SearchBookTitleTextBox.Text, 
                                                                    SearchBookAuthorTextBox.Text, 
                                                                    (bool)SearchAvalibleCheckBox.IsChecked);
        }
        // SearchAvalibleCheckBox_Toggle event handler
        private void SearchAvalibleCheckBox_Toggle(object sender, RoutedEventArgs e)
        {
            // Update the list to filter the books by availability
            SearchedBooksListView.ItemsSource = libraryLogic.GetBooks(SearchBookTitleTextBox.Text, 
                                                                    SearchBookAuthorTextBox.Text, 
                                                                    (bool)SearchAvalibleCheckBox.IsChecked);
        }

        // ExitButton_Click event handler
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit(); // Close the application
        }
    }
}
