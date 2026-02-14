using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
