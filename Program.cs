using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using jwtapi_app.Models;

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

            var result = await client.GetAsync(url + GetInput());
            Console.WriteLine(await result.Content.ReadAsStringAsync());
        }

        private static string GetInput()
        {
            while (true)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1 - GET /HelloWorld");
                Console.WriteLine("2 - GET /HelloWorld/ServerInfo");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        return "HelloWorld";
                    case "2":
                        return "HelloWorld/ServerInfo";
                    default:
                        Console.WriteLine("Please input a valid number!");
                        continue;
                }
            }
        }
    }
}
