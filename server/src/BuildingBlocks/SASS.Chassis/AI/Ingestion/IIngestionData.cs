namespace SASS.Chassis.AI.Ingestion;

public interface IIngestionData
{
    /// <summary>
    /// From documents, chunk into multiple smaller documents, every document is an index
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// To store current page number if it has (for PDF, Word)
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Index on that page if it has page number (for PDF, Word)
    /// </summary>
    public int? IndexOnPage { get; set; }
}
