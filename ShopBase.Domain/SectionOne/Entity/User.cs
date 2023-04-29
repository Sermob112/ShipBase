using ShipBase.Domain.SectionOne.Enum;

namespace ShipBase.Domain.SectionOne.Entity
{
    public class User
    {
        public long Id { get; set; }
        
        public string Password { get; set; }

        public string Login { get; set; }

        public Role Role { get; set; }
        
        public Profile Profile { get; set; }
        
    }
}