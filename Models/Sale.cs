using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AuctionsAPI.Models.Enumerations;

namespace AuctionsAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime FinishedDt { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public AuctionsStatus Status { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}
