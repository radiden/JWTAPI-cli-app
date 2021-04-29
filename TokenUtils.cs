using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using jwtapi_app.Models;

namespace jwtapi_app
{
    class TokenUtils
    {
        public static int IsTokenValid()
        {
            if (!File.Exists("token.json"))
            {
                return 2;
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
                return 2;
            }

            if (deserializedToken.TokenExpirationDate >= DateTime.Now)
            {
                return 0;
            }
            return deserializedToken.RefreshExpirationDate >= DateTime.UtcNow ? 1 : 2;
        }

        public static async Task<bool> RefreshToken(string url)
        {
            await using var file = File.Open("token.json", FileMode.OpenOrCreate);
            var serializedToken = await JsonSerializer.DeserializeAsync<TokenDetails>(file);
            using var client = new HttpClient();

            var response = await client.PostAsJsonAsync(url + "Authorization/RefreshToken", serializedToken);

            file.SetLength(0);
            
            await response.Content.CopyToAsync(file);
            return true;
        }
        /// <summary>True if token was successfully retrieved</summary>
        public static async Task<bool> GetToken(string username, string password, string url)
        {
            if (File.Exists("token.json"))
            {
                File.Delete("token.json");
            }

            await using var file = File.Open("token.json", FileMode.Create);
            using var client = new HttpClient();

            var postData = new User
            {
                Username = username,
                Password = password
            };

            var response = await client.PostAsJsonAsync(url + "Authorization/Login", postData);
            await response.Content.CopyToAsync(file);
            return true;
        }
        public static async Task<TokenDetails> GetTokenDetails()
        {
            using FileStream stream = File.OpenRead("token.json");
            return await JsonSerializer.DeserializeAsync<TokenDetails>(stream);
        }
    }
}