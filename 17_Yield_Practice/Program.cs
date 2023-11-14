//! playground 1 : Iterator without loop
//var digitNumbers = GetSingleDigitNumbers();
//foreach (var digitNumber in digitNumbers)
//{
//    Console.WriteLine(digitNumber);
//}

//! playground 2 : Implement Distict from Linq
//var input = new[] { "a", "b", "c", "d", "a", "b", "c", "d", };
//foreach (var item in Distinct(input))
//{
//    Console.WriteLine(item);
//}

//! Playground 3 : Return all numbers before the first negative number
var numbers = new int[] { 1, 2, 3, 4, -5, 6, 7, 8, 9, -2, -3, -4, -5 };
foreach (var number in IntsBeforeTheFirstNegative(numbers))
{
    Console.WriteLine(number);
}

Console.ReadKey();

IEnumerable<int> GetSingleDigitNumbers()
{
    //! This shows that iterater not necessary contain a loop 
    yield return 0;
    yield return 1;
    yield return 2;
    yield return 3;
    yield return 4;
    yield return 5;
    yield return 6;
    yield return 7;
    yield return 8;
    yield return 9;
}

IEnumerable<T> Distinct<T>(IEnumerable<T> input)
{
    //var inputHashSet = new HashSet<T>(input);
    //return inputHashSet.ToArray();
    var hashSet = new HashSet<T>();
    foreach (var item in input)
    {
        if (!hashSet.Contains(item))
        {
            hashSet.Add(item);
            yield return item;
            Console.WriteLine("After yield"); // this line will run no problem
        }
    }
}
IEnumerable<int> IntsBeforeTheFirstNegative(IEnumerable<int> input)
{
    foreach (var item in input)
    {
        if (item >= 0)
        {
            yield return item;
            Console.WriteLine("After yield");
        }
        else
        {
            yield break;
        }

    }
}static IEnumerable<T> GetAllAfterLastNullReversed<T>(IList<T> input)
{
    //your code goes here
    for(int i = input.Count - 1; i >= 0; i--)
    {
        if (input[i] != null)
        {
            yield return input[i];
        }
        else
        {
            yield break; 
        }
    }
}