using System.Text.Json.Serialization;

namespace _06_SttarWars_PlanetsStats_SampleSolution.DTOs;

//todo DataDeserializing
// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

//! 2. The result type is only a "DTOs" - Data Transfer Objects. Only serving a data transfer purpose. They should be move to a seperate place
public record Result(
    [property: JsonPropertyName("name")] string name,
    //! 1. here, the attributes can be omitted, since the property names are the same as they were in json
    //! It's better to leave them here though
    [property: JsonPropertyName("rotation_period")] string rotation_period,
    [property: JsonPropertyName("orbital_period")] string orbital_period,
    [property: JsonPropertyName("diameter")] string diameter,
    [property: JsonPropertyName("climate")] string climate,
    [property: JsonPropertyName("gravity")] string gravity,
    [property: JsonPropertyName("terrain")] string terrain,
    [property: JsonPropertyName("surface_water")] string surface_water,
    [property: JsonPropertyName("population")] string population,
    [property: JsonPropertyName("residents")] IReadOnlyList<string> residents,
    [property: JsonPropertyName("films")] IReadOnlyList<string> films,
    [property: JsonPropertyName("created")] DateTime created,
    [property: JsonPropertyName("edited")] DateTime edited,
    [property: JsonPropertyName("url")] string url
);