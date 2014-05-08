
namespace DecafIde.HelperClasses
{
    static class LabelNameGenerator
    {
        private static int labelCounter;

        public static string getLabelName(string suffix = "")
        {
            return "label" + suffix + (labelCounter++).ToString();
        }
    }
}
