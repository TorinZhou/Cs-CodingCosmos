﻿
using _06_SttarWars_PlanetsStats_SampleSolution.DTOs;
using StarWarsPlanetsStats.ApiDataAccess;
using System.Numerics;
using System.Text.Json;


try
{
    await new StarWarsPlanetsStatsApp(new ApiDataReader(), new MockStarWarsApiDataReader()).Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine("Press any key to close.");
Console.ReadKey();




public class StarWarsPlanetsStatsApp
{
    private readonly IApiDataReader _apiDataReader;
    private readonly IApiDataReader _secondaryApiDataReader;

    public StarWarsPlanetsStatsApp(IApiDataReader apiDataReader, IApiDataReader secondaryApiDataReader)
    {
        // By doing this, we inject the dependency into the class. Aligning with the DIP.
        _apiDataReader = apiDataReader;
        _secondaryApiDataReader = secondaryApiDataReader;
    }

    public async Task Run() // IDK why have to return Task for a async method
    {
        string? json = null;  // this line is not my code, I don't know why explicitly assign a null here.
        try
        {
            json = await _apiDataReader.Read("https://swapi.dev", "api/planets");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("API request was failed" + ex.Message);
        }

        json ??= await _secondaryApiDataReader.Read("https://swapi.dev", "api/planets"); 

        var root = JsonSerializer.Deserialize<Root>(json);

        var planets = ToPlants(root);

        foreach (var planet in planets)
        {
            Console.WriteLine(planet);
        }

        var propertyNamesToSelectorsMapping = new Dictionary<string, Func<Planet, int?>>
        {
            ["population"] = planet => planet.Population,
            ["diameter"] = planet => planet.Diameter,
            ["surface water"] = planet => planet.SurfaceWater,
        };

        Console.WriteLine("Select from the following");
        Console.WriteLine(string.Join(Environment.NewLine, propertyNamesToSelectorsMapping.Keys));
        var userChoice = Console.ReadLine();

        if(userChoice is null || !propertyNamesToSelectorsMapping.ContainsKey(userChoice))
        {
            Console.WriteLine("Invalid Choice");
        }
        else
        {
            ShowStatistics(planets, userChoice, propertyNamesToSelectorsMapping[userChoice]);
            ShowStatistics(planets, userChoice, propertyNamesToSelectorsMapping[userChoice]);
        }


        // This is a really ugly mapping(from a string to a func).
        // when dealing with mapping, our first thought should be a dictionary
        // We should make the string a key, and the func as the value
        //switch (userChoice)
        //{
        //    case "population":
        //        ShowStatistics(planets, "population", planet => planet.Population);
        //        break;
        //    case "diameter":
        //        ShowStatistics(planets, "diameter", planet => planet.Diameter);
        //        break;
        //    case "surface water":
        //        ShowStatistics(planets, "surface water", planet => planet.SurfaceWater);
        //        break;
        //}

        
    }

    private void ShowStatistics(IEnumerable<Planet> planets, string propertyName, Func<Planet, int?> propertySelector)
    {
        ShowStatistics("Max", planets.MaxBy(propertySelector), propertySelector, propertyName);
        ShowStatistics("Min", planets.MinBy(propertySelector), propertySelector, propertyName);


        //var planetWithMaxProperty = planets.MaxBy(propertySelector);
        ////! If I used Max() rather than MaxBy, LINQ would return the max population in int
        ////! question here, do we use reflection here, cause in the next line we use the planet name
        //Console.WriteLine($"Max {propertyName} is: " +
        //    $"{propertySelector(planetWithMaxProperty)}" +
        //    $"planet: {planetWithMaxProperty.Name}");

        //var planetWithMinProperty = planets.MinBy(propertySelector);
        ////! If I used Max() rather than MaxBy, LINQ would return the max population in int
        ////! question here, do we use reflection here, cause in the next line we use the planet name
        //Console.WriteLine($"Min {propertyName} is: " +
        //    $"{propertySelector(planetWithMinProperty)}" +
        //    $"planet: {planetWithMinProperty.Name}");
    }

    
    private void ShowStatistics(string descriptor, Planet selectPlanet, Func<Planet, int?> propertySelector, string propertyName)
        //! this method doesn't use any class members. It could be static
    {
        // If I used Max() rather than MaxBy, LINQ would return the max population in int
        // question here, do we use reflection here, cause in the next line we use the planet name
        Console.WriteLine($"{descriptor} {propertyName} is: " +
            $"{propertySelector(selectPlanet)}" +
            $"planet: {selectPlanet.Name}");
    }


    private IEnumerable<Planet> ToPlants(Root? root)
    {
        if (root is null)
        {
            throw new ArgumentNullException(nameof(root));
        }
        return root.results.Select(planetDto => (Planet)planetDto);

        //var planets = new List<Planet>();
        //foreach (var planetDto in root.results)
        //{
        //    Planet planet = (Planet)planetDto;
        //    planets.Add(planet);
        //}

        //return planets;
    }
}


public readonly record struct Planet
// Didn't make it positional, because want to use the constructor to do validating.
// With positional record struct. The auto generated constructor doesn't have the validation.
{
    public string Name { get; }
    public int Diameter { get; }
    public int? SurfaceWater { get; }
    public int? Population { get; }

    public Planet(string name, int diameter, int? surfaceWater, int? population)
    {
        if (name is null)
        {
            throw new ArgumentNullException("name");
        }
        Name = name;
        Diameter = diameter;
        SurfaceWater = surfaceWater;
        Population = population;
    }

    public static explicit operator Planet(Result planetDto)
    {
        var name = planetDto.name;
        var diameter = int.Parse(planetDto.diameter);

        // so beautiful this code.
        int? population = planetDto.population.ToIntOrNull();

        int? surfaceWater = planetDto.surface_water.ToIntOrNull();

        return new Planet(name, diameter, surfaceWater, population);
        // In real app, The Main project should know nothing about DTOs.
        // We should do another project only for DTOs
    }


}

public static class StringExtensions
{
    public static int? ToIntOrNull(this string? input)
    {
        return int.TryParse(input, out var resultParsed) ? resultParsed : null ;
        //int? result = null;
        //if (int.TryParse(input, out int inputParsed)) // if(false), nothing happened.
        //{
        //    result = inputParsed;
        //}
        //return result;
    }

}


