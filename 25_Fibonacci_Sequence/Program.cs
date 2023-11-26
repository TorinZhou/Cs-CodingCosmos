
using System.Text;
using System.Xml.Schema;

//var resultSequence = CheckedFibonacciExercise.GetFibonacci(-3);
//var sb = new StringBuilder();
//foreach (var fibonacci in resultSequence)
//{
//    sb.Append($"{fibonacci} ");
//}
//Console.WriteLine(sb.ToString());


Console.ReadLine();

public static class CheckedFibonacciExercise
{
    public static IEnumerable<int> GetFibonacci(int n)
    {
        //your code goes here
        checked
        {
            int seedOne = 0;
            int seedTwo = 1;
            int current;
            if (n >= 1)
            {
                yield return seedOne;
            }
            if (n >= 2)
            {
                yield return seedTwo;
            }

            for (int i = 3; i <= n; i++)
            {
                current = seedOne + seedTwo;
                seedOne = seedTwo;
                seedTwo = current;
                yield return current;
            }
        }

    }
}

public static class FloatingPointNumbersExercise
{
    public static bool IsAverageEqualTo(
        this IEnumerable<double> input, double valueToBeChecked)
    {
        //your code goes here
        if ( input.Any(x => double.IsNaN(x) || double.IsInfinity(x)))
        {
            throw new ArgumentException();
        }
        return Math.Abs(input.Average() - valueToBeChecked) < 0.00001;
    }
}