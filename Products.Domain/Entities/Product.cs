using System.ComponentModel.DataAnnotations;

namespace Products.Domain.Entities
{
    public class Product
    {

            [Key]
            public string ProductId { get; set; }
            [Required]
            public string ProductName { get; set; }
            [Required]
            public string ProductDescription { get; set; }
            [Required]
            public int StockAvailable { get; set; }

        }
    }
