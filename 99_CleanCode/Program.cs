using static System.Console;
bool shallReadFromDatabase = false;
PersonDataFormatter personalDataFormatter = (shallReadFromDatabase) ?
    new DatabaseSourcePersonalDataFormatter() :
    new ExcelSourcePersonalDataFormatter();
WriteLine(personalDataFormatter.Format());

ReadKey();
record struct Person(string Name, int YearOfBirth, string Country);
abstract class PersonDataFormatter
{
    public string Format()
    {
        var people = ReadPeople();
        return string.Join("\n",
            people.Select(people => $"{people.Name} born in" +
            $" {people.Country} on {people.YearOfBirth}"));
    }

    public abstract IEnumerable<Person> ReadPeople();
}

class DatabaseSourcePersonalDataFormatter : PersonDataFormatter
{

    public override IEnumerable<Person> ReadPeople()
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

class ExcelSourcePersonalDataFormatter : PersonDataFormatter
{

    public override IEnumerable<Person> ReadPeople()
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