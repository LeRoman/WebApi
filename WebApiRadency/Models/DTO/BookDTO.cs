using System.Text.Json.Serialization;

namespace WebApiRadency.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Author { get; set; }
        
        public decimal Rating { get; set; }

        [JsonPropertyName("reviewsNumber")]
        public int Reviews { get; set; }
    }
}
