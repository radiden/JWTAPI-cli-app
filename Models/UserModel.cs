using System.Text.Json.Serialization;

namespace jwtapi_app
{
    public class UserModel
    {
        [JsonPropertyName("UserName")]
        public string Username { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}