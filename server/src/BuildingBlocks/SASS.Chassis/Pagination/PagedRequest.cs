using System.Text.Json.Serialization;

namespace SASS.Chassis.Pagination;

public class PagedRequest
{
    public int Page { get; init; } = Constants.Core.Pagination.DefaultPageIndex;
    public int PageSize { get; init; } = Constants.Core.Pagination.DefaultPageSize;
    
    [JsonConverter(typeof(JsonStringEnumConverter<Order>))]
    public Order Order { get; init; } = Order.Desc;
}
