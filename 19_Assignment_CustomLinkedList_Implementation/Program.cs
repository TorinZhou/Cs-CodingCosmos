

using System.Collections;

Console.ReadKey();

public interface ILinkedList<T> : ICollection<T>
{
    void AddToFront(T? item);
    void AddToEnd(T? item);
}
public class CustomLikedList<T> : ILinkedList<T>
{
    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => false;

    public void Add(T? item)
    {
        AddToEnd(item);
    }

    public void AddToEnd(T? item)
    {
        throw new NotImplementedException();
    }

    public void AddToFront(T? item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T? item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }


    
    public bool Remove(T? item)
        //todo remove the first node whose data is equal to the given argument, including null
    {
        throw new NotImplementedException();
    }
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public record Node<T>
{
    T? Value { get; set; }
    Node<T>? Next { get; set; }
}