using System;
using System.Text.Json.Serialization;

namespace NPaperless.Services.Models;

public partial class CorrespondentDto
{
    [JsonPropertyName("name")]
    public string name
    {
        get; set;
    }

    [JsonPropertyName("match")]
    public string match
    {
        get; set;
    }

    [JsonPropertyName("matching_algorithm")]
    public long matching_algorithm
    {
        get; set;
    }

    [JsonPropertyName("is_insensitive")]
    public bool is_insensitive
    {
        get; set;
    }

    [JsonPropertyName("document_count")]
    public long document_count
    {
        get; set;
    }

    [JsonPropertyName("last_correspondence")]
    public DateTime last_correspondence
    {
        get; set;
    }
}

public partial class Correspondent
{
    [JsonPropertyName("id")]
    public long id
    {
        get; set;
    }


   
    private string _slug = "Default";
    
    [JsonPropertyName("slug")]
    
    public string slug
    {
        get { return _slug; }
        set { _slug = value; }
    }


    [JsonPropertyName("name")]
    public string name
    {
        get; set;
    }

    [JsonPropertyName("match")]
    public string match
    {
        get; set;
    }

    [JsonPropertyName("matching_algorithm")]
    public long matching_algorithm
    {
        get; set;
    }

    [JsonPropertyName("is_insensitive")]
    public bool is_insensitive
    {
        get; set;
    }

    [JsonPropertyName("document_count")]
    public long document_count
    {
        get; set;
    }

    /*
    private DateTime _lastCorrespondence = DateTime.Now;

    [JsonPropertyName("last_correspondence")]
    public DateTime last_correspondence
    {
        get { return _lastCorrespondence; }
        set { _lastCorrespondence = value != DateTime.MinValue ? value : _lastCorrespondence; }
    }

    private DateTime _lastCorrespondence = DateTime.Now;

    [JsonPropertyName("last_correspondence")]
    public DateTime last_correspondence
    {
        get { return _lastCorrespondence; }
        set { 
            if(value != DateTime.MinValue)
                _lastCorrespondence = value;
        }
    }

    */
    [JsonPropertyName("last_correspondence")]
    public DateTime last_correspondence
    {
        get;set;
    }
}