using System;
using System.Collections.Generic;
using Framework.Core;

namespace Framework.Errors
{
    /// <summary>
    /// Represents a simple way to verify that a specific condition is encountered - otherwise raise an excpetion.
    /// for example, ensure the parameters of a method are as expected, before to proceed.
    /// </summary>
    /// <remarks>
    /// The exception type could be specified, but it has to derivate from ErrorException with an Error and message constructor parameters.
    /// </remarks>
    public static class Contract
    {
        #region ThrowIfNotNull
        public static void ThrowIfNotNull(object value, string message = null, params object[] args)
        {
            ThrowIfNotNull(value, null, message, args);
        }

        public static void ThrowIfNotNull(object value, Error error, string message = null, params object[] args)
        {
            if (value != null)
                ThrowException(error ?? ContractError.ThrowIfNotNull.ToError(value.GetType().FullName, value), message, args);
        }

        public static void ThrowIfNotNull<T>(object value, string message = null, params object[] args)
            where T : ErrorException
        {
            ThrowIfNotNull<T>(value, null, message, args);
        }

        public static void ThrowIfNotNull<T>(object value, Error error, string message = null, params object[] args)
            where T : ErrorException
        {
            if (value != null)
                ThrowException<T>(error ?? ContractError.ThrowIfNotNull.ToError(value.GetType().FullName, value), message, args);
        }
        #endregion

        #region ThrowIfNull
        public static void ThrowIfNull(object value, string message = null, params object[] args)
        {
            ThrowIfNull(value, null, message, args);
        }

        public static void ThrowIfNull(object value, Error error, string message = null, params object[] args)
        {
            if (value == null)
                ThrowException(error ?? ContractError.ThrowIfNull.ToError(), message, args);
        }

        public static void ThrowIfNull<T>(object value, string message = null, params object[] args)
            where T : ErrorException
        {
            ThrowIfNull<T>(value, null, message, args);
        }

        public static void ThrowIfNull<T>(object value, Error error, string message = null, params object[] args)
            where T : ErrorException
        {
            if (value == null)
                ThrowException<T>(error ?? ContractError.ThrowIfNull.ToError(), message, args);
        }
        #endregion

        #region ThrowIfFalse
        public static void ThrowIfFalse(bool value, string message = null, params object[] args)
        {
            ThrowIfFalse(value, null, message, args);
        }

        public static void ThrowIfFalse(bool value, Error error, string message = null, params object[] args)
        {
            if (value == false)
                ThrowException(error ?? ContractError.ThrowIfFalse.ToError(), message, args);
        }

        public static void ThrowIfFalse<T>(bool value, string message = null, params object[] args)
            where T : ErrorException
        {
            ThrowIfFalse<T>(value, null, message, args);
        }

        public static void ThrowIfFalse<T>(bool value, Error error, string message = null, params object[] args)
            where T : ErrorException
        {
            if (value == false)
                ThrowException<T>(error ?? ContractError.ThrowIfFalse.ToError(), message, args);
        }
        #endregion

        #region ThrowIfTrue
        public static void ThrowIfTrue(bool value, string message = null, params object[] args)
        {
            ThrowIfTrue(value, null, message, args);
        }

        public static void ThrowIfTrue(bool value, Error error, string message = null, params object[] args)
        {
            if (value == true)
                ThrowException(error ?? ContractError.ThrowIfTrue.ToError(), message, args);
        }

        public static void ThrowIfTrue<T>(bool value, string message = null, params object[] args)
            where T : ErrorException
        {
            ThrowIfTrue<T>(value, null, message, args);
        }

        public static void ThrowIfTrue<T>(bool value, Error error, string message = null, params object[] args)
            where T : ErrorException
        {
            if (value == true)
                ThrowException<T>(error ?? ContractError.ThrowIfTrue.ToError(), message, args);
        }
        #endregion

        #region ThrowIfStringIsNullOrWhiteSpace
        public static void ThrowIfStringIsNullOrWhiteSpace(string value, string message = null, params object[] args)
        {
            ThrowIfStringIsNullOrWhiteSpace(value, null, message, args);
        }

        public static void ThrowIfStringIsNullOrWhiteSpace(string value, Error error, string message = null, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(value))
                ThrowException(error ?? ContractError.ThrowIfStringIsNullOrWhiteSpace.ToError(), message, args);
        }

