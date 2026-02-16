using System;

namespace ECommerce.Domain.Entities
{
    public class WishlistItem
    {
        public Guid Id { get; set; }

        public Guid WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
