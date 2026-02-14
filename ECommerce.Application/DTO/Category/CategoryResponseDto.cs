using System;

namespace ECommerce.Application.DTO.Category
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
