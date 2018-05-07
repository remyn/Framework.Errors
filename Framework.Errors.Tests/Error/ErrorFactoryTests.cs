using System.Resources;
using NUnit.Framework;

namespace Framework.Errors.Tests
{
    [TestFixture]
    public class ErrorFactoryTests
    {
        [Test]
        public void CreateErrorFromResourceWithNullResourceManager()
        {
            const uint errorNumber = 4;
            var error = Error.Factory.FromResource(errorNumber, "ResourceName", null);

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "ResourceName");
            Assert.True(error.Description.Contains("Parameter 'resManager' is null"));
        }

        [Test]
        public void CreateErrorFromResourceWithNotExistingResourceManager()
        {
            const uint errorNumber = 8;
            var error = Error.Factory.FromResource(errorNumber, "ResourceName", new ResourceManager(typeof(string)));

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "ResourceName");
            Assert.True(error.Description.Contains("Can't get description"));
        }

        [Test]
        public void CreateErrorFromResourceWithNotExistingKey()
        {
            const uint errorNumber = 8;
            var error = Error.Factory.FromResource(errorNumber, "ResourceName", ResourceErrorFactoryTest.ResourceManager);

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "ResourceName");
            Assert.True(error.Description.Contains("The description can't be found."));
        }

        [Test]
        public void CreateErrorFromResourceWithExistingKeyButWrongFormatWithoutArgs()
        {
            const uint errorNumber = 8;
            var error = Error.Factory.FromResource(errorNumber, "KeyWithWrongFormat", ResourceErrorFactoryTest.ResourceManager);

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "KeyWithWrongFormat");
            Assert.False(error.Description.Contains("Can't get description"));
        }

        [Test]
        public void CreateErrorFromResourceWithExistingKeyButWrongFormatWithArgs()
        {
            const uint errorNumber = 8;
            var error = Error.Factory.FromResource(errorNumber, "KeyWithWrongFormat", ResourceErrorFactoryTest.ResourceManager, "Only one param");

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "KeyWithWrongFormat");
            Assert.True(error.Description.Contains("Can't get description"));
        }

        [Test]
        public void CreateErrorFromResourceWithEmptyKey()
        {
            const uint errorNumber = 8;
            var error = Error.Factory.FromResource(errorNumber, "", ResourceErrorFactoryTest.ResourceManager);

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "0x00000008");
            Assert.True(error.Description.Contains("Parameter 'name' is null"));
        }

        [Test]
        public void CreateErrorFromResourceWithNullKey()
        {
            const uint errorNumber = 0x801;
            var error = Error.Factory.FromResource(errorNumber, null, ResourceErrorFactoryTest.ResourceManager);

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, "0x00000801");
            Assert.True(error.Description.Contains("Parameter 'name' is null"));
        }

        [Test]
        public void CreateErrorFromResource()
        {
            const uint errorNumber = 0x124;
            const string errorKey = "Key1";
            var error = Error.Factory.FromResource(errorNumber, errorKey, ResourceErrorFactoryTest.ResourceManager);

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, errorKey);
            Assert.AreEqual(error.Description, ResourceErrorFactoryTest.Key1);
        }

        [Test]
        public void CreateErrorFromResourceWithFormat()
        {
            const uint errorNumber = 0x80000001;
            const string errorKey = "KeyWithFormat";
            const string param1 = "firstParam";
            const string param2 = "secondParam";
            var error = Error.Factory.FromResource(errorNumber, errorKey, ResourceErrorFactoryTest.ResourceManager,
                param1, param2);

            Assert.NotNull(error);
            Assert.AreEqual(error.Value, errorNumber);
            Assert.AreEqual(error.Name, errorKey);
            Assert.AreEqual(error.Description, string.Format(ResourceErrorFactoryTest.KeyWithFormat, param1, param2));
        }
    }
}
