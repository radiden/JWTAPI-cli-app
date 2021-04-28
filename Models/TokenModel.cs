using System;
using System.Text.Json.Serialization;

namespace jwtapi_app.Models
{
    public class TokenDetails
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("tokenExpirationDate")]
        public DateTime TokenExpirationDate { get; set; }
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("refreshExpirationDate")]
        public DateTime RefreshExpirationDate { get; set; }
    }
}