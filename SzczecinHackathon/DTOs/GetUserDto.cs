namespace SzczecinHackathon.DTOs
{
    public class GetUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime Birthday { get; set; }  
    }
}
