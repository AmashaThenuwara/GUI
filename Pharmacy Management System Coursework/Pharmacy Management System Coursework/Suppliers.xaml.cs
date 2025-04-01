using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Pharmacy_Management_System_Coursework
{
    /// <summary>
    /// Interaction logic for Suppliers.xaml
    /// </summary>
    public partial class Suppliers : Window
    {
        public Suppliers()
        {
            InitializeComponent();
        }

        private void Btn14_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtSupID.Text;
            string name = TxtSupName.Text;
            string number = TxtSupCNumber.Text;
            string address = TxtSupAddress.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Suppliers(SupplierID,Name,ContactNumber,Address) " +
                                          "VALUES (@id,@name,@number,@address)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("number", number);
                        command.Parameters.AddWithValue("address", address);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Suppliers saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadSupplierData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn15_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtSupID.Text;
            string name = TxtSupName.Text;
            string number = TxtSupCNumber.Text;
            string address = TxtSupAddress.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Suppliers SET Name = @name, ContactNumber = @number, Address = @address " +
                                  "WHERE SupplierID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("number", number);
                        command.Parameters.AddWithValue("address", address);
                        command.Parameters.AddWithValue("id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Suppliers updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadSupplierData();
                        }
                        else
                        {
                            MessageBox.Show("No suppliers found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn16_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtSupID.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Suppliers WHERE SupplierID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        // Confirm deletion
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this supplier?",
                                                                  "Confirm Deletion",
                                                                  MessageBoxButton.YesNo,
                                                                  MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadSupplierData();
                            }
                            else
                            {
                                MessageBox.Show("No supplier found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn17_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;



                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Suppliers";



                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        SuppliersDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}