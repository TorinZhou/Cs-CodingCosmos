using NUnit.Framework;
using FibonacciGenerator;

namespace FibonacciGeneratorTests
{
    [TestFixture]
    public class FibonacciTests
    {
        [TestCase(0)]
        public void Generate_ShallReturnNull_WhenInputIs_0()
        {
            var result = Fibonacci.Generate(0);
            Assert.IsEmpty(result);
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void Generate_ShallThrowArgumentException_WhenInputIsNegative(int n)
        {
            //TestDelegate action = () => Fibonacci.Generate(n);

            Assert.Throws<ArgumentException>(() => Fibonacci.Generate(n));
        }
        [TestCase(47)]
        [TestCase(48)]
        [TestCase(100)]
        [TestCase(1000)]
        public void Generate_ShallThrowArgumentException_WhenInputGreaterThanMaxValidNumber(int n)
        {
            Assert.Throws<ArgumentException>(() => Fibonacci.Generate(n));
        }
        [TestCase(1)]
        [TestCase(2)]
        public void Generate_ShallReturnZero_WhenInputEqualsOneOrTwo(int n)
        {
            var result = Fibonacci.Generate(n);
            switch (n)
            {
                case 1:
                    CollectionAssert.AreEqual(new List<int> { 0 }, result);
                    break;
                case 2:
                    CollectionAssert.AreEqual(new List<int> { 0, 1 }, result);
                    break;

            }
        }
        [TestCase(3, new int[] { 0, 1, 1 })]
        [TestCase(5, new int[] { 0, 1, 1, 2, 3 })]
        [TestCase(10, new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 })]
        public void Generate_ShallReturnCorrectResult_WhenInputIsValid(int n, int[] expected)
        {
            var result = Fibonacci.Generate(n);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Generate_ShallReturnCorrectLastNumber_WhenInputIsEdge()
        {
            var lastNumber = Fibonacci.Generate(46).Last();
            Assert.AreEqual(1134903170, lastNumber);
        }
    }
}