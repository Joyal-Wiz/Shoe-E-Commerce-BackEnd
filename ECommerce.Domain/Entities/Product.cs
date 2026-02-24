using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Foreign Key
        public Guid CategoryId { get; set; }

        // Navigation Property
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
