using System.Text.Json.Serialization;

namespace _06_SttarWars_PlanetsStats_SampleSolution.DTOs;

public record Root(
    [property: JsonPropertyName("count")] int count,
    [property: JsonPropertyName("next")] string next,
    [property: JsonPropertyName("previous")] object previous,
    [property: JsonPropertyName("results")] IReadOnlyList<Result> results
);