
namespace DecafIde.ILCode.ClassMembers.MethodMembers
{
    class LocalVar : Command
    {
        VarType typeName;
        internal VarType TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        string varId;
        public string VarId
        {
            get { return varId; }
            set { varId = value; }
        }

        bool isArray;
        public bool IsArray
        {
            get { return isArray; }
            set { isArray = value; }
        }
        public override string getTemplateName()
        {
            return "LocalVar";
        }
        public override void setGenCode()
        {
            setSingleCommand(typeName);
            setPlaceholder("id", varId);
            setPlaceholder("isArray", isArray);
        }
    }
}
