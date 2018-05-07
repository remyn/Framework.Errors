using System;
using System.Runtime.Serialization;


namespace Framework.Errors
{
    /// <summary>
    /// Represents an Exception with Error information.
    /// The Exception supports the standard execption message and add an Error information.
    /// A descriptive message can be supplied in complement of the Error description.
    /// If no Error is provided, Error(0) = Unknown Error is generated.
    /// </summary>
    /// <remarks>
    /// The standard overload constructors are presents.
    /// The member Error is part of the serialisation process.
    /// </remarks>
    [Serializable]
    public class ErrorException : Exception
    {
        public Error Error { get; private set; }

        #region Contructors
        public ErrorException()
            : this(new Error())
        {
        }

        public ErrorException(string message)
            : this(message, new Error())
        {
        }

        public ErrorException(string message, Exception inner)
            : this(message, new Error(), inner)
        {
        }

        protected ErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Error = (Error)info.GetValue("Error", typeof(Error));
        }

        public ErrorException(Error error)
            : base(ResourceCoreStrings.ErrorException_DefaultMessage)
        {
            Error = error ?? new Error();
        }

        public ErrorException(string message, Error error)
            : base(message)
        {
            Error = error ?? new Error();
        }

        public ErrorException(Error error, Exception inner)
            : base(ResourceCoreStrings.ErrorException_DefaultMessage, inner)
        {
            Error = error ?? new Error();
        }

        public ErrorException(string message, Error error, Exception inner)
            : base(message, inner)
        {
            Error = error ?? new Error();
        }
        #endregion

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            base.GetObjectData(info, context);
            info.AddValue("Error", Error, typeof(Error));
        }

        public override string Message
        {
            get
            {
                var message = base.Message;
                if (Error == null)
                    return message;

                var errorMessage = string.Format(ResourceCoreStrings.Error_Prefix, Error);
                return (string.IsNullOrWhiteSpace(message))
                    ? errorMessage
                    : message + Environment.NewLine + errorMessage;
            }
        }
    }
}
