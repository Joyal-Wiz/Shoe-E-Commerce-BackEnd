using ECommerce.Application.DTO.Cart;
using System;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface ICartService
    {
        Task AddToCartAsync(Guid userId, Guid productId);
        Task<List<CartItemResponseDto>> GetCartAsync(Guid userId);
        Task UpdateCartItemAsync(Guid userId, Guid cartItemId, int quantity);
        Task DeleteCartItemAsync(Guid userId, Guid cartItemId);



    }
}
