using System;

namespace ECommerce.Application.DTO.Wishlist
{
    public class WishlistItemResponseDto
    {
        public Guid WishlistItemId { get; set; }   
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