        public static void ThrowIfStringIsNullOrWhiteSpace<T>(string value, string message = null, params object[] args)
            where T : ErrorException
        {
            ThrowIfStringIsNullOrWhiteSpace<T>(value, null, message, args);
        }

        public static void ThrowIfStringIsNullOrWhiteSpace<T>(string value, Error error, string message = null, params object[] args)
            where T : ErrorException
        {
            if (string.IsNullOrWhiteSpace(value))
                ThrowException<T>(error ?? ContractError.ThrowIfStringIsNullOrWhiteSpace.ToError(), message, args);
        }
        #endregion

        #region ThrowIfEqual
        public static void ThrowIfEqual<T>(T left, T right, string message = null, params object[] args)
        {
            ThrowIfEqual(left, right, null, null, message, args);
        }

        public static void ThrowIfEqual<T>(T left, T right, IEqualityComparer<T> comparer, Error error, string message = null, params object[] args)
        {
            var comparator = comparer ?? EqualityComparer<T>.Default;
            if (comparator.Equals(left, right))
                ThrowException(error ?? ContractError.ThrowIfEqual.ToError(left.ToSafeString(), right.ToSafeString()), message, args);
        }

        public static void ThrowIfEqual<T, E>(T left, T right, string message = null, params object[] args)
            where E : ErrorException
        {
            ThrowIfEqual<T, E>(left, right, null, null, message, args);
        }

        public static void ThrowIfEqual<T, E>(T left, T right, IEqualityComparer<T> comparer, Error error, string message = null, params object[] args)
            where E : ErrorException
        {
            var comparator = comparer ?? EqualityComparer<T>.Default;
            if (comparator.Equals(left, right))
                ThrowException<E>(error ?? ContractError.ThrowIfEqual.ToError(left.ToSafeString(), right.ToSafeString()), message, args);
        }
        #endregion

        #region ThrowIfNotEqual
        public static void ThrowIfNotEqual<T>(T left, T right, string message = null, params object[] args)
        {
            ThrowIfNotEqual(left, right, null, null, message, args);
        }

        public static void ThrowIfNotEqual<T>(T left, T right, IEqualityComparer<T> comparer, string message = null, params object[] args)
        {
            ThrowIfNotEqual(left, right, comparer, null, message, args);
        }

        public static void ThrowIfNotEqual<T>(T left, T right, IEqualityComparer<T> comparer, Error error, string message = null, params object[] args)
        {
            var comparator = comparer ?? EqualityComparer<T>.Default;
            if (comparator.Equals(left, right) == false)
                ThrowException(error ?? ContractError.ThrowIfNotEqual.ToError(left.ToSafeString(), right.ToSafeString()), message, args);
        }

        public static void ThrowIfNotEqual<T, E>(T left, T right, string message = null, params object[] args)
            where E : ErrorException
        {
            ThrowIfNotEqual<T, E>(left, right, null, null, message, args);
        }

        public static void ThrowIfNotEqual<T, E>(T left, T right, IEqualityComparer<T> comparer, string message = null, params object[] args)
            where E : ErrorException
        {
            ThrowIfNotEqual<T, E>(left, right, comparer, null, message, args);
        }

        public static void ThrowIfNotEqual<T, E>(T left, T right, IEqualityComparer<T> comparer, Error error, string message = null, params object[] args)
            where E : ErrorException
        {
            var comparator = comparer ?? EqualityComparer<T>.Default;
            if (comparator.Equals(left, right) == false)
                ThrowException<E>(error ?? ContractError.ThrowIfNotEqual.ToError(left.ToSafeString(), right.ToSafeString()), message, args);
        }
        #endregion

        #region ThrowIfNotType
        public static T ThrowIfNotType<T>(object value, string message = null, params object[] args)
        {
            return ThrowIfNotType<T>(value, null, message, args);
        }

        public static T ThrowIfNotType<T>(object value, Error error, string message = null, params object[] args)
        {
            var type = default(T);

            try
            {
                type = (T) value;
            }
            catch (InvalidCastException)
            {
                // Notice that value will never be null here, because a cast of a null value will always succeed.
                ThrowException(error ?? ContractError.ThrowIfNotType.ToError(value.GetType().FullName, typeof(T).FullName), message, args);
            }

            return type;
        }

        public static T ThrowIfNotType<T, E>(object value, string message = null, params object[] args)
            where E : ErrorException
        {
            return ThrowIfNotType<T, E>(value, null, message, args);
        }

