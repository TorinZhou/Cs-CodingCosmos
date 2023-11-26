namespace _30_Utilities
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    internal static class EnumerableExtensions
    {
        public static int SumOfEvenNumbers(this IEnumerable<int> numbers)
        {
            int sum = 0;
            //foreach (var number in numbers)
            //{
            //    if (number % 2 == 0)
            //    {
            //        sum += number;
            //    }
            //}
            return numbers.Where(number => IsEven(number)).Sum();
        }

        
        private static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
    }

}