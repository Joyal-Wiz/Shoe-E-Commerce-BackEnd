using ECommerce.Application.DTO.Wishlist;
using System;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface IWishlistService
    {
        Task AddToWishlistAsync(Guid userId, Guid productId);
        Task<List<WishlistItemResponseDto>> GetWishlistAsync(Guid userId);
        Task DeleteWishlistItemAsync(Guid userId, Guid wishlistItemId);


    }
}
