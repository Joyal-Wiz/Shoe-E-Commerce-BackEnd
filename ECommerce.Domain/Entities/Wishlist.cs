using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Wishlist
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<WishlistItem> WishlistItems { get; set; }
            = new List<WishlistItem>();
    }
}
