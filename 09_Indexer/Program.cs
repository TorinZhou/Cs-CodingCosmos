using System.Collections;
var customCollection = new CustomCollection(
    new string[] { "aaa", "bbb", "cccc" });

var first = customCollection[0];
customCollection[1] = "abc";

Console.ReadKey();
public record CustomCollection : IEnumerable<string>
{
    public string[] Words { get; }
    public CustomCollection(string[] words)
    {
        Words = words;
    }

    public string this[int index]
    {
        get => Words[index];
        set => Words[index] = value;  
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public IEnumerator<string> GetEnumerator()
    {
        return new WordsEnumerator(Words);
    }
}

public class WordsEnumerator : IEnumerator<string>
{
    private const int InitialPosition = -1;
    private int _currentPosition = InitialPosition;
    private readonly string[] _words;

    public WordsEnumerator(string[] words)
    {
        _words = words;
    }
    object IEnumerator.Current => Current;
    public string Current
    {
        get
        {
            try
            {
                return _words[_currentPosition];
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException($"{nameof(CustomCollection)}'s end reached", ex);
            }
        }
    }
    public bool MoveNext()
    {
        _currentPosition++;
        return _currentPosition < _words.Length;
    }
    public void Reset()
    {
        _currentPosition = InitialPosition;
    }
    public void Dispose()
    {

    }
}