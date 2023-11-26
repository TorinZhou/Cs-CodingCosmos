using NUnit.Framework;
using _30_Utilities;

namespace _30_UtilitiesTests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {

        [Test]
        public void SumOfEvenNumbers_ShallReturnZero_ForEmptyCollection()
        {
            var input = Enumerable.Empty<int>();

            var result = input.SumOfEvenNumbers();

            Assert.AreEqual(0, result);
        }

        [TestCaseSource(nameof(GetSumOfEvenNumbersTestCases))]
        //! we use TestCaseSource to overcome the limitation of attributes(they only take simple types and array of simple types)
        //! what's the advantage of using nameof()
        public void SumOfEvenNumbers_ShallReturnNonZeroResult_IfEvenNumbersArePresent(IEnumerable<int> input, int expected)
        {

            var result = input.SumOfEvenNumbers();

            var inputAsString = string.Join(",", input);

            Assert.AreEqual(expected, result, $"For input {inputAsString}, " +
                $"the result shall be {expected} but it was {result}.");
        }
        private static IEnumerable<object> GetSumOfEvenNumbersTestCases()
        //! why it must be static, and must implementing IEnumerable
        {
            return new[]
            {
                new object[] { new int[] { 4, 6, 3 }, 10 },
                new object[] { new List<int> { 100, 200, 1 }, 300 },
                //! what's these two wierd way of creating []
            };
        }
        [TestCase(8)]
        [TestCase(-8)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(0)]
        public void SumOfEvenNumbers_ShallReturnValueOfTheOnlyItem_IfItIsEven(int number)
        {
            var input = new int[] { number, };

            var result = input.SumOfEvenNumbers();

            Assert.AreEqual(number, result);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(-1)]
        [TestCase(-9)]
        [TestCase(7)]
        public void SumOfEvenNumbers_ShallReturnZero_ForSingleOddNumber(int number)
        {
            var input = new int[] { number, };

            var result = input.SumOfEvenNumbers();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void SumOfEvenNumbers_ShallThrowException_ForNullInput()
        {
            IEnumerable<int>? input = null;


            var exception = Assert.Throws<ArgumentNullException>(
                () => input!.SumOfEvenNumbers());

            
        }
    }
}