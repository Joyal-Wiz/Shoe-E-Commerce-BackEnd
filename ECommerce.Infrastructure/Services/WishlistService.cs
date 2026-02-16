using ECommerce.Application.DTO.Wishlist;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _context;

        public WishlistService(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddToWishlistAsync(Guid userId, Guid productId)
        {
            // Check product exists
            var productExists = await _context.Products
                .AnyAsync(p => p.Id == productId);

            if (!productExists)
                throw new NotFoundException(ErrorMessages.Productnotfound);

            // Get or create wishlist
            var wishlist = await _context.Wishlists
                .Include(w => w.WishlistItems)
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };

                _context.Wishlists.Add(wishlist);
                await _context.SaveChangesAsync();
            }

            // Check duplicate
            var alreadyExists = wishlist.WishlistItems
                .Any(wi => wi.ProductId == productId);

            if (alreadyExists)
                throw new BadRequestException(ErrorMessages.Productalreadyinwishlist);

            // Add item
            var wishlistItem = new WishlistItem
            {
                Id = Guid.NewGuid(),
                WishlistId = wishlist.Id,
                ProductId = productId
            };

            _context.WishlistItems.Add(wishlistItem);

            await _context.SaveChangesAsync();
        }
        //wishlist
        public async Task<List<WishlistItemResponseDto>> GetWishlistAsync(Guid userId)
        {
            var wishlist = await _context.Wishlists
                .Include(w => w.WishlistItems)
                    .ThenInclude(wi => wi.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null || !wishlist.WishlistItems.Any())
                return new List<WishlistItemResponseDto>();

            var result = wishlist.WishlistItems.Select(wi => new WishlistItemResponseDto
            {
                WishlistItemId = wi.Id, 
                ProductId = wi.ProductId,
                ProductName = wi.Product.Name,
                ImageUrl = wi.Product.ImageUrl,
                Price = wi.Product.Price
            }).ToList();


            return result;
        }
        //delete item in wishlist
        public async Task DeleteWishlistItemAsync(Guid userId, Guid wishlistItemId)
        {
            var wishlist = await _context.Wishlists
                .Include(w => w.WishlistItems)
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null)
                throw new NotFoundException("Wishlist not found");

            var item = wishlist.WishlistItems
                .FirstOrDefault(wi => wi.Id == wishlistItemId);

            if (item == null)
                throw new NotFoundException("Wishlist item not found");

            _context.WishlistItems.Remove(item);

            await _context.SaveChangesAsync();
        }

    }
}
