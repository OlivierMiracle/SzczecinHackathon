namespace SzczecinHackathon.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime Birthday { get; set; } = DateTime.MinValue;
    }
}
