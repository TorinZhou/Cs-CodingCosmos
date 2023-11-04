using System.Text.Json;
using System.Text.Json.Serialization;



//todo create a class that reads JSON strings from any open API
//! 1. Declaritive, I want to use this class this way

string apiString = "datausa.io/api/data?drilldowns=Nation&measures=Population";
string baseAddress = "https://datausa.io/api/";
string requestUri = "data?drilldowns=Nation&measures=Population";
//x ApiDataReader jsonReader = new ApiDataReader();
//! 4. After abstract the interface, we can declare the instance with the API type.
IApiDataReader apiDataReader = new ApiDataReader();

//! 5 add the await keyword before it, make sure the result is a string, rather than a task of string
//x var jsonString = apiDataReader.Read(baseAddress, requestUri);
var jsonString = await apiDataReader.Read(baseAddress, requestUri);
Console.WriteLine(jsonString);
Root myDeserializedClass = JsonSerializer.Deserialize<Root>(jsonString);

foreach (var item in myDeserializedClass.data)
{
    Console.WriteLine($"{item.IDYear}: {item.Nation} - {item.Population}");
}

Console.ReadKey();

public interface IApiDataReader
{
    Task<string> Read(string baseAddress, string requestUri);
}



public class ApiDataReader : IApiDataReader
{
    //! 2. First, Should the API be a property. Or a parameter of the Read() method?
    //!     - Property means each instance take responsibility for an API connection. When the connection ends, may be I can dispose it.
    //!     - Parameter means otherwise. it sounds like a static method.

    private readonly HttpClient _httpClient = new HttpClient();
    public ApiDataReader()
    {

    }

    public async Task<string> Read(string baseAddress, string requestUri)
    {
        _httpClient.BaseAddress = new Uri(baseAddress);
        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            string jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //! 3. After implement the Read() method, it's perfect time to abstract an interface.
}



// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record Annotations(
    [property: JsonPropertyName("source_name")] string source_name,
    //! [property: Json.......] the attribute is telling the serializer:
    //! "When you are deserializing (or serializing) this object, map the C# property to the JSON property named 'source'."
    //! "Rather than a field with the same name"
    //! the attribute blow in built in .Net, if I want to use Newtonsoft.Json, It would be [JsonProperty("some_name")]
    //! both instruct how the program map the json to class, and vise versa
    [property: JsonPropertyName("source_description")] string source_description,
    [property: JsonPropertyName("dataset_name")] string dataset_name,
    [property: JsonPropertyName("dataset_link")] string dataset_link,
    [property: JsonPropertyName("table_id")] string table_id,
    [property: JsonPropertyName("topic")] string topic,
    [property: JsonPropertyName("subtopic")] string subtopic
);

public record Datum(
    [property: JsonPropertyName("ID Nation")] string IDNation,
    [property: JsonPropertyName("Nation")] string Nation,
    [property: JsonPropertyName("ID Year")] int IDYear,
    [property: JsonPropertyName("Year")] string Year,
    [property: JsonPropertyName("Population")] int Population,
    [property: JsonPropertyName("Slug Nation")] string SlugNation
);

public record Root(
    [property: JsonPropertyName("data")] IReadOnlyList<Datum> data,
    [property: JsonPropertyName("source")] IReadOnlyList<Source> source
);

public record Source(
    [property: JsonPropertyName("measures")] IReadOnlyList<string> measures,
    [property: JsonPropertyName("annotations")] Annotations annotations,
    [property: JsonPropertyName("name")] string name,
    [property: JsonPropertyName("substitutions")] IReadOnlyList<object> substitutions
);


// The JSON:
//{
//    "data":[
//       { "ID Nation":"01000US","Nation":"United States","ID Year":2021,"Year":"2021","Population":329725481,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2020,"Year":"2020","Population":326569308,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2019,"Year":"2019","Population":324697795,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2018,"Year":"2018","Population":322903030,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2017,"Year":"2017","Population":321004407,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2016,"Year":"2016","Population":318558162,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2015,"Year":"2015","Population":316515021,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2014,"Year":"2014","Population":314107084,"Slug Nation":"united-states"},
//		{ "ID Nation":"01000US","Nation":"United States","ID Year":2013,"Year":"2013","Population":311536594,"Slug Nation":"united-states"}
//		],
//
//    "source":[
//      {"measures":["Population"],
//       "annotations":
//	        {"source_name":"Census Bureau",
//	         "source_description":"The American Community Survey (ACS) is conducted by the US Census and sent to a portion of the population every year.",
//	         "dataset_name":"ACS 5-year Estimate",
//	         "dataset_link":"http://www.census.gov/programs-surveys/acs/",
//	         "table_id":"B01003",
//	         "topic":"Diversity",
//	         "subtopic":"Demographics"
//	        },
//	     "name":"acs_yg_total_population_5",
//	     "substitutions":[]
//	        }
//	     ] 
//}