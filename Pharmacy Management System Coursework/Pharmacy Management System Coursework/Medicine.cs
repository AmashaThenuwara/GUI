using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System_Coursework
{
    public class Medicine
    {
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string SupplierID { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string ExpiryDate { get; set; }
    }
}
