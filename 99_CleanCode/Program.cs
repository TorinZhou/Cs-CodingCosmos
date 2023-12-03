using static System.Console;
bool shallReadFromDatabase = true;
var personalDataFormatter = new PersonDataFormatter();
WriteLine(personalDataFormatter.Format());

ReadKey();
class PersonDataFormatter
{
    public string Format()
    {
        var people = ReadPeople();
        return string.Join("\n",
            people.Select(people => $"{people.Name} born in" +
            $" {people.Country} on {people.YearOfBirth}"));
    }

    public IEnumerable<Person> ReadPeople()
    {
        WriteLine("Reading from database");
        return new List<Person>
        {
            new Person("John", 1982, "USA"),
            new Person("Aja", 1992, "India"),
            new Person("Tom", 1954, "Australia"),
        };
    }
}

record struct Person(string Name, int YearOfBirth, string Country);