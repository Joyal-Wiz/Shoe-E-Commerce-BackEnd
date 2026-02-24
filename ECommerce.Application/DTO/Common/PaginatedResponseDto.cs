using System.Collections.Generic;

namespace ECommerce.Application.DTO.Common
{
    public class PaginatedResponseDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
