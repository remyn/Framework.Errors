using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace Framework.Errors.Tests
{
    [TestFixture]
    public class ErrorExceptionTests
    {
        private Stream Serialize(object source)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            return stream;
        }
        
        private T Deseriaze<T>(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            return (T) formatter.Deserialize(stream);
        }

        [Test]
        public void CreateNoParamters()
        {
            var exception = new ErrorException();

            Assert.NotNull(exception);
            Assert.NotNull(exception.Error);
            Assert.AreEqual(exception.Error.Value, (uint)0);
            Assert.True(exception.Message.Contains("has been throw"));
        }

        [Test]
        public void CreateWithMessage()
        {
            const string message = "my Message";
            var exception = new ErrorException(message);

            Assert.NotNull(exception);
            Assert.NotNull(exception.Error);
            Assert.AreEqual(exception.Error.Value, (uint)0);
            Assert.True(exception.Message.StartsWith(message));
        }

        [Test]
        public void CreateWithMessageAndInnerException()
        {
            const string message = "my Message";
            const string innerMessage = "The inner exception message";
            var inner = new Exception(innerMessage);
            var exception = new ErrorException(message, inner);

            Assert.NotNull(exception);
            Assert.NotNull(exception.Error);
            Assert.NotNull(exception.InnerException);
            Assert.AreEqual(exception.InnerException.Message, innerMessage);
            Assert.AreEqual(exception.Error.Value, (uint)0);
            Assert.True(exception.Message.StartsWith(message));
        }

        [Test]
        public void ErrorExceptionSerialisation_ShouldIncludeError()
        {
            var exception = new ErrorException();
            var clone = Deseriaze<ErrorException>(Serialize(exception));

            Assert.AreEqual(exception.Message, clone.Message);
            Assert.AreEqual(exception.Error, clone.Error);
            Assert.AreEqual(exception.InnerException, clone.InnerException);
        }

        [Test]
        public void CreateWithError()
        {
            var exception = new ErrorException(new Error(8));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Error);
            Assert.AreEqual(exception.Error.Value, (uint)8);
            Assert.True(exception.Message.Contains("has been throw"));
        }

        [Test]
        public void CreateWithErrorAndMessage()
        {
            const string message = "A super useful message";
            var exception = new ErrorException(message, new Error(24));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Error);
            Assert.AreEqual(exception.Error.Value, (uint)24);
            Assert.True(exception.Message.StartsWith(message));
        }

        [Test]
        public void CreateWithErrorAndInnerException()
        {
            const string innerMessage = "The inner exception message.";
            var exception = new ErrorException(new Error(8), new Exception(innerMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Error);
            Assert.AreEqual(exception.Error.Value, (uint)8);
            Assert.True(exception.Message.Contains("has been throw"));
            Assert.NotNull(exception.InnerException);
            Assert.AreEqual(exception.InnerException.Message, innerMessage);
        }

        [Test]
        public void CreateWithErrorAndMessageAndInnerException()
        {
            const string innerMessage = "The inner exception message.";
            const string message = "My useful message";
            var exception = new ErrorException(message, new Error(128), new Exception(innerMessage));

            Assert.NotNull(exception);
            Assert.NotNull(exception.Error);
            Assert.AreEqual(exception.Error.Value, (uint)128);
            Assert.True(exception.Message.StartsWith(message));
            Assert.NotNull(exception.InnerException);
            Assert.AreEqual(exception.InnerException.Message, innerMessage);
        }
    }
}
