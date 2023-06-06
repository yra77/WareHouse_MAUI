

using System;


namespace WareHouse_MAUI.Models
{
    public class HumanBaseModel : BaseModel
    {

        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public DateTime DateOfRegistration { get; set; } = DateTime.UtcNow;
    }
}
