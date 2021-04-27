using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace jwtapi_app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (!TokenUtils.IsTokenValid())
            {
                Console.WriteLine("Missing valid token! Retrieving new token...");
                await TokenUtils.GetToken();
            }

            // TODO: all api calling things
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("http://localhost:5000/WeatherForecast");
                Console.WriteLine(await result.Content.ReadAsStringAsync());
            }
        }   
    }
}
