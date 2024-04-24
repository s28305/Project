using System.ComponentModel.DataAnnotations;

namespace Tutorial7
{
    public class ProductDto
    {
        [Required(ErrorMessage = "IdProduct is required.")]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "IdWarehouse is required.")]
        public int IdWarehouse { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; }
    }
}