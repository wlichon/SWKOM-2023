using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NPaperless.Services.Models;
public class IntArrayConverter : JsonConverter<int[]>
{
    public override int[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the JSON string value and split it into an array of int values
        string numbersString = reader.GetString();
        string[] numbersArray = numbersString.Split(',');
        int[] numbers = Array.ConvertAll(numbersArray, int.Parse);
        return numbers;
    }

    public override void Write(Utf8JsonWriter writer, int[] value, JsonSerializerOptions options)
    {
        string numbersString = string.Join(",", value);
        writer.WriteStringValue(numbersString);
    }
}
