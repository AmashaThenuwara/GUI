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
    /// Interaction logic for Prescription.xaml
    /// </summary>
    public partial class Prescription : Window
    {
        public Prescription()
        {
            InitializeComponent();
        }

        private void Btn22_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtPreID.Text;
            string mid = TxtMedID.Text;
            string pid = TxtPatID.Text;
            string quantity = TxtPreQuantity.Text;
            string price = TxtPrePrice.Text;
            string date = TxtPreDate.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Prescription (PrescriptionID,PatientID,MedicineID,Quantity,Price,PrescriptionDate) " +
                                          "VALUES (@id,@mid,@pid,@quantity,@price,@date)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("mid", mid);
                        command.Parameters.AddWithValue("pid", pid);
                        command.Parameters.AddWithValue("quantity", quantity);
                        command.Parameters.AddWithValue("price", price);
                        command.Parameters.AddWithValue("date", date);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Prescription saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn23_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string prescriptionID = TxtPreID.Text;
                string patientID = TxtPatID.Text;
                string medicineID = TxtMedID.Text;
                string quantity = TxtPreQuantity.Text;
                string price = TxtPrePrice.Text;
                string date = TxtPreDate.Text;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                string patientName = "";
                string medicineName = "";


                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    string patientQuery = "SELECT Name FROM Patients WHERE PatientID = @patientID";
                    using (MySqlCommand patientCmd = new MySqlCommand(patientQuery, connection))
                    {
                        patientCmd.Parameters.AddWithValue("@patientID", patientID);
                        object result = patientCmd.ExecuteScalar();
                        if (result != null)
                            patientName = result.ToString();
                        else
                            MessageBox.Show("Patient not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    string medicineQuery = "SELECT MedicineName FROM Medicines WHERE MedicineID = @medicineID";
                    using (MySqlCommand medicineCmd = new MySqlCommand(medicineQuery, connection))
                    {
                        medicineCmd.Parameters.AddWithValue("@medicineID", medicineID);
                        object result = medicineCmd.ExecuteScalar();
                        if (result != null)
                            medicineName = result.ToString();
                        else
                            MessageBox.Show("Medicine not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                string prescriptionDetails = $"Prescription ID: {prescriptionID}\n" +
                                             $"Patient ID: {patientID}\n" +
                                             $"Patient Name: {patientName}\n" +
                                             $"Medicine ID: {medicineID}\n" +
                                             $"Medicine Name: {medicineName}\n" +
                                             $"Quantity: {quantity}\n" +
                                             $"Price: {price}\n" +
                                             $"Date: {date}\n";

                string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Prescription_{prescriptionID}.txt");
                System.IO.File.WriteAllText(fileName, prescriptionDetails);

                MessageBox.Show("Prescription saved successfully on Desktop!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn24_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtPreID.Text;

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Prescriptions WHERE PrescriptionID = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);

                        // Confirm deletion
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this prescription?",
                                                                  "Confirm Deletion",
                                                                  MessageBoxButton.YesNo,
                                                                  MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Prescription deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadPrescriptionsData();
                            }
                            else
                            {
                                MessageBox.Show("No prescription found with the given ID.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void Btn25_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPrescriptionsData();
        }

        private void LoadPrescriptionsData()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["pharmacymanagementsystem"].ConnectionString;



                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Prescription";



                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        PrescriptionDataGrid.ItemsSource = dataTable.DefaultView;
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