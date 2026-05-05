using System.Text.Json.Serialization;
namespace SASS.Chassis.Pagination;

public sealed class CursorPagedRequest
{
    public string? Cursor { get; init; }
    public int Limit { get; init; } = Constants.Core.Pagination.DefaultCursorLimit;
    
    [JsonConverter(typeof(JsonStringEnumConverter<Order>))]
    public Order Order { get; init; } = Order.Desc;
}
