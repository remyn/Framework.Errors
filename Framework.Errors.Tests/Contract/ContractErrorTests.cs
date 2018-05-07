using System;
using NUnit.Framework;

namespace Framework.Errors.Tests
{
    [TestFixture]
    public class ContractErrorTests
    {
        [Test]
        public void CoreErrorAllHaveDescription()
        {
            var errors = Enum.GetValues(typeof(ContractError)) as ContractError[];

            Assert.NotNull(errors);
            foreach (var error in errors)
                Assert.False(error.ToError().Description.Contains("Can't get description"));
        }
    }
}
