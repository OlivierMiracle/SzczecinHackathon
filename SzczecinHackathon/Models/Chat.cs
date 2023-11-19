namespace SzczecinHackathon.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();

    }
}
