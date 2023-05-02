using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Domain.SectionOne.Entity
{
    public class PurchasingData
    {
        public long Id { get; set; }
        public string Purchase_stage { get; set; }
        public int Num_Of_Applications { get; set; }
        public string Method_of_purchasing { get; set; }
        public DateTimeOffset Start_data { get; set; }
        public DateTimeOffset End_data { get; set; }
        public long NMCK { get; set; }
        public int Federal_law { get; set; }
        public int Num_of_ships { get; set; }
        public string Purchase_object { get; set; }
        public ICollection<Customer> Customers { get; set; }


    }
}
