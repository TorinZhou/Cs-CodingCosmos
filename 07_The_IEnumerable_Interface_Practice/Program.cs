using System.Collections;

var text = "hello there";
foreach (char c in text)
{
    Console.WriteLine(c);
}
//! 1. String is a collection. The underlying structure of strings is arrays of chars
//!    If string type didn't implement IEnumerable, the foreach woundn't work.
// --------------------------------------------------------------------------------------
var customCollection = new CustomCollection(
    new string[] { "aaa", "bbb", "cccc" });


//var words = new string[] { "aaa", "bbb", "ccc" };
//IEnumerator wordsEnumerator = words.GetEnumerator();
//object currentWord;
//while(wordsEnumerator.MoveNext())
//{
//    currentWord = wordsEnumerator.Current;
//    Console.WriteLine(currentWord);
//}



foreach (var word in customCollection)
{
    Console.WriteLine(word);
}



Console.ReadKey();

public record CustomCollection : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        return new WordsEnumerator(Words);
    }
    public string[] Words { get; }

    public CustomCollection(string[] words)
    {
        Words = words;
    }
}

public class WordsEnumerator : IEnumerator
{
    private const int InitialPosition = -1;
    private int _currentPosition = InitialPosition;
    private readonly string[] _words;

    public WordsEnumerator(string[] words)
    {
        _words = words;
    }

    public object Current
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
        //if (_currentPosition <= _words.Length)
        //{
        //    return true;
        //}
        //return false;
        return _currentPosition < _words.Length;
    }

    public void Reset()
    {
        _currentPosition = InitialPosition;
    }
}