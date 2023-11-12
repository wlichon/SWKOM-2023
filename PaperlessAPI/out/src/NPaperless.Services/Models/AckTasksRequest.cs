using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NPaperless.Services.Models;

public class AckTasksRequest
{
    [JsonPropertyName("tasks")]
    public IEnumerable<int> Tasks { get; set; }
}