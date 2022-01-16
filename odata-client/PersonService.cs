using Newtonsoft.Json;
using odata_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace odata_client
{
    class PersonService
    {
        // This is V4 base URL
        private readonly string baseUrl = "https://services.odata.org/v4";
        HTTPClientHelper client;

        public PersonService(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                this.baseUrl = this.baseUrl + "/" + key;
            }
            this.baseUrl += "/TripPinServiceRW";
            this.client = new HTTPClientHelper(baseUrl);

        }

        // Helper function to display Header Grid
        static void ShowHeader()
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.Write("UserName".PadRight(15));
            Console.Write(" \t" + "FirstName".PadRight(8));
            Console.Write(" \t" + "LastName".PadRight(8));
            Console.Write(" \t" + "Gender".PadRight(5));
            Console.Write(" \t" + "Address".PadRight(15));
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        // Helper function to display Person Record
        static void ShowPerson(Person person)
        {
            Console.Write(person.UserName.PadRight(15));
            Console.Write(" \t" + person.FirstName.PadRight(8));
            Console.Write(" \t" + person.LastName.PadRight(8));
            Console.Write(" \t" + person.Gender.PadRight(5));
            if (person.AddressInfo.Count > 0)
            {
                Console.Write(" \t" + person.AddressInfo.ElementAt(0).Address.PadRight(15));
            }
            Console.WriteLine();
        }

        // Get a list of people
        public async Task ListPersonAsync()
        {
            Console.Clear();
            var result = await client.GetAsync<PersonResponse>(baseUrl + "/People");
            // TODO give option to load next page as well until result.Next is null
            if (result != null)
            {
                ShowHeader();
                foreach (var people in result.PersonList)
                {
                    ShowPerson(people);
                }
            }
        }

        // Filter/Search record based on few params
        public async Task FilterPersonAsync()
        {
            Console.Clear();
            Console.WriteLine("Please select field to filter");
            Console.WriteLine("1) UserName (default)");
            Console.WriteLine("2) FirstName");
            Console.WriteLine("3) LastName");
            Console.WriteLine("4) MiddleName");
            Console.WriteLine("5) Gender");
            Console.WriteLine("6) Emails");

            // TODO Add more parameters to filter 

            string FilterBy;
            switch (Console.ReadLine())
            {
                case "2":
                    FilterBy = "FirstName";
                    break;
                case "3":
                    FilterBy = "LastName";
                    break;
                case "4":
                    FilterBy = "MiddleName";
                    break;
                case "5":
                    FilterBy = "Gender";
                    break;
                case "6":
                    FilterBy = "Emails";
                    break;
                default:
                    FilterBy = "UserName";
                    break;
            }

            Console.Write($"Please enter value to filter by : {FilterBy} :");
            string Value = Console.ReadLine();
            var result = await client.GetAsync<PersonResponse>($"{baseUrl}/People?$filter=contains({FilterBy},'{Value}')");

            if (result != null)
            {
                ShowHeader();
                foreach (var people in result.PersonList)
                {
                    ShowPerson(people);
                }
            }
        }

        // Get a detail for given ID
        public async Task GetPersonDetailAsync()
        {
            Console.Clear();
            Console.Write("Please enter User Name to get detail :");
            string userName = Console.ReadLine();
            var result = await client.GetAsync<Person>($"{baseUrl}/People('{userName}')");
            if (result != null)
            {
                ShowHeader();
                ShowPerson(result);
            }
        }

        // Add Person
        public async Task AddPersonAsync()
        {
            Console.Clear();
            Console.Write("Please enter User Name to add :");
            string userName = Console.ReadLine();

            var person = ReadPersonFromConsole();
            person.UserName = userName;

            HttpContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");

            var result = await client.PostAsync<Person>($"{baseUrl}/People", content);
            if (result != null)
            {
                ShowHeader();
                ShowPerson(result);
            }
        }

        // Update Person
        public async Task UpdatePersonAsync()
        {
            Console.Clear();
            Console.Write("Please enter UserName to update detail :");
            string userName = Console.ReadLine();

            var person = ReadPersonFromConsole();

            HttpContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");

            var result = await client.PutAsync<Person>($"{baseUrl}/People('{userName}')", content);
            if (result != null)
            {
                ShowHeader();
                ShowPerson(result);
            }
        }

        private Person ReadPersonFromConsole()
        {
            Person person = new Person();

            Console.Write("Enter First Name :");
            person.FirstName = Console.ReadLine();

            Console.Write("Enter LastName :");
            person.LastName = Console.ReadLine();

            Console.Write("Enter Gender (Male/Female):");
            person.Gender = Console.ReadLine();

            Console.Write("Enter Emails(comma seperated for mulriple) :");
            person.Emails = Console.ReadLine().Split(",").ToList();

            Console.Write("Enter Address :");
            var address = Console.ReadLine();

            Console.Write("Enter Country Region :");
            var countryRegion = Console.ReadLine();

            Console.Write("Enter City Name :");
            var cityName = Console.ReadLine();

            Console.Write("Enter region :");
            var region = Console.ReadLine();


            AddressInfo addressInfo = new AddressInfo()
            {
                Address = address,
                City = new City()
                {
                    Name = cityName,
                    CountryRegion = countryRegion,
                    Region = region
                }
            };

            person.AddressInfo = new List<AddressInfo>() { addressInfo };

            return person;
        }

    }
}
