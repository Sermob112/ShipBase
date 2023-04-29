using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Domain.SectionOne.Entity
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name_of_organization { get; set; }
        public long OGRN { get; set; }
        public long INN { get; set; }
        public long KPP { get; set; }
        public long purchasing_id { get; set; } // внешний ключ
        public PurchasingData PurchasingData { get; set; }


    }
}
