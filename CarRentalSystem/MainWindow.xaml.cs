using System;
using System.Windows;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using System.Collections.ObjectModel;

namespace CarRentalSystem
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<RentalDisplay> Rentals { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            CarsDataGrid.ItemsSource = Cars;
            CustomersDataGrid.ItemsSource = Customers;
            RentalsDataGrid.ItemsSource = Rentals;

            // Initialize ComboBoxes for booking form
            CarComboBox.ItemsSource = Cars;
            CarComboBox.DisplayMemberPath = "MakeModel";
            CustomerComboBox.ItemsSource = Customers;
            CustomerComboBox.DisplayMemberPath = "Name";
        }

        private void LoadData()
        {
            try
            {
                Cars = new ObservableCollection<Car>(DataGenerator.GenerateCars());
                Customers = new ObservableCollection<Customer>(DataGenerator.GenerateCustomers());
                Rentals = new ObservableCollection<RentalDisplay>(DataGenerator.GenerateRentals(new System.Collections.Generic.List<Car>(Cars), new System.Collections.Generic.List<Customer>(Customers)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Data Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a car.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CustomerComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (RentalDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select a rental date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ReturnDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select a return date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                MessageBox.Show("Please enter a price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedCar = (Car)CarComboBox.SelectedItem;
            var selectedCustomer = (Customer)CustomerComboBox.SelectedItem;
            var rentalDate = RentalDatePicker.SelectedDate.Value;
            var returnDate = ReturnDatePicker.SelectedDate.Value;
            decimal price;
            if (!decimal.TryParse(PriceTextBox.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create new RentalDisplay object
            var newRental = new RentalDisplay
            {
                Id = Rentals.Count + 1,
                CarDisplay = $"{selectedCar.Make} {selectedCar.Model}",
                CustomerDisplay = selectedCustomer.Name,
                RentalDate = rentalDate.ToShortDateString(),
                ReturnDate = returnDate.ToShortDateString(),
                Price = price.ToString("C")
            };

            Rentals.Add(newRental);

            // Clear form inputs
            CarComboBox.SelectedIndex = -1;
            CustomerComboBox.SelectedIndex = -1;
            RentalDatePicker.SelectedDate = null;
            ReturnDatePicker.SelectedDate = null;
            PriceTextBox.Clear();

            MessageBox.Show("Booking added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
