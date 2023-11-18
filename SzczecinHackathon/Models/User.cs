using System.Text.Json.Serialization;

namespace SzczecinHackathon.Models
{
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string[] Friends { get; set; } = new string[0];
        public string[] FriendsRequests { get; set; } = new string[0];
        public string[] SendedInvitations { get; set; } = new string[0];

        [JsonIgnore]
        public ICollection<HappeningUser> HappeningUsers { get; set; } = new List<HappeningUser>();
    }
}
