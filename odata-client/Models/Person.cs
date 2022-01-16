using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace odata_client.Models
{

    public class PersonResponse
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }

        [JsonProperty("@odata.nextLink")]
        public string Next { get; set; }

        [JsonProperty("value")]
        public List<Person> PersonList { get; set; }
    }

    public class Person
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public List<string> Emails { get; set; }

        public List<AddressInfo> AddressInfo { get; set; }

    }

    public class AddressInfo
    {
        public string Address { get; set; }

        public City City { get; set; }
    }

    public class City
    {
        public string CountryRegion { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
    }
}
