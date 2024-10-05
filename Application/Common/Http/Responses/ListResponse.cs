using System.ComponentModel.DataAnnotations;

namespace Application.Common.Http.Responses;

public class ListResponse<T>
{
    private readonly IList<T> _items = [];

    [Required]
    public IList<T> Items
    {
        get => _items;
        init => _items = value;
    }
}