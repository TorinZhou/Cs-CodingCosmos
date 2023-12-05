bool useSpecial = true;
var passwordGenerator = useSpecial ?
    new PasswordGenerator(new BuiltInRandom(), new SpecialCharacterProvider()) :
    new PasswordGenerator(new BuiltInRandom(), new StandardCharacterProvider());
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(passwordGenerator.Generate(5, 10));
}
Console.ReadKey();

public class PasswordGenerator
{
    private readonly IRandomIntAlgorithm _randomIntAlgorith;
    private readonly ICharacterProvider _characterProvider;
    public PasswordGenerator(IRandomIntAlgorithm randomIntAlgorith, ICharacterProvider characterProvider)
    {
        _randomIntAlgorith = randomIntAlgorith;
        _characterProvider = characterProvider;
    }

    public string Generate(
        int minLength, int maxLength)
    {
        //validate max and min length
        EvaluateLengthRange(minLength, maxLength);

        //randomly pick the length of password between left and right range
        var resultLength = _randomIntAlgorith.Next(minLength, maxLength + 1);

        //generate random string
        var chars = _characterProvider.GetCharacters();

        return new string(Enumerable.Repeat(chars, resultLength).Select(chars => chars[_randomIntAlgorith.Next(chars.Length)]).ToArray());
    }

    private static void EvaluateLengthRange(int minLength, int maxLength)
    {
        if (minLength < 1)
        {
            throw new ArgumentOutOfRangeException(
                $"leftRange must be greater than 0");
        }
        if (maxLength < minLength)
        {
            throw new ArgumentOutOfRangeException(
                $"leftRange must be smaller than rightRange");
        }
    }
}



public interface IRandomIntAlgorithm
{
    int Next(int min, int max);
    int Next(int max);
}

public class BuiltInRandom : IRandomIntAlgorithm
{
    public int Next(int min, int max)
    {
        return new Random().Next(min, max);
    }

    public int Next(int max)
    {
        return new Random().Next(max);
    }
}

public interface ICharacterProvider
{
    string GetCharacters();
}

public class StandardCharacterProvider : ICharacterProvider
{
    public string GetCharacters()
    {
        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }
}

public class SpecialCharacterProvider : ICharacterProvider
{
    public string GetCharacters()
    {
        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=";
    }
}