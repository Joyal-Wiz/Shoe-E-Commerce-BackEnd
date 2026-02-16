using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
            = new List<CartItem>();
    }
}
