using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Domain.SectionOne.ViewModels.Purch
{
    public class PurchViewModel
    {
        public long Id { set; get; }
        public string Federal_law { set; get; }
        public string Method_of_purchasing { set; get; }
        public string Purchase_object { set; get; }
        public string Purchase_stage { set; get; }
    }
}
