namespace ShipBase.Domain.Entity
{
    public class Profile
    {
        public long Id { get; set; }
        
        public byte Age { get; set; }
        
        public string Address { get; set; }
        
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNum { get; set; }
        public User User { get; set; }
    }
}