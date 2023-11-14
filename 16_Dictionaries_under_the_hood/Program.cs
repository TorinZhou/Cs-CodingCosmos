using System.Security.Cryptography.X509Certificates;

var hasSet = new HashSet<string>()
{
    "aaa", "aaa"
};

var testStack = new Stack<string>();
Console.ReadLine();

public class SpellChecker
{
    //private readonly Dictionary<string, ?> _correctWords = new();
    private readonly HashSet<string> _correctWords = new()
    {
        "dog", "cat", "fish"
    };
    public bool IsCorrect(string word) =>
        _correctWords.Contains(word);

    public void AddCorrectWord(string word) =>
        _correctWords.Add(word);


    // the exercise: merge to HashSets is easy 
    public static HashSet<T> CreateUnion<T>(
            HashSet<T> set1, HashSet<T> set2)
    {
        //your code goes here
        HashSet<T> result = new HashSet<T>(set1);
        foreach (var item in set2)
        {
            result.Add(item);
        }
        return result;
    }
  }

public static class StackExtensions
{
    //your code goes here
    public static bool DoesContainAny(this Stack<string> stackToBeTest, params string[] input)
    {
        bool result = false;
        foreach (var item in input)
        {
            if (stackToBeTest.Contains(item))
            {
                return true;
            }
        }
        return false;
    }
    public static bool DoesCotainAnyLinkVersion(this Stack<string> stackToBeTest, params string[] input)
    {
        return input.Any(input => stackToBeTest.Contains(input));
    }
}
