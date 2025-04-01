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
    /// Interaction logic for Medicines.xaml
    /// </summary>
    public partial class Medicines : Window
    {
        public Medicines()
        {
            InitializeComponent();
        }

        private void Btn18_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtMedID.Text;
            string name = TxtMedName.Text;
            string sid = TxtSupID.Text;
            string date = TxtMedEDate.Text;
            string quantity = TxtMedQuantity.Text;
            string price = TxtMedPrice.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Medicines (MedicineID,MedicineName,SupplierID,ExpiryDate,Quantity,Price) " +
                                          "VALUES (@id,@name,@sid,@date,@quantity,@price)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("sid", sid);
                        command.Parameters.AddWithValue("date", date);
                        command.Parameters.AddWithValue("quantity", quantity);
                        command.Parameters.AddWithValue("price", price);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Medicines saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn19_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtMedID.Text;
            string name = TxtMedName.Text;
            string sid = TxtSupID.Text;
            string date = TxtMedEDate.Text;
            string quantity = TxtMedQuantity.Text;
            string price = TxtMedPrice.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Medicines SET MedicineName = @name, SupID = @sid, ExpiryDate = @date, Quantity = @quantity, Price = @price " +
                                  "WHERE MedicineID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("sid", sid);
                        command.Parameters.AddWithValue("date", date);
                        command.Parameters.AddWithValue("quantity", quantity);
                        command.Parameters.AddWithValue("price", price);
                        command.Parameters.AddWithValue("id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Medicines updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadMedicineData();
                        }
                        else
                        {
                            MessageBox.Show("No medicines found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn20_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtMedID.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Medicines WHERE MedicineID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        // Confirm deletion
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this medicine?",
                                                                  "Confirm Deletion",
                                                                  MessageBoxButton.YesNo,
                                                                  MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Medicine deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadMedicineData();
                            }
                            else
                            {
                                MessageBox.Show("No medicine found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void Btn21_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMedicineData();
        }

        private void LoadMedicineData()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;



                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Medicines";



                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        MedicinesDataGrid.ItemsSource = dataTable.DefaultView;
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