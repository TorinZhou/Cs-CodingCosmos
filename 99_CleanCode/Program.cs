using static System.Console;
bool shallReadFromDatabase = true;
var personalDataFormatter = (shallReadFromDatabase) ?
    new PersonDataFormatter(new DatabaseSourcePersonalDataReader()) :
    new PersonDataFormatter(new ExcelSourcePersonalDataReader());
WriteLine(personalDataFormatter.Format());

ReadKey();

interface IPersonalDataReader
{
    IEnumerable<Person> ReadPeople();
}
record struct Person(string Name, int YearOfBirth, string Country);

class PersonDataFormatter
{
    private readonly IPersonalDataReader _personDataReader;

    public PersonDataFormatter(IPersonalDataReader personDataReader)
    {
        _personDataReader = personDataReader;
    }

    public string Format()
    {
        var people = _personDataReader.ReadPeople();
        return string.Join("\n",
            people.Select(people => $"{people.Name} born in" +
            $" {people.Country} on {people.YearOfBirth}"));
    }

}

class DatabaseSourcePersonalDataReader : IPersonalDataReader
{

    public IEnumerable<Person> ReadPeople()
    {
        WriteLine("Reading from database");
        return new List<Person>
        {
            new Person("Jonh", 1982, "USA"),
            new Person("Aja", 1982, "India"),
            new Person("Tom", 1982, "Australia"),
        };
    }
}

class ExcelSourcePersonalDataReader : IPersonalDataReader
{

    public IEnumerable<Person> ReadPeople()
    {
        WriteLine("Reading from Excel File");
        return new List<Person>
        {
            new Person("Martin", 1972, "France"),
            new Person("Aiko", 1982, "Japan"),
            new Person("Selene", 1982, "UK"),
        };
    }
}

// first I made the class abstract, then made the ReadPeople abstract by deleting the body