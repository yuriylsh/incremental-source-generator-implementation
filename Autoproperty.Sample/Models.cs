﻿namespace Autoproperty.Sample;

public interface IAuditMetadata
{
    DateTimeOffset LastUpdated { get; set; }
}

public record Book : IAuditMetadata
{
    public required string Title { get; set; }

    public required string Author { get; set; }

    public DateTimeOffset LastUpdated { get; set; }
}