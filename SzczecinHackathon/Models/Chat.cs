namespace SzczecinHackathon.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<ChatUser> ChatUsers { get; set; }
        public List<Message> Messages { get; set; }
    }
}
