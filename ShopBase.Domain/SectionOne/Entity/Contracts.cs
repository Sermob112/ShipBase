using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Domain.Entity
{
    public class Contracts
    {
        public long Id { get; set; }
        public DateTime Data_of_conclusion { get; set; }
        public PurchasingData Purchace_id { get; set; }
    }
}
