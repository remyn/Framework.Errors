
namespace Framework.Errors.Tests
{
    public class WrongErrorTestException : ErrorException
    {
    }

    public class ErrorTestException : ErrorException
    {
        public ErrorTestException(string message, Error error)
            : base(message, error)
        {
        }
    }
}
