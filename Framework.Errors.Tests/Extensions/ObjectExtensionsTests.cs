using System;
using NUnit.Framework;

namespace Framework.Core.Tests
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void ToSafeString()
        {
            string nullString = null;

            Assert.Null(nullString);
            Assert.AreEqual(nullString.ToSafeString().ToLower(), "null");
        }

        [Test]
        public void ToSafeStringWithFormat()
        {
            DateTime? nullDate = null;

            Assert.Null(nullDate);
            Assert.AreEqual(nullDate.ToSafeString("dd/MM/yyyy").ToLower(), "null");
        }

        [Test]
        public void ToSafeStringWithFormat_DoesnotReturnNullString()
        {
            var date = new DateTime(2124, 8, 24);

            Assert.NotNull(date);
            Assert.AreEqual(date.ToSafeString("dd/MM/yyyy"), "24/08/2124");
        }
    }
}
