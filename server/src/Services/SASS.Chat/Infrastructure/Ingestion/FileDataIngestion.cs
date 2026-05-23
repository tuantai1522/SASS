using SASS.Chassis.AI.Ingestion;

namespace SASS.Chat.Infrastructure.Ingestion;

public sealed record FileDataIngestion(Guid UserId, Guid FileId, string Name, string Content, int Index, int? PageNumber, int? IndexOnPage) : IIngestionData;
