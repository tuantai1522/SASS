namespace SASS.SharedKernel.Results;

public sealed record CursorPagedResponse<T>(IReadOnlyList<T> Items, string? NextCursor, bool HasNextPage);
