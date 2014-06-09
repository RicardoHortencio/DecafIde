
namespace DecafIde.HelperClasses
{
    /// <summary>
    /// Gets the MSIL type mapping
    /// </summary>
    static class TypeConstructor
    {
        public static string getMappedType(string type)
        {
            switch (type)
            {
                case "int":
                    return "int32";
                case "boolean":
                    return "bool";
                default:
                    return type;
            }
        }
    }
}
