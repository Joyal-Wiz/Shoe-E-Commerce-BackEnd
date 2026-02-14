using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTO.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
