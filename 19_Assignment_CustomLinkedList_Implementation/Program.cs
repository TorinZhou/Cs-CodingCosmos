using System.Collections;

var testLinkedList = new CustomLinkedList<int>(1, 3, 5, 7, 8);
testLinkedList.Add(1);
foreach (var item in testLinkedList)
{
    Console.WriteLine(item);
}

Console.WriteLine($"The Count is: {testLinkedList.Count}");
Console.WriteLine($"Contains 7: {testLinkedList.Contains(7)} ");

var testArray = new int[99];
testLinkedList.CopyTo(testArray, 3);
Console.WriteLine($"{testLinkedList.Remove(3)}");

Console.ReadKey();



public interface ILinkedList<T> : ICollection<T>
{
    void AddToFront(T? item);
    void AddToEnd(T? item);
}
public class CustomLinkedList<T> : ILinkedList<T>
{
    public Node<T>? Head { get; set; }
    public Node<T>? Tail { get; set; }
    public int Count { get; private set; }

    public CustomLinkedList(params T[]? input)
    {
        if (input is null)
        {
            Head = null;
        }
        Head = new Node<T>(input[0]);
        var current = Head;
        Count = input.Count();
        for (var i = 1; i < input.Length; i++)
        {
            var newNode = new Node<T>(input[i]);
            current.Next = newNode;
            current = newNode;
            Tail = current;
        }
    }


    public bool IsReadOnly => false;

    public void Add(T? item)
    {
        AddToEnd(item);
    }

    public void AddToEnd(T? item)
    {
        var node = new Node<T>(item);
        Tail.Next = node;
        Tail = node;
        Count++;
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
        var current = Head;
        while (current is not null)
        {
            if (current.Value.Equals(item))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new NullReferenceException();
        }
        var current = Head;
        int index = arrayIndex;
        while (current is not null)
        {
            array[index] = current.Value;
            current = current.Next;
            index++;
        }
    }

    public bool Remove(T? item)
    //todo remove the first node whose data is equal to the given argument, including null
    {
        if (Head is null)
        {
            return false;
        }
        if (Head.Value.Equals(item))
        {
            Head = Head.Next;
            if (Head == null) Tail = null;
            Count--;
            return true;
        }

        var current = Head;
        while(current.Next is not null)
        {
            if(current.Next.Value.Equals(item))
            {
                current.Next = current.Next.Next;
                if(current.Next == null) Tail = current;
                Count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }
    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = Head;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public record Node<T>
{
    public T? Value { get; init; }
    public Node<T>? Next { get; set; }

    public Node(T? value)
    {
        Value = value;
    }

}