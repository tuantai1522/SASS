namespace SASS.SharedKernel.Results;

public sealed class PagedResult<T>
{
    public PagedResult(IReadOnlyList<T> items, int page, int pageSize, long totalItems)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (long)Math.Ceiling((double)TotalItems / PageSize);
    }

    public IReadOnlyList<T> Items { get; }

    public int Page { get; }

    public int PageSize { get; }

    public long TotalItems { get; }

    public long TotalPages { get; }

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}
