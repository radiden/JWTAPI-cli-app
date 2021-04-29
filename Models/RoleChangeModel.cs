namespace jwtapi_app.Models
{
    public class RoleChange
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public int Action { get; set; }
    }
}