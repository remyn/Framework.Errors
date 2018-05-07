using NUnit.Framework;

namespace Framework.Errors.Tests
{
    [TestFixture]
    public class ErrorTests
    {
        [Test]
        public void CreateErrorwWithValueOnly()
        {
            const uint errorNumber = 0x40201008;
            var error = new Error(errorNumber);

            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "0x"+ errorNumber.ToString("X8"));
            Assert.AreEqual(error.Description, "Error 0x" + errorNumber.ToString("X8"));
        }

        [Test]
        public void EqualityTest()
        {
            const uint errorNumber = 18;
            var errorFirst = new Error(errorNumber, "ERROR_18", "It is a big error");
            var errorSecond = new Error(errorNumber);

            Assert.AreEqual(errorFirst, errorFirst);
            Assert.AreEqual(errorFirst, errorSecond);
            Assert.True(errorFirst.Equals(errorSecond));
            Assert.True(errorFirst == errorFirst);
            Assert.True(errorFirst == errorSecond);
        }

        [Test]
        public void InequalityTest()
        {
            const uint errorFirstNumber = 80;
            const uint errorSecondNumber = 140;

            var errorFirst = new Error(errorFirstNumber);
            var errorSecond = new Error(errorSecondNumber, "ERROR_SECOND");

            Assert.AreNotEqual(errorFirst, null);
            Assert.AreNotEqual(null, errorSecond);
            Assert.AreNotEqual(errorFirst, errorSecond);
            Assert.False(errorSecond.Equals(errorFirst));
            Assert.True(errorFirst != errorSecond);
            Assert.True(errorFirst != null);
            Assert.True(null != errorFirst);
        }
    }
}
