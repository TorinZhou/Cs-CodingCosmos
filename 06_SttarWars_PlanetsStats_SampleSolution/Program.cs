
using _06_SttarWars_PlanetsStats_SampleSolution.DTOs;
using StarWarsPlanetsStats.ApiDataAccess;
using System.Text.Json;


try
{
    await new StarWarsPlanetsStatsApp(new ApiDataReader(), new MockStarWarsApiDataReader()).Run();
}
catch(Exception ex)
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
        //! By doing this, we inject the dependency into the class. Aligning with the DIP.
        _apiDataReader = apiDataReader;
        _secondaryApiDataReader = secondaryApiDataReader;
    }

    public async Task Run() //! IDK why have to return Task for a async method
    {
        string? json = null;  //! this line is not my code, I don't know why explicitly assign a null here.
        try
        {
            json = await _apiDataReader.Read("https://swapi.dev", "api/planets");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("API request was failed" + ex.Message);
        }
        if(json == null)
        {
            json = await _secondaryApiDataReader.Read("https://swapi.dev", "api/planets");
        }
        var root = JsonSerializer.Deserialize<Root>(json);
    }
}






