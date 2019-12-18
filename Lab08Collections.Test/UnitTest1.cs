using NUnit.Framework;
using System.Collections.Generic;

namespace Lab08Collections.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TotalTest()
        {
            Assert.AreEqual(365, ArrayListDictionary.GetTotal(2, 3, 4, 5, 6));
        }

        [TestCase(1, 2, 3, 4, 5, 280)]
        [TestCase(2, 3, 4, 5, 6, 365)]
        [TestCase(3, 4, 5, 6, 7, 460)]
        [TestCase(0, 10, 100, 1000, 10000, 101121275)]
        [TestCase(-1, -4, -9, -16, -25, 504)]
        [TestCase(8, 5, 11, 7, 16, 1060)]

        public void ArrayListDictionaryGetTotal(int a, int b, int c, int d, int e, int expected)
        {
            int actual = ArrayListDictionary.GetTotal(a, b, c, d, e);
            Assert.AreEqual(expected, actual);
        }
    }
}

namespace Lab09RabbitTest.Test
{
    [TestFixture]
    
    public class Tests
    {
        [TestCase(3, 3, 1)]
        [TestCase(4, 4, 2)]
        [TestCase(5, 6, 3)]
        [TestCase(6, 9, 4)]
        [TestCase(7, 13, 5)]
        [TestCase(8, 18, 7)]

        public void RabbitGrowthTest(int totalYears,int expectedRabbitAge, int expectedCount)
        {
            (int actualRabbitAge, int actualCount) = RabbitCollection.MultiplyRabbits(totalYears);
            Assert.AreEqual(expectedRabbitAge, actualRabbitAge);
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}

namespace Lab17NorthwindTests.Test
{
    [TestFixture]

    public class Tests
    {
        [TestCase(null, 91)]
        [TestCase("London", 6)]

        public void TetsNumberOfNorthwindCustomers(string city, int expected)
        {
            //arrange
            var testInstance = new Lab14LINQ.NorthwindCustomerCollection();
            //act
            int actual = testInstance.peopleCount(city);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}

namespace Lab20NorthwindProducts.Test
{
    [TestFixture]
    public class Tests
    {
        [TestCase("p",3)]
        [TestCase("L",5)]
        [TestCase("b",1)]
        public void StartingLetterTest(string letter, int expected)
        {
            var testInstance = new ProductTransformations();
            int actual = testInstance.StartingLetter(letter);
            Assert.AreEqual(expected, actual);
        }
        [TestCase("p",17)]
        [TestCase("a",58)]
        [TestCase("d",30)]
        public void ContainingLetterTest(string letter, int expected)
        {
            var testInstance = new ProductTransformations();
            int actual = testInstance.ContainingLetter(letter);
            Assert.AreEqual(expected, actual);
        }
    }
}