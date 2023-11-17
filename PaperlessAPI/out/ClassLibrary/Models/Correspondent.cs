using System;
using System.Text.Json.Serialization;

namespace NPaperless.Services.Models;

public partial class CorrespondentDto
{
    [JsonPropertyName("name")]
    public string name
    {
        get; set;
    } = string.Empty;

    [JsonPropertyName("match")]
    public string match
    {
        get; set;
    } = string.Empty;

    [JsonPropertyName("matching_algorithm")]
    public long matching_algorithm
    {
        get; set;
    } = 0;

    [JsonPropertyName("is_insensitive")]
    public bool is_insensitive
    {
        get; set;
    } = false;

    [JsonPropertyName("document_count")]
    public long document_count
    {
        get; set;
    } = 0;

    [JsonPropertyName("last_correspondence")]
    public DateTime last_correspondence
    {
        get; set;
    } = DateTime.Now;
}

public partial class Correspondent
{
    [JsonPropertyName("id")]
    public long id
    {
        get; set;
    }
    
    [JsonPropertyName("slug")]
    
    public string slug
    {
        get;set;
    } = string.Empty;


    [JsonPropertyName("name")]
    public string name
    {
        get; set;
    } = string.Empty;

    [JsonPropertyName("match")]
    public string match
    {
        get; set;
    } = string.Empty;

    [JsonPropertyName("matching_algorithm")]
    public long matching_algorithm
    {
        get; set;
    } = 0;

    [JsonPropertyName("is_insensitive")]
    public bool is_insensitive
    {
        get; set;
    } = false;

    [JsonPropertyName("document_count")]
    public long document_count
    {
        get; set;
    } = 0;

    [JsonPropertyName("last_correspondence")]
    public DateTime last_correspondence
    {
        get; set;
    } = DateTime.Now;
}