using System;
using System.Text.Json.Serialization;

namespace NPaperless.Services.Models;

/*public interface IDocument
{
    string Title { get; set; }
    string Content { get; set; }
}

public partial class Document : IDocument
{
    // ... other properties and methods ...

    // Implement properties from the interface
    public string Title { get; set; }
    public string Content { get; set; }
}*/

public partial class Document
{
    [JsonPropertyName("id")]
    public uint id { get; set; }

    [JsonPropertyName("correspondent")]
    public uint? correspondent { get; set; }

    [JsonPropertyName("document_type")]
    public uint? documentType { get; set; }

    [JsonPropertyName("storage_path")]
    public uint? storagePath { get; set; }

    [JsonPropertyName("title")]
    public string title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string content { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public uint[] tags { get; set; } = new uint[0];

    [JsonPropertyName("created")]
    public DateTime created { get; set; }

    [JsonPropertyName("created_date")]
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateTime created_date { get; set; }

    [JsonPropertyName("modified")]
    public DateTime modified { get; set; }

    [JsonPropertyName("added")]
    public DateTime added { get; set; }

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
    public uint id { get; set; }

    [JsonPropertyName("correspondent")]
    public uint? correspondent { get; set; }

    [JsonPropertyName("document_type")]
    public uint? documentType { get; set; }

    [JsonPropertyName("title")]
    public string title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string content { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public uint[] tags { get; set; } = new uint[0];

    [JsonPropertyName("created_date")]
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateTime created_date { get; set; }

    [JsonPropertyName("created")]
    public DateTime created { get; set; }


    [JsonPropertyName("added")]
    public DateTime added { get; set; }
}
