using System;
using System.Collections.Generic;
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

namespace Pharmacy_Management_System_Coursework
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Employees employees = new Employees();
            this.Hide();
            employees.Show();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Patients patients = new Patients();
            this.Hide();
            patients.Show();
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            Medicines medicines = new Medicines();
            this.Hide();
            medicines.Show();
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            Suppliers supliers = new Suppliers();
            this.Hide();
            supliers.Show();
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            Prescription prescription = new Prescription();
            this.Hide();
            prescription.Show();
        }
    }
}
