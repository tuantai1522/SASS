namespace SASS.SharedKernel.Results;

public sealed class CursorPagedRequest
{
    public string? Cursor { get; init; }
    public int Limit { get; init; } = 20;
    public bool IsAscending { get; init; }
}
