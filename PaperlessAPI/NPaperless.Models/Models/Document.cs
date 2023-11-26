using System;
using System.Text.Json.Serialization;

namespace NPaperless.Services.Models;

public partial class Document
{
    [JsonPropertyName("id")]
    public uint Id { get; set; }

    [JsonPropertyName("correspondent")]
    public uint? Correspondent { get; set; }

    [JsonPropertyName("document_type")]
    public uint? DocumentType { get; set; }

    [JsonPropertyName("storage_path")]
    public uint? StoragePath { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public uint[] Tags { get; set; } = new uint[0];

    [JsonPropertyName("created")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("created_date")]
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("modified")]
    public DateTime Modified { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("added")]
    public DateTime Added { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("archive_serial_number")]
    public string? ArchiveSerialNumber { get; set; } = string.Empty;

    [JsonPropertyName("original_file_name")]
    public string OriginalFileName { get; set; } = string.Empty;

    [JsonPropertyName("archived_file_name")]
    public string ArchivedFileName { get; set; } = string.Empty;
}

public partial class DocumentDto
{
    [JsonPropertyName("id")]
    public uint Id { get; set; }

    [JsonPropertyName("correspondent")]
    public uint? Correspondent { get; set; }

    [JsonPropertyName("document_type")]
    public uint? DocumentType { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public uint[] Tags { get; set; } = new uint[0];

    [JsonPropertyName("created_date")]
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateTime created_date { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("created")]
    public DateTime Created { get; set; } = DateTime.UtcNow;


    [JsonPropertyName("added")]
    public DateTime Added { get; set; } = DateTime.UtcNow;
}
