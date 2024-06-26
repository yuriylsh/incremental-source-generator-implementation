﻿namespace Autoproperty.Sample;

public interface IAuditMetadata
{
    DateTimeOffset LastUpdated { get; set; }
}

public partial record Book : IAuditMetadata
{
    public required string Title { get; set; }

    public required string Author { get; set; }

    public DateTimeOffset LastUpdated { get; set; }
}