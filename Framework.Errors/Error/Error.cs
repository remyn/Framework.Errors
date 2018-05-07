using System;
using Framework.Core;


namespace Framework.Errors
{
    /// <summary>
    /// Represents an Error, quantified by a (UInt32)value, a name and a description.
    /// </summary>
    /// <remarks>
    /// The Error values are split into different range for specific meaning, e.g.:
    ///  - Error 0x00000000 - 0x000000FF : General Errors
    ///  - Error 0x00000100 - 0x000001FF : Contract Errors
    ///  - Error 0x00000200 - 0x000002FF : Payroll Generic Errors
    /// You can retrieve these values in ErrorOffset.
    /// We used the Serialize attribute in the ErrorException.
    /// If that becomes a constraint you can directly do the serialization of the Value, Name and Description in the GetObjectData() of the serialisation.
    /// </remarks>
    [Serializable]
    public class Error
    {
        public static ErrorFactory Factory { get; private set; }

        #region Properties
        public uint Value { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        #endregion

        #region Constructors
        static Error()
        {
            Factory = new ErrorFactory();
        }

        public Error()
            : this(0, DefaultName(0), ResourceCoreErrors.ERROR_CORE_UNKNOWN)
        {
        }

        public Error(uint value, string name = null, string description = null)
        {
            Value = value;
            Name = string.IsNullOrWhiteSpace(name) ? DefaultName(value) : name;
            Description = string.IsNullOrWhiteSpace(description) ? DefaultDescription(value) : description;
        }
        #endregion

        public static string DefaultName(uint value)
        {
            return string.Format("0x{0}", value.ToString("X8"));
        }

        public static string DefaultDescription(uint value)
        {
            return string.Format("Error {0}", DefaultName(value));
        }

        public override string ToString()
        {
            return string.Format("Error #{0}: {1}, {2}", Value, Name.ToSafeString(), Description.ToSafeString());
        }

        #region Equality / Inequality
        public static bool operator ==(Error lhs, Error rhs)
        {
            return ReferenceEquals(lhs, null) ? ReferenceEquals(rhs, null) : lhs.Equals(rhs);
        }

        public static bool operator !=(Error lhs, Error rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object @object)
        {
            if (ReferenceEquals(null, @object))
                return false;
            if (ReferenceEquals(this, @object))
                return true;

            var error = @object as Error;
            return (error != null) && error.Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        #endregion
    }
}
