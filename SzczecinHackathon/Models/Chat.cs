namespace SzczecinHackathon.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string[] UserIds { get; set; }
        public List<Message> Messages { get; set; }
    }
}
