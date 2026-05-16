using SASS.Chassis.AI.Ingestion;

namespace SASS.Chat.Infrastructure.Ingestion;

public sealed record FileDataIngestion(Guid UserId, string Name, string Content, int Index, int? PageNumber, int? IndexOnPage) : IIngestionData;
