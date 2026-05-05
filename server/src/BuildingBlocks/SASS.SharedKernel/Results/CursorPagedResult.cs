namespace SASS.SharedKernel.Results;

public sealed record CursorPagedResult<T>(IReadOnlyList<T> Items, string? NextCursor, bool HasNextPage);
