﻿namespace netflix_clone_media.Api.Settings;

public class RedisSettings
{
    public const string SectionName = "RedisSettings";
    public string ConnectionString { get; set; } = default!;
    public bool Enabled { get; set; }
}
