
namespace Framework.Errors
{
    /// <summary>
    /// Convert the ContractError enum into an Error instance based on the ResourceContractErrors resource storage.
    /// </summary>
    public static class ContractErrorExtensions
    {
        private const string ERROR_NAME_PREFIX = "ERROR_CONTRACT_";

        public static Error ToError(this ContractError error, params object[] args)
        {
            var number = (uint)ErrorOffset.Contract + (uint) error;
            var name = ERROR_NAME_PREFIX + error.ToString().ToUpper();
            return Error.Factory.FromResource(number, name, ResourceContractErrors.ResourceManager, args);
        }
    }
}
