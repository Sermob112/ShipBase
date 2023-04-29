using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Domain.SectionOne.ViewModels.Customer
{
    public class СustomerViewModel
    {

      

        [Required(ErrorMessage = "Укажите номер исполнителя")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Укажите название исполнителя")]
        public string Name_of_organization { get; set; }
        [Required(ErrorMessage = "Укажите номер ОГРН")]
        public long OGRN { get; set; }
        [Required(ErrorMessage = "Укажите номер ИНН")]
        public long INN { get; set; }
        [Required(ErrorMessage = "Укажите номер КПП")]
        public long KPP { get; set; }
        public long purchasing_id { get; set; }
    }
}
