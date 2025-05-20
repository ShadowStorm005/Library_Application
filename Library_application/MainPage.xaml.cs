using System;
using System.Collections.Generic;
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
    }
}
