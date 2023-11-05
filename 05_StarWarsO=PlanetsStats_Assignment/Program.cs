// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Serialization;

string baseAddress = "https://swapi.dev/api/";
string requestUri = "planets";
var userInterface = new UserConsoleInterface();
var apiDataReader = new ApiDataJsonReader();
var dataConverter = new DataToRecordStructConverter();
App app = new App(userInterface, apiDataReader, dataConverter);

try
{
    await app.LoadDataAsync(baseAddress, requestUri);
    app.Present();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.PromptUser();

Console.WriteLine("Press any key to close.");
Console.ReadKey();

public class App
{
    private readonly IUserInterface _userInterface;
    private readonly IApiDataReader _apiDataReader;
    private readonly IApiDataCache _apiDataCache;
    private readonly IDataConverter _dataConverter;
    public List<PlanetInfo> PlanetsInfos { get; private set; } = new();

    public App(IUserInterface userInterface, IApiDataReader apiDataReader, IDataConverter dataConverter)
    {
        _userInterface = userInterface;
        _apiDataReader = apiDataReader;
        _dataConverter = dataConverter;
    }
    public async Task LoadDataAsync(string baseAddress, string requestUri)
    {
        var jsonString = await Read(baseAddress, requestUri);
        //_apiDataCache.AddOrUpdate()
        PlanetsInfos = _dataConverter.Convert(jsonString);
    }
    private Task<string> Read(string baseAddress, string requestUri)
    {
        return _apiDataReader.Read(baseAddress, requestUri);
    }
    public void Present()
    {
        //_userInterface.DisplayMessage();
        _userInterface.Present(PlanetsInfos);
    }

    public void PromptUser()
    {
        throw new NotImplementedException();
    }
}


//todo UI
public interface IUserInterface
{
    void Present(List<PlanetInfo> planetsInfos);
}

public class UserConsoleInterface : IUserInterface
{
    
    public void Present(List<PlanetInfo> planetsInfos)
    {
        foreach (var planetInfo in planetsInfos)
        {
            Console.WriteLine(planetInfo);
        }
    }
}


//todo DataReader
public interface IApiDataReader
{
    Task<string> Read(string baseAddress, string requestUri);
}

public class ApiDataJsonReader : IApiDataReader
{
    private readonly HttpClient _httpClient = new HttpClient();
    public async Task<string> Read(string baseAddress, string requestUri)
    {
        _httpClient.BaseAddress = new Uri(baseAddress);
        try
        {
            using var response = await _httpClient.GetAsync(requestUri);
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}


//todo DataCache
public interface IApiDataCache
{
    bool TryGetValue(string key, out string value);
    void AddOrUpdate(string key, string value);
}
public class ApiDataCache : IApiDataCache
{
    private readonly Dictionary<string, string> _cache = new();
    public void AddOrUpdate(string key, string value)
    {
        _cache[key] = value;
    }

    public bool TryGetValue(string key, out string value)
    {
        return _cache.TryGetValue(key, out value);
    }
}


//todo DataDeserializing
// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record Result(
    [property: JsonPropertyName("name")] string name,
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

public record Root(
    [property: JsonPropertyName("count")] int count,
    [property: JsonPropertyName("next")] string next,
    [property: JsonPropertyName("previous")] object previous,
    [property: JsonPropertyName("results")] IReadOnlyList<Result> results
);


//todo DataManipulation
public interface IDataConverter
{
    List<PlanetInfo> Convert(string jsonString);

}
public class DataToRecordStructConverter : IDataConverter
{
    public List<PlanetInfo> Convert(string jsonString)
    {
        var planetsInfos = new List<PlanetInfo>();
        Root myDeserializedClass = JsonSerializer.Deserialize<Root>(jsonString);

        foreach (var planet in myDeserializedClass.results)
        {
            if (int.TryParse(planet.population, out var population) &&
               int.TryParse(planet.diameter, out var diameter) &&
               int.TryParse(planet.surface_water, out var surfaceWater))
            {
                planetsInfos.Add(new PlanetInfo(population, diameter, surfaceWater));
            }
        }
        return planetsInfos;
    }
}



//todo PlanetData
public record struct PlanetInfo(int Population, int Diameter, int SurfaceWater)
{
    public override string ToString()
    {
        return $"Population: {Population}, Diameter: {Diameter}km, Surface Water: {SurfaceWater}%";
    }
}