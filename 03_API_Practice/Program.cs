// See https://aka.ms/new-console-template for more information

// API: Any protocol specifying how software components should interact with each other

using var client = new HttpClient();
// Uri: Uniform* Resource Identifier; do not omit the / at the end
client.BaseAddress = new Uri("https://datausa.io/api/");

// the return type response, is a Task<HttpResponseMessage>, it's a task, not the result itself, 
// for now we have to add the await keyword.
//var response = client.GetAsync("data?drilldowns=Nation&measures=Population");

// now the response type is the response message itself as we need
var response = await client.GetAsync("data?drilldowns=Nation&measures=Population");

response.EnsureSuccessStatusCode();

string json = await response.Content.ReadAsStringAsync();

// the api: https://datausa.io/api/data?drilldowns=Nation&measures=Population








Console.ReadKey();