using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SzczecinHackathon.Models
{
    public class Happening
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ImgPath { get; set; } = string.Empty;
        public bool IsHidden { get; set; } = false;

        public ICollection<HappeningUser> HappeningUsers { get; set; } = new List<HappeningUser>();
    }
}
