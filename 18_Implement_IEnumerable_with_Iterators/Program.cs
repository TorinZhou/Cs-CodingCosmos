using System.Collections;


//! 1. String is a collection. The underlying structure of strings is arrays of chars
//!    If string type didn't implement IEnumerable, the foreach woundn't work.
// --------------------------------------------------------------------------------------
var customCollection = new CustomCollection(
    new string[] { "aaa", "bbb", "cccc" });

var enumerator = customCollection.GetEnumerator();




foreach (var word in customCollection)
{
    Console.WriteLine(word);
}


Console.ReadKey();


public record CustomCollection : IEnumerable<string>
{
    public string[] Words { get; }
    public CustomCollection(string[] words)
    {
        Words = words;
    }
    IEnumerator IEnumerable.GetEnumerator()
    //! not working until set var to IEnumerable
    //! Which means, it's only usable through an instance of the interface itself.
    //! for CustomCollection type, it's not accessable. 
    //! and that's the reason why we can't use access modifier, such as public, on it in this class
    {
        return GetEnumerator();
    }


    public IEnumerator<string> GetEnumerator()
    {
        //! To implement the GetEnumerator() method, we got several options.
        //! 1. This works because our collection simply wraps aother collection, we can easily refer to its own GetEnumerator() method.
        IEnumerable<string> words = Words;
        return words.GetEnumerator();
        //! 2. If not, we can implement our custom IEnumerator. version 1
        //return new WordsEnumerator(Words);
        //! 3. version 2 
        //foreach(var word in Words)
        //{
        //    yield return word;
        //}
    }


}

//public class WordsEnumerator : IEnumerator<string>
////! the generic IEnumerator<T> implemented IEnumerator, whichi means we have to implement both of them in this class
//{
//    private const int InitialPosition = -1;
//    private int _currentPosition = InitialPosition;
//    private readonly string[] _words;

//    public WordsEnumerator(string[] words)
//    { 
//        _words = words;
//    }

//    object IEnumerator.Current => Current;

//    public string Current
//    {
//        get
//        {
//            try
//            {
//                return _words[_currentPosition];
//            }
//            catch (IndexOutOfRangeException ex)
//            {
//                throw new IndexOutOfRangeException($"{nameof(CustomCollection)}'s end reached", ex);
//            }
//        }
//    }

//    public bool MoveNext()
//    {
//        _currentPosition++;
//        //if (_currentPosition <= _words.Length)
//        //{
//        //    return true;
//        //}
//        //return false;
//        return _currentPosition < _words.Length;
//    }

//    public void Reset()
//    {
//        _currentPosition = InitialPosition;
//    }

//    public void Dispose()
//    {

//    }
//}