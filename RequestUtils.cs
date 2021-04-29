using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using jwtapi_app.Models;
using jwtapi.Models;

namespace jwtapi_app
{
    public class RequestUtils
    {
        public static async Task<string> RequestGet(string url, HttpClient client)
        {
            var result = await client.GetAsync(url);
            return await result.Content.ReadAsStringAsync();
        }
        
        public static async Task<string> ChangeRole(string url, HttpClient client)
        {
            var info = new RoleChange();
            var choice = "";
            while (choice.ToLower() != "add" && choice.ToLower() != "delete")
            {
                Console.WriteLine("Action? (Add/Delete)");
                choice = Console.ReadLine();
            }

            switch (choice.ToLower())
            {
                case "add":
                    info.Action = (int)RoleAction.Add;
                    break;
                case "delete":
                    info.Action = (int)RoleAction.Delete;
                    break;
                default:
                    throw new Exception("something is very wrong");
            }
            Console.WriteLine("User?");
            info.Username = Console.ReadLine();
            Console.WriteLine("Role?");
            info.Role = Console.ReadLine();
            var response = await client.PostAsJsonAsync(url + "Authorization/ChangeRole", info);
            
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> AddProduct(string url, HttpClient client)
        {
            var info = new Product();
            Console.WriteLine("Name?");
            info.Name = Console.ReadLine();
            
            var input = "";
            var price = 0.0;
            while (!Double.TryParse(input, out price))
            {
                Console.WriteLine("Price?");
                input = Console.ReadLine();
            }

            info.Price = price;
            
            var response = await client.PostAsJsonAsync(url + "Product/Add", info);
            
            return await response.Content.ReadAsStringAsync();
        }
        
        public static async Task<string> AddClient(string url, HttpClient client)
        {
            var info = new Client();
            Console.WriteLine("Name?");
            info.Name = Console.ReadLine();
            Console.WriteLine("Surname?");
            info.Surname = Console.ReadLine();
            var response = await client.PostAsJsonAsync(url + "Client/Add", info);
            
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> RegisterUser(string url, HttpClient client)
        {
            var info = new Register();
            Console.WriteLine("Username?");
            info.UserName = Console.ReadLine();

            var password = "";
            while (password.Length < 8)
            {
                Console.WriteLine("Password? (at least 8 chars)");
                password = Console.ReadLine();
            }

            info.Password = password;
            
            Console.WriteLine("Full name?");
            info.FullName = Console.ReadLine();
            var response = await client.PostAsJsonAsync(url + "Authorization/Register", info);

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> AddTransaction(string url, HttpClient client)
        {
            var info = new TransactionProd();

            var pidchoice = "";
            var pid = 0;
            while (!int.TryParse(pidchoice, out pid) && pid < 1)
            {
                Console.WriteLine("Product ID?");
                pidchoice = Console.ReadLine();
            }
            info.ProductId = pid;
            
            var cidchoice = "";
            var cid = 0;
            while (!int.TryParse(cidchoice, out cid) && cid < 1)
            {
                Console.WriteLine("Client ID?");
                cidchoice = Console.ReadLine();
            }

            info.ClientId = cid;
            
            info.Time = DateTime.Now;

            var response = await client.PostAsJsonAsync(url + "Transaction/Add", info);

            return await response.Content.ReadAsStringAsync();
        }
        
        private enum RoleAction : int
        {
            Add = 0,
            Delete = 1
        }
    }
}