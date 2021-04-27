using System;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using jwtapi_app.Models;

namespace jwtapi_app
{
    class TokenUtils
    {
        public static bool IsTokenValid()
        {
            if (!File.Exists("token.json"))
            {
                return false;
            }

            var jsonString = File.ReadAllText("token.json");
            TokenDetails deserializedToken;

            try
            {
                deserializedToken = JsonSerializer.Deserialize<TokenDetails>(jsonString);
            }
            catch (System.Text.Json.JsonException)
            {
                Console.WriteLine("Invalid JSON in token! Recreating...");
                return false;
            }

            if (DateTime.Compare(deserializedToken.TokenExpirationDate, DateTime.Now) < 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>True if token was successfully retrieved</summary>
        public async static Task<bool> GetToken()
        {
            if (File.Exists("token.json"))
            {
                File.Delete("token.json");
            }

            using (var file = File.Open("token.json", FileMode.Create))
            {
                using (var client = new HttpClient())
                {
                    var fileBuf = await client.GetByteArrayAsync("http://localhost:5000/Authorization/GetToken");
                    file.Write(fileBuf);
                }
            }
            return true;
        }
        public async static Task<TokenDetails> GetTokenDetails()
        {
            using (var stream = File.OpenRead("token.json"))
            {
                return await JsonSerializer.DeserializeAsync<TokenDetails>(stream);           
            }
        }
    }
}