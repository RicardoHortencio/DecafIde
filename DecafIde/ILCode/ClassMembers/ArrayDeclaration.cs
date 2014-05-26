
namespace DecafIde.ILCode.ClassMembers
{
    class ArrayDeclaration : Command
    {
        string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        int arraySize;
        public int ArraySize
        {
            get { return arraySize; }
            set { arraySize = value; }
        }

        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string getTemplateName()
        {
            return "ArrayFieldInit";
        }
        public override void setGenCode()
        {
            setPlaceholder("type", Type);
            setPlaceholder("name", Name);
            setPlaceholder("size", ArraySize.ToString());
        }
    }
}
