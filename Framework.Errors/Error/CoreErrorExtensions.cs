
namespace Framework.Errors
{
    /// <summary>
    /// Convert the CoreError enum into an Error instance based on the ResourceCoreErrors resource storage.
    /// </summary>
    public static class CoreErrorExtensions
    {
        private const string ERROR_NAME_PREFIX = "ERROR_CORE_";

        public static Error ToError(this CoreError error, params object[] args)
        {
            var name = ERROR_NAME_PREFIX + error.ToString().ToUpper();
            return Error.Factory.FromResource((uint)error, name, ResourceCoreErrors.ResourceManager, args);
        }
    }
}
