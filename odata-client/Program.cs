using System;
using System.Threading.Tasks;
using odata_client.Models;

namespace odata_client
{
    class Program
    {
        static PersonService personService;
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // This is for Data modification, we need to get key from https://www.odata.org/odata-services/service-usages/request-key-tutorial/
            Console.Write("Enter key if you have any for Trippin service (default empty key) :");
            var key = Console.ReadLine();
            personService = new PersonService(key);
            
            // Run program until user exits forcefully
            while (await PrepareMenuAsync())
            {
            }
        }

        static async Task<bool> PrepareMenuAsync()
        {
            Console.WriteLine("\r\n========================================================================");
            Console.WriteLine("OData App - Choose an option:");
            Console.WriteLine("\r\n========================================================================");
            Console.WriteLine("1) List people");
            Console.WriteLine("2) Allow searching/filtering people");
            Console.WriteLine("3) Show details on a specific Person");
            Console.WriteLine("4) Add new person");
            Console.WriteLine("5) Update existing person");
            Console.Write("\r\nSelect an option:");

            // TODO add service with SOLID
            // For any other option close app
            switch (Console.ReadLine())
            {
                case "1":
                    await personService.ListPersonAsync();
                    return true;
                case "2":
                    await personService.FilterPersonAsync();
                    return true;
                case "3":
                    await personService.GetPersonDetailAsync();
                    return true;
                case "4":
                    await personService.AddPersonAsync();
                    return true;
                case "5":
                    await personService.UpdatePersonAsync();
                    return true;
                default:
                    return false;
            }
        }
    }
}
