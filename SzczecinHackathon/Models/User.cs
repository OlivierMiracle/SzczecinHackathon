namespace SzczecinHackathon.Models
{
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string[]? Friends { get; set; }
        public string[]? FriendsRequests { get; set; }
    }
}
