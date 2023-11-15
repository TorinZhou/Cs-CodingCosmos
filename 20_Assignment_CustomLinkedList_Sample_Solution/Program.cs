using System.Collections;

var testLinkedList = new CustomLinkedList<int>(2, 3, 5, 7, 8);
testLinkedList.Add(1);
testLinkedList.AddToFront(99);
testLinkedList.AddToFront(98);
foreach (var item in testLinkedList)
{
    Console.WriteLine(item);
}

Console.WriteLine($"{testLinkedList.Remove(3)}");

Console.ReadKey();

public interface ILinkedList<T> : ICollection<T>
{
    void AddToFront(T? item);
    void AddToEnd(T? item);
}
public class CustomLinkedList<T> : ILinkedList<T?>
//! 1. changed T to T? to support better IDE auto-generated implementation of interfaces. (T -> T? in everywhere)
{
    public Node<T>? Head { get; private set; } //! 2. from set to private set
    public Node<T>? Tail { get; private set; }
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
    //! 1. implemented the method. Notice the init use of Head property of Node<T>
    {
        var newHead = new Node<T>(item)
        {
            Next = Head,
        };
        Head = newHead;
        if (Tail is null) //! 2. don't ignore the Tail
        {
            Tail = Head;
        }
        Count++;
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
        while (current.Next is not null)
        {
            if (current.Next.Value.Equals(item))
            {
                current.Next = current.Next.Next;
                if (current.Next == null) Tail = current;
                Count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }
    public IEnumerator<T?> GetEnumerator()
    {
        foreach(var node in GetNodes())
        {
            yield return node.Value;
        }
        //Node<T>? current = Head;
        //while (current is not null)
        //{
        //    yield return current.Value;
        //    current = current.Next;
        //}
    }
    private IEnumerable<Node<T>> GetNodes()
        //! added the ability to go through the Node
        //! Notice: it's private, we don't want to expose the underlying data structure(Nodes) of the implemetation(Linked List)
    {
        if (Head is null)
        {
            yield break; //! I'm not sure what's this break does
        }
        Node<T>? current = Head;
        while (current is not null)
        {
            yield return current;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Node<T>
//! 1. changed from record to class to support ref base equality
{
    public T? Value { get; set; } //! 2. changed from init to set
    public Node<T>? Next { get; set; }

    public Node(T? value)
    {
        Value = value;
    }

    //! 3. Include an override of ToString()
    public override string ToString() =>
        $"Value: {Value}" +
        $"Node: {((Next is null) ? null : Next.Value)}";
}

