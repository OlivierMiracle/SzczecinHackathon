namespace SzczecinHackathon.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
