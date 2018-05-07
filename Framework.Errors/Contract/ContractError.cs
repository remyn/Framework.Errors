
namespace Framework.Errors
{
    public enum ContractError : uint
    {
        Unknown = 0,
        General,

        CantInstantiateException,
        ThrowIfNotNull,
        ThrowIfNull,
        ThrowIfFalse = 0x05,

        ThrowIfTrue,
        ThrowIfStringIsNullOrWhiteSpace,
        ThrowIfEqual,
        ThrowIfNotEqual,
        ThrowIfNotType = 0x0A,

        ThrowIfInvalidEnumValue
    }
}
