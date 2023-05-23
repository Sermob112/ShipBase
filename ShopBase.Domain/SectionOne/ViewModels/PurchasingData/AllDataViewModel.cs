using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Domain.SectionOne.ViewModels.PurchasingData
{
    public class AllDataViewModel
    {
        [Required(ErrorMessage = "Укажите номер закупки")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Укажите этап закупки")]
        public string Purchase_stage { get; set; }
        [Required(ErrorMessage = "Укажите количество заявок")]
        public int Num_Of_Applications { get; set; }
        [Required(ErrorMessage = "Укажите cпособ осуществления закупки")]
        public string Method_of_purchasing { get; set; }
        [Required(ErrorMessage = "Укажите дату начала осуществления закупки")]
        public DateTimeOffset Start_data { get; set; }
        [Required(ErrorMessage = "Укажите дату оканчания осуществления закупки")]
        public DateTimeOffset End_data { get; set; }
        [Required(ErrorMessage = "Укажите Н(М)ЦК")]
        public long NMCK { get; set; }
        [Required(ErrorMessage = "Укажите номер Федерального закона")]
        public int Federal_law { get; set; }
        [Required(ErrorMessage = "Укажите количество кораблей")]
        public int Num_of_ships { get; set; }
        [Required(ErrorMessage = "Укажите название объекта")]
        public string Purchase_object { get; set; }
        [Required(ErrorMessage = "Укажите название объекта")]
        public string Name_of_organization { get; set; }
    }
}
