using System.Collections;

var testLinkedList = new CustomLinkedList<int>(2, 3, 5, 7, 8);
testLinkedList.AddToFront(99);
testLinkedList.AddToFront(98);
testLinkedList.Add(9998);
testLinkedList.Add(9998);
testLinkedList.AddToEnd(9998);
Console.WriteLine(testLinkedList.Contains(7));
var arr = new int[9999];
testLinkedList.CopyTo(arr, 2);

foreach (var item in testLinkedList)
{
    Console.WriteLine(item);
}


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
        var newNode = new Node<T>(item);
        if (Head is null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            Tail = newNode;
        }
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
        //! this doesn't work because the IEnumerator is breaked
        //foreach(var node in GetNodes())
        //{
        //    node.Next = null; 
        //}
        //Head = null;
        //Tail = null;
        //---------------------------------------------------------
        //! this approach is slow and space-consuming because it builds a List
        //foreach(var node in GetNodes().ToList())
        //{
        //    node.Next = null; 
        //}
        //Head = null;
        //Tail = null;
        Node<T>? current = Head;
        while (current is not null)
        {
            Node<T>? temp = current;
            current = current.Next;
            temp.Next = null;
        }
    }

    public bool Contains(T? item)
    {
        //var current = Head;
        //while (current is not null)
        //{
        //    if (current.Value.Equals(item))
        //    {
        //        return true;
        //    }
        //    current = current.Next;
        //}
        //return false;
        if(item is null)
        {
            //! here, we can't use GetNodes().Contains(), because Contains checked the equality right away by ref, in our case, we have to provide a more complex check. so we need the Any
            //! btw, we're benifiting from implemented the GetNodes() method previously. Because we noe can use LINQ.
            return GetNodes().Any(node => node.Value is null);
        }
        return GetNodes().Any(node => item.Equals(node.Value));
    }

    public void CopyTo(T?[]? array, int arrayIndex) //! add ? after T, the array could be an array of null
    {
        //var current = Head;
        //int index = arrayIndex;
        //while (current is not null)
        //{
        //    array[index] = current.Value;
        //    current = current.Next;
        //    index++;
        //}
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }
        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }
        if(array.Length < Count + arrayIndex)
        {
            throw new ArgumentException("Array is not long engough");
        }
        foreach (var item in GetNodes())
        {
            array[arrayIndex] = item.Value;
            ++arrayIndex;
        }
    }

    public bool Remove(T? item)
    //todo remove the first node whose data is equal to the given argument, including null
    {
        //if (Head is null)
        //{
        //    return false;
        //}
        //if (Head.Value.Equals(item))
        //{
        //    Head = Head.Next;
        //    if (Head == null) Tail = null;
        //    Count--;
        //    return true;
        //}

        //var current = Head;
        //while (current.Next is not null)
        //{
        //    if (current.Next.Value.Equals(item))
        //    {
        //        current.Next = current.Next.Next;
        //        if (current.Next == null) Tail = current;
        //        Count--;
        //        return true;
        //    }
        //    current = current.Next;
        //}
        Node<T>? predecessor = null;
        foreach (var node in GetNodes())
        {
            if ((node.Value is null && item is null) ||
               (node.Value is not null && node.Value.Equals(item)))
            {
                if (predecessor == null)
                {
                    Head = node;
                    Count--;
                }
                else
                {
                    predecessor.Next = node.Next;
                }
                Count--;
                break;
            }
            predecessor = node;
        }
        return false;
    }
    public IEnumerator<T?> GetEnumerator()
    {
        //! since we implemented the GetNodes, now we simple use it to go one step further and get the value.
        //! we provide the logic, in this case, simply call node.Value
        //! the yield will manage the heavylifting of implementing MoveNext(), Current(),
        foreach (var node in GetNodes())
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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    private IEnumerable<Node<T>> GetNodes()
    //! added the ability to go through the Node
    //! Notice: it's private, we don't want to expose the underlying data structure(Nodes) of the implemetation(Linked List)
    {
        //if (Head is null)
        //{
        //    yield break; //! I'm not sure what's this break does
        //    //! in a Iterator method, yield break signals the end of the sequence.
        //    //! State Machine: When yield break is encountered, it sets the state machine to a final state, inicating there are no more elements to yield.
        //}
        Node<T>? current = Head;
        while (current is not null)
        {
            yield return current;
            current = current.Next;
        }
    }
}

public class Node<T>
//! 1. changed from record to class to support ref base equality
{
    public T? Value { get; } //! 2. changed from init to set
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

