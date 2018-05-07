using System;
using NUnit.Framework;

namespace Framework.Errors.Tests
{
    [TestFixture]
    public class CoreErrorTests
    {
        [Test]
        public void CoreErrorAllHaveDescription()
        {
            var errors = Enum.GetValues(typeof (CoreError)) as CoreError[];

            Assert.NotNull(errors);
            foreach (var error in errors)
                Assert.False(error.ToError().Description.Contains("Can't get description"));
        }
    }
}
