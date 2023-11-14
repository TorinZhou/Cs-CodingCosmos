

using System.Collections.ObjectModel;

var planets = ReadPlanets();
var asList = (ReadOnlyCollection<string>)planets;
//! asList.Clear(); would not compile
var a = asList[1];
//! asList[2] = "dddd"; would not compile, because ReadOnlyCollection set the index setter to helperError while keeping the getter

var dictionary = new Dictionary<string, int>
{
    ["aaa"] = 1
};

var readOnlyDictionary = new ReadOnlyDictionary<string, int>(dictionary);

Console.ReadKey();

IEnumerable<string> ReadPlanets()
{
    var result = new List<string>
    {
        "aaaa",
        "vvvv",
        "dddd",
    };
    return new ReadOnlyCollection<string>(result);
}

