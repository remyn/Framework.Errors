using System;
using System.Linq;
using Framework.Core;
using NUnit.Framework;

namespace Framework.Errors.Tests
{
    [TestFixture]
    public class ContractTests
    {
        [Test]
        public void ThowExceptionWithNoParamters()
        {
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowException());

            Assert.AreEqual(exception.Error, ContractError.General.ToError());
        }

        [Test]
        public void ThrowExceptionWithMessageAndError()
        {
            var error = new Error(8);
            const string message = "A very useful message.";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowException(error, message));

            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, error);
        }

        [Test]
        public void ThrowExceptionWithFormattedMessageAndError()
        {
            var error = new Error(8);
            const string message = "A very useful message with parameters {0} and {1}.";
            const string first = "firstParam";
            const string second = "secondParam";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowException(error, message, first, second));

            Assert.True(exception.Message.StartsWith(string.Format(message, first, second)));
            Assert.AreEqual(exception.Error, error);
        }

        [Test]
        public void ThrowExceptionWithWrongFormattedMessageAndError()
        {
            var error = new Error(8);
            const string message = "A very useful message with parameters {0} and {3}.";
            const string first = "firstParam";
            const string second = "secondParam";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowException(error, message, first, second));

            Assert.True(exception.Message.Contains("parse the format"));
            Assert.AreEqual(exception.Error, error);
        }

        [Test]
        public void ThrowTypeException()
        {
            var exception = Assert.Throws<ErrorTestException>(() => Contract.ThrowException<ErrorTestException>());

            Assert.NotNull(exception);
        }

        [Test]
        public void ThrowWrongTypeException()
        {
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowException<WrongErrorTestException>());

            Assert.NotNull(exception);
            Assert.NotNull(exception.InnerException);
            Assert.IsInstanceOf<ErrorException>(exception.InnerException);
        }

        [Test]
        public void ThrowWrongTypeExceptionWithMessageandError()
        {
            const string message = "a useful message.";
            var error = ContractError.Unknown.ToError();
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowException<WrongErrorTestException>(error, message));

            Assert.NotNull(exception);
            Assert.NotNull(exception.InnerException);
            Assert.IsInstanceOf<ErrorException>(exception.InnerException);
        }

        [Test]
        public void ThrowIfNotNull_NotNullWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfNotNull(8, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfNotNull.ToError());
        }

        [Test]
        public void ThrowIfNotNull_NotNull_ShouldThrowException()
        {
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfNotNull("tada"));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfNotNull.ToError());
        }

        [Test]
        public void ThrowIfNotNull_NotNullWithError_ShouldThrowException()
        {
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfNotNull("tada", ContractError.Unknown.ToError()));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.Unknown.ToError());
        }

        [Test]
        public void ThrowIfNotNull_NotNullWithErrorAndMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfNotNull("tada", ContractError.Unknown.ToError(), message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.Unknown.ToError());
        }

        [Test]
        public void ThrowIfNotNull_Null_ShouldNotThrowException()
        {
            Contract.ThrowIfNotNull(null);
        }

        [Test]
        public void ThrowIfNotNullTyped_NotNullWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorTestException>(() => Contract.ThrowIfNotNull<ErrorTestException>("tada", message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfNotNull.ToError());
        }

        [Test]
        public void ThrowIfNull_NullWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfNull(null, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfNull.ToError());
        }

        [Test]
        public void ThrowIfNullTyped_NullWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorTestException>(() => Contract.ThrowIfNull<ErrorTestException>(null, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfNull.ToError());
        }

        [Test]
        public void ThrowIfNull_NotNull_ShouldNotThrowException()
        {
            Contract.ThrowIfNull(124);
        }

        [Test]
        public void ThrowIfFalse_FalseWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfFalse(false, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfFalse.ToError());
        }

        [Test]
        public void ThrowIfFalseTyped_FalseWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorTestException>(() => Contract.ThrowIfFalse<ErrorTestException>(false, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfFalse.ToError());
        }

        [Test]
        public void ThrowIfFalse_NotFalse_ShouldNotThrowException()
        {
            Contract.ThrowIfFalse(true);
        }

        [Test]
        public void ThrowIfTrue_TrueWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfTrue(true, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfTrue.ToError());
        }

        [Test]
        public void ThrowIfTrueTyped_TrueWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorTestException>(() => Contract.ThrowIfTrue<ErrorTestException>(true, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfTrue.ToError());
        }

        [Test]
        public void ThrowIfTrue_NotTrue_ShouldNotThrowException()
        {
            Contract.ThrowIfTrue(false);
        }

        [Test]
        public void ThrowIfStringIsNullOrWhiteSpace_EmptyWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfStringIsNullOrWhiteSpace("     ", message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfStringIsNullOrWhiteSpace.ToError());
        }

        [Test]
        public void ThrowIfStringIsNullOrWhiteSpaceTyped_NullWithMessage_ShouldThrowException()
        {
            const string message = "From unit test.";
            var exception = Assert.Throws<ErrorTestException>(() => Contract.ThrowIfStringIsNullOrWhiteSpace<ErrorTestException>(null, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfStringIsNullOrWhiteSpace.ToError());
        }

        [Test]
        public void ThrowIfStringIsNullOrWhiteSpace_NotNull_ShouldNotThrowException()
        {
            Contract.ThrowIfStringIsNullOrWhiteSpace("a valid string.");
        }

        [Test]
        public void ThrowIfEqual_DefaultComparer()
        {
            const int left = 2;
            const int right = 2;

            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfEqual(left, right));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfEqual.ToError(left, right));
        }

        [Test]
        public void ThrowIfEqual_DefaultComparer_AsNull()
        {
            int? left = null;
            int? right = null;

            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfEqual(left, right));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfEqual.ToError(left.ToSafeString(), right.ToSafeString()));
        }

        [Test]
        public void ThrowIfEqual_WithComparerAndMessage()
        {
            const string message = "a useful message.";
            var left = new ItemTest() { Value = 4 };
            var right = new ItemTest() { Value = 4 };

            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfEqual(left, right, new ItemTestEqualityComparer(), null, message));

            Assert.NotNull(exception);
            Assert.True(exception.Message.StartsWith(message));
            Assert.AreEqual(exception.Error, ContractError.ThrowIfEqual.ToError(left.ToSafeString(), right.ToSafeString()));
        }

        [Test]
        public void ThrowIfEqual_ReferenceComparerWIthMessage_ShouldNotThrowException()
        {
            const string message = "a useful message.";
            var left = new ItemTest() { Value = 4 };
            var right = new ItemTest() { Value = 4 };

            Contract.ThrowIfEqual(left, right, message);
        }

        [Test]
        public void ThrowIfEqualTyped_DefaultComparer()
        {
            const string left = "identical";
            const string right = "identical";

            var exception = Assert.Throws<ErrorTestException>(() => Contract.ThrowIfEqual<string, ErrorTestException>(left, right));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfEqual.ToError(left, right));
        }

        [Test]
        public void ThrowIfNotEqual_DefaultComparer()
        {
            const int left = 1;
            const int right = 2;

            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfNotEqual(left, right));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfNotEqual.ToError(left, right));
        }

        [Test]
        public void ThrowIfNotEqual_WithComparer_ShouldNotThrowException()
        {
            var left = new ItemTest() { Value = 4 };
            var right = new ItemTest() { Value = 4 };

            Contract.ThrowIfNotEqual(left, right, new ItemTestEqualityComparer());
        }

        [Test]
        public void ThrowIfNotType()
        {
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfNotType<TypeTest>(new ItemTest()));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfNotType.ToError());
        }

        [Test]
        public void ThrowIfNotType_ShouldNotThrowException()
        {
            var type = Contract.ThrowIfNotType<ITypeTest>(new TypeTest());

            Assert.IsAssignableFrom<TypeTest>(type);
        }

        [Test]
        public void ThrowIfNotType_WithNull_ShouldNotThrowException()
        {
            var type = Contract.ThrowIfNotType<ITypeTest>(null);

            Assert.Null(type);
        }

        [Test]
        public void ThrowIfInvalidEnumValue()
        {
            var exception = Assert.Throws<ErrorException>(() => Contract.ThrowIfInvalidEnumValue<EnumTest>(Enum.GetValues(typeof(EnumTest)).Cast<int>().Max() + 1));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfInvalidEnumValue.ToError());
        }

        [Test]
        public void ThrowIfInvalidEnumValueTyped()
        {
            var value = Enum.GetValues(typeof (EnumTest)).Cast<int>().Max() + 1;
            var exception = Assert.Throws<ErrorTestException>(
                () => Contract.ThrowIfInvalidEnumValue<EnumTest, ErrorTestException>(value));

            Assert.NotNull(exception);
            Assert.AreEqual(exception.Error, ContractError.ThrowIfInvalidEnumValue.ToError());
        }

        [Test]
        public void ThrowIfInvalidEnumValue_ShouldNotThrowException()
        {
            var value = Enum.GetValues(typeof (EnumTest)).Cast<int>().First();
            var enumType = Contract.ThrowIfInvalidEnumValue<EnumTest>(value);

            Assert.AreEqual((int)enumType, value);
        }
    }
}
