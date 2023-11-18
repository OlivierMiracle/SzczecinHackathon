namespace SzczecinHackathon.Models
{
    public class HappeningUser
    {
        public int HappeningId { get; set; }
        public Happening Happening { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