        public static T ThrowIfNotType<T, E>(object value, Error error, string message = null, params object[] args)
            where E : ErrorException
        {
            var type = default(T);

            try
            {
                type = (T)value;
            }
            catch (InvalidCastException)
            {
                ThrowException<E>(error ?? ContractError.ThrowIfNotType.ToError(value.GetType().FullName, typeof(T).FullName), message, args);
            }

            return type;
        }
        #endregion

        #region ThrowIfInvalidEnumValue
        public static T ThrowIfInvalidEnumValue<T>(object value, string message = null, params object[] args)
            where T : struct, IComparable, IConvertible, IFormattable // the closest to Enum for compile time check
        {
            return ThrowIfInvalidEnumValue<T>(value, null, message, args);
        }

        public static T ThrowIfInvalidEnumValue<T>(object value, Error error, string message = null, params object[] args)
            where T : struct, IComparable, IConvertible, IFormattable // the closest to Enum for compile time check
        {
            ThrowIfFalse(typeof(T).IsEnum, error, message, args);
            T? enumType = null;

            try
            {
                var enumNumericValue = Convert.ChangeType(value, typeof (T).GetEnumUnderlyingType());
                if (Enum.IsDefined(typeof (T), enumNumericValue))
                    enumType =  (T) Enum.ToObject(typeof (T), value);
            }
            catch
            {
                enumType = null;
            }

            if (enumType.HasValue == false)
                ThrowException(error ?? ContractError.ThrowIfInvalidEnumValue.ToError(value.ToSafeString(), typeof(T).FullName));

            // ReSharper disable once PossibleInvalidOperationException
            // enumType can't be null at this point.
            return enumType.Value;
        }

        public static T ThrowIfInvalidEnumValue<T, E>(object value, string message = null, params object[] args)
            where T : struct, IComparable, IConvertible, IFormattable // the closest to Enum for compile time check
            where E : ErrorException
        {
            return ThrowIfInvalidEnumValue<T, E>(value, null, message, args);
        }

        public static T ThrowIfInvalidEnumValue<T, E>(object value, Error error, string message = null, params object[] args)
            where T : struct, IComparable, IConvertible, IFormattable // the closest to Enum for compile time check
            where E : ErrorException
        {
            ThrowIfFalse(typeof(T).IsEnum, error, message, args);
            T? enumType = null;

            try
            {
                var enumNumericValue = Convert.ChangeType(value, typeof(T).GetEnumUnderlyingType());
                if (Enum.IsDefined(typeof(T), enumNumericValue))
                    enumType = (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                enumType = null;
            }

            if (enumType.HasValue == false)
                ThrowException<E>(error ?? ContractError.ThrowIfInvalidEnumValue.ToError(value.ToSafeString(), typeof(T).FullName));

            // ReSharper disable once PossibleInvalidOperationException
            // enumType can't be null at this point.
            return enumType.Value;
        }
        #endregion

        private static string Message(string message, params object[] args)
        {
            try
            {
                return ((args != null) && (args.Length > 0)) ? string.Format(message, args) : message;
            }
            catch
            {
                return string.Format(ResourceCoreStrings.Error_FormattingText, message, 
                                        (args == null) ? "no parameters" : string.Join(",", args));
            }
        }

        /// <summary>
        /// Throw an ErrorException. If the message can't be parsed properly, the resulting message will indicate it. 
        /// </summary>
        public static void ThrowException(Error error = null, string message = null, params object[] args)
        {
            throw new ErrorException(Message(message ?? string.Empty, args), error ?? ContractError.General.ToError());
        }

        /// <summary>
        /// Throw an exception of type &lt;T&gt;. If any errors is encountered during the generation of the exception, an inner exception will indicate the reason.
        /// </summary>
        public static void ThrowException<T>(Error error = null, string message = null, params object[] args)
            where T : ErrorException
        {
            var errorMessage = Message(message ?? string.Empty, args);
            var errorInstance = error ?? ContractError.General.ToError();

            var exception = new ErrorException(errorMessage, errorInstance,
                                new ErrorException(ContractError.CantInstantiateException.ToError(typeof(T).FullName)));
            try
            {
                exception = Activator.CreateInstance(typeof(T), errorMessage, errorInstance) as T ??
                            new ErrorException(errorMessage, errorInstance,
                                new ErrorException(ContractError.CantInstantiateException.ToError(typeof(T).FullName)));
            }
            catch
            {
                // Silently ignore error - exception already set up for error - pessimist approach.
            }

            throw exception;
        }
    }
}
