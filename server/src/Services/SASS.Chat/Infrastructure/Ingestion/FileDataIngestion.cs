using SASS.Chassis.AI.Ingestion;

namespace SASS.Chat.Infrastructure.Ingestion;

public sealed record FileDataIngestion(string UserId, string FileId, string Content, int Index, int? PageNumber, int? IndexOnPage) : IIngestionData;
