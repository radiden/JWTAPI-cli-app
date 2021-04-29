using System.Text.Json.Serialization;

namespace jwtapi_app
{
    public class User
    {
        [JsonPropertyName("UserName")]
        public string Username { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}