using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace jwtapi_app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/";
            const string username = "rai";
            const string password = "password";
            switch (TokenUtils.IsTokenValid())
            {
                case 1:
                    Console.WriteLine("Refreshing token...");
                    await TokenUtils.RefreshToken(url);
                    break;
                case 2:
                    Console.WriteLine("Getting new token...");
                    await TokenUtils.GetToken(username, password, url);
                    break;
            }

            var tokenDetails = await TokenUtils.GetTokenDetails();

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenDetails.Token);

            var response = await GetInput(url, client);
            if (String.IsNullOrEmpty(response))
            {
                Console.WriteLine("Nothing was returned. You probably don't have permission to do that.");
            }
            else
            {
                Console.WriteLine(response);   
            }
        }

        private static async Task<string> GetInput(string url, HttpClient client)
        {
            string[] options =
            {
                "1 - GET /HelloWorld", "2 - GET /HelloWorld/ServerInfo",
                "3 - List all products", "4 - Add product",
                "5 - List all clients", "6 - Add client",
                "7 - List all transactions", "8 - Add transaction",
                "9 - Register user", "10 - Add/Delete role from user"
            };
            while (true)
            {
                Console.WriteLine("What would you like to do?");
                foreach (var option in options)
                {
                    Console.WriteLine(option);
                }
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        return await RequestUtils.RequestGet(url+"HelloWorld", client);
                    case "2":
                        return await RequestUtils.RequestGet(url+"HelloWorld/ServerInfo", client);
                    case "3":
                        return await RequestUtils.RequestGet(url+"Product/List", client);
                    case "4":
                        return await RequestUtils.AddProduct(url, client);
                    case "5":
                        return await RequestUtils.RequestGet(url+"Client/List", client);
                    case "6":
                        return await RequestUtils.AddClient(url, client);
                    case "7":
                        return await RequestUtils.RequestGet(url+"Transaction/List", client);
                    case "8":
                        return await RequestUtils.AddTransaction(url, client);
                    case "9":
                        return await RequestUtils.RegisterUser(url, client);
                    case "10":
                        return await RequestUtils.ChangeRole(url, client);
                    default:
                        Console.WriteLine("Please input a valid number!");
                        continue;
                }
            }
        }
    }
}
