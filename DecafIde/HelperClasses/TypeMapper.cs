
namespace DecafIde.ILCode
{
    static class TypeMapper
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
