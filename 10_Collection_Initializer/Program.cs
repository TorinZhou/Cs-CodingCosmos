using System.Collections;

//! to do initializer, an public Add is needed.
var newCollection = new CustomCollection
{ 
    "aaa", "bbb", "ccc" 
};



Console.ReadKey();
public record CustomCollection : IEnumerable<string>
{
    public string[] Words { get; }
    
    public CustomCollection() 
    {
        Words = new string[10];
    }
    private int _currentIndex = 0;
    public void Add(string word)
    {
        Words[_currentIndex] = word;
        ++ _currentIndex;
    }
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