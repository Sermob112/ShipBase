using System.ComponentModel.DataAnnotations;

namespace ShipBase.Domain.SectionOne.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Укажите возраст")]
        [Range(0, 150, ErrorMessage = "Диапазон возраста должен быть от 0 до 150")]
        public byte Age { get; set; }
        
        [Required(ErrorMessage = "Укажите адрес")]
        [MinLength(5, ErrorMessage = "Минимальная длина должна быть больше 5 символов")] 
        [MaxLength(200, ErrorMessage = "Максимальная длина должна быть меньше 200 символов")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(200, ErrorMessage = "Максимальная длина должна быть меньше 200 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Укажите Фамилию")]
        [MaxLength(200, ErrorMessage = "Максимальная длина должна быть меньше 200 символов")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Укажите отчество")]
        [MaxLength(200, ErrorMessage = "Максимальная длина должна быть меньше 200 символов")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Укажите Телефон")]
        [MaxLength(200, ErrorMessage = "Максимальная длина должна быть меньше 200 символов")]
        public string PhoneNum { get; set; }
    }
}