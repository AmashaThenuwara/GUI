using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System_Coursework
{
    public class Prescriptions
    {
        public string PrescriptionID { get; set; }
        public string PatientID { get; set; }
        public string MedicineID { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string PrescriptionDate { get; set; }
    }
}
