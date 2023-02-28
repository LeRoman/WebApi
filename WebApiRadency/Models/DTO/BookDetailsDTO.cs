using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WebApiRadency.Models.DTO
{
    public class BookDetailsDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Cover { get; set; }
        public string? Content { get; set; }
        public string? Genre { get; set; }

        public decimal Rating { get; set; }

        [JsonPropertyName("rewiews")]
        
        public List<ReviewOutputDTO> ReviewsList { get; set; }

    }
}
