using System;

namespace jwtapi_app.Models
{
    public class TokenDetails
    {
        public string Token { get; set; }
        public DateTime TokenExpirationDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshExpirationDate { get; set; }
    }
}
