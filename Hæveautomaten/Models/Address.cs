namespace Hæveautomaten.Models
{
    public class Address
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public Address(string houseNumber, string street, string city, string zipCode)
        {
            HouseNumber = houseNumber;
            Street = street;
            City = city;
            ZipCode = zipCode;
        }
    }
}