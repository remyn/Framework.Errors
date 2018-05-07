using System;
using System.Resources;

namespace Framework.Errors
{
    public class ErrorFactory
    {
        /// <summary>
        /// Create an Error from an uint, providing the Description from a resource file based on the named key.
        /// If the description can't be found, the description contains a message indicating the reason why the description could not be extracted of the resource storage.
        /// </summary>
        /// <remarks>
        /// Here we don't throw any exceptions, but instead always return an Error, completed as much as we can.
        /// </remarks>
        public Error FromResource(uint value, string name, ResourceManager resManager, params object[] args)
        {
            if (resManager == null)
                return new Error(value, name,
                    string.Format(ResourceCoreStrings.Error_RetrievingDescription, value, name, string.Empty,
                                            string.Format(ResourceCoreStrings.Error_ParameterNull, "resManager")));
            if (string.IsNullOrWhiteSpace(name))
                return new Error(value, name,
                    string.Format(ResourceCoreStrings.Error_RetrievingDescription, value, name, resManager.BaseName,
                                            string.Format(ResourceCoreStrings.Error_ParameterNull, "name")));
         
            try
            {
                var description = resManager.GetString(name);
                if (string.IsNullOrWhiteSpace(description))
                    return new Error(value, name,
                        string.Format(ResourceCoreStrings.Error_RetrievingDescription, value, name, resManager.BaseName,
                                                ResourceCoreStrings.Error_NoDescriptionFound));

                return new Error(value, name,
                                    ((args != null) && (args.Length > 0)) ? string.Format(description, args) : description);
            }
            catch (Exception ex)
            {
                return new Error(value, name,
                    string.Format(ResourceCoreStrings.Error_RetrievingDescription, value, name, resManager.BaseName, ex.Message));            }
        }
    }
}
