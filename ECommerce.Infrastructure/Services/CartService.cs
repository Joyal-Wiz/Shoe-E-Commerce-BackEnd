using ECommerce.Application.DTO.Cart;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(Guid userId, Guid productId)
        {
            // Check product exists
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new NotFoundException(ErrorMessages.notfound);

            //  Get or create cart
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Check if item already exists
            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };

                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
        }
        //get item in cart 
        public async Task<List<CartItemResponseDto>> GetCartAsync(Guid userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
                return new List<CartItemResponseDto>();

            var result = cart.CartItems.Select(ci => new CartItemResponseDto
            {
                CartItemId = ci.Id,     
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                ImageUrl = ci.Product.ImageUrl,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList();


            return result;
        }
        //update item in cart
        public async Task UpdateCartItemAsync(Guid userId, Guid cartItemId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new NotFoundException(ErrorMessages.notfound);

            var cartItem = cart.CartItems
                .FirstOrDefault(ci => ci.Id == cartItemId);

            if (cartItem == null)
                throw new NotFoundException(ErrorMessages.itemnotfound);

            if (quantity <= 0)
            {
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
            }

            await _context.SaveChangesAsync();
        }
        //delete item in cart
        public async Task DeleteCartItemAsync(Guid userId, Guid cartItemId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new NotFoundException(ErrorMessages.notfound);

            var cartItem = cart.CartItems
                .FirstOrDefault(ci => ci.Id == cartItemId);

            if (cartItem == null)
                throw new NotFoundException(ErrorMessages.itemnotfound);

            _context.CartItems.Remove(cartItem);

            await _context.SaveChangesAsync();
        }


    }
}
