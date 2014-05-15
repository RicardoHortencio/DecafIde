
namespace DecafIde.HelperClasses
{
    /// <summary>
    /// Helper class to generate the label names.
    /// </summary>
    static class LabelNameGenerator
    {
        private static int labelCounter;
        /// <summary>
        /// Gets a label name with an optional given Suffix
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string getLabelName(string suffix = "")
        {
            return "label" + suffix + (labelCounter++).ToString();
        }
    }
}
