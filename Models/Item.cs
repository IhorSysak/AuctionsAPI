using System.ComponentModel.DataAnnotations;

namespace AuctionsAPI.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public Sale Sale { get; set; }
    }
}
