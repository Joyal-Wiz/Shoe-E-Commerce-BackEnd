using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTO.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
