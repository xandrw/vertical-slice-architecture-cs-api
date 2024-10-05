using System.ComponentModel.DataAnnotations;

namespace Application.Common.Http.Responses;

public class PaginatedListResponse<T>
{
    private readonly IList<T> _items = [];

    [Required]
    public IList<T> Items
    {
        get => _items;
        init => _items = value;
    }
    
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}