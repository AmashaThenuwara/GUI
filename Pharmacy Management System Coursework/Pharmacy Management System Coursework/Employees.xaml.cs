using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        public Employees()
        {
            InitializeComponent();
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtEmpID.Text;
            string name = TxtEmpName.Text;
            string number = TxtEmpCNumber.Text;
            string address = TxtEmpAddress.Text;
            string salary = TxtEmpSalary.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Employees (EmployeeID,Name,ContactNumber,Address,Salary) " +
                                          "VALUES (@id,@name,@number,@address,@salary)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("number", number);
                        command.Parameters.AddWithValue("address", address);
                        command.Parameters.AddWithValue("salary", salary);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Employees saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtEmpID.Text;
            string name = TxtEmpName.Text;
            string number = TxtEmpCNumber.Text;
            string address = TxtEmpAddress.Text;
            string salary = TxtEmpSalary.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Employees SET Name = @name, ContactNumber = @number, Address = @address, Salary = @salary " +
                                  "WHERE EmployeeID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("name", name);
                        command.Parameters.AddWithValue("number", number);
                        command.Parameters.AddWithValue("address", address);
                        command.Parameters.AddWithValue("salary", salary);
                        command.Parameters.AddWithValue("id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employees updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadEmployeeData();
                        }
                        else
                        {
                            MessageBox.Show("No employees found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtEmpID.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Employees WHERE EmployeeID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        // Confirm deletion
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this employee?",
                                                                  "Confirm Deletion",
                                                                  MessageBoxButton.YesNo,
                                                                  MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadEmployeeData();
                            }
                            else
                            {
                                MessageBox.Show("No employee found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;



                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Employees";



                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        EmployeesDataGrid.ItemsSource = dataTable.DefaultView;
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
