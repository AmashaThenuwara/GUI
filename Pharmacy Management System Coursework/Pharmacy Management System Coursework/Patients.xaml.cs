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
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Window
    {
        public Patients()
        {
            InitializeComponent();
        }

        private void Btn10_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtPatID.Text;
            string name = TxtPatName.Text;
            string age = TxtPatAge.Text;
            string number = TxtPatCNumber.Text;
            string address = TxtPatAddress.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Patients(PatientID,Name,Age,ContactNumber,Address)" +
                                          "VALUES (@id,@name,@age,@number,@address)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("age", age);
                        command.Parameters.AddWithValue("number", number);
                        command.Parameters.AddWithValue("address", address);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Patients saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadPatientData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn11_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtPatID.Text;
            string name = TxtPatName.Text;
            string age = TxtPatAge.Text;
            string number = TxtPatCNumber.Text;
            string address = TxtPatAddress.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Patients SET Name = @name, Age = @age, ContactNumber = @number, Address = @address" +
                                  "WHERE PatientID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("age", age);
                        command.Parameters.AddWithValue("number", number);
                        command.Parameters.AddWithValue("address", address);
                        command.Parameters.AddWithValue("id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadPatientData();
                        }
                        else
                        {
                            MessageBox.Show("No patients found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn12_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtPatID.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Patients WHERE PatientID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        // Confirm deletion
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this patient?",
                                                                  "Confirm Deletion",
                                                                  MessageBoxButton.YesNo,
                                                                  MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Patient deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadPatientData();
                            }
                            else
                            {
                                MessageBox.Show("No patient found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void Btn13_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPatientData();
        }

        private void LoadPatientData()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;



                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Patients";



                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        PatientsDataGrid.ItemsSource = dataTable.DefaultView;
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
