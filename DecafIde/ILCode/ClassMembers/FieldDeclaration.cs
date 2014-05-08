
namespace DecafIde.ILCode.ClassMembers
{
    class FieldDeclaration : Command
    {
        VarType typeName;
        internal VarType TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        string fieldId;
        public string FieldId
        {
            get { return fieldId; }
            set { fieldId = value; }
        }

        bool isArray;
        public bool IsArray
        {
            get { return isArray; }
            set { isArray = value; }
        }

        public override string getTemplateName()
        {
            return "Field";
        }
        public override void setGenCode()
        {
            setSingleCommand(typeName);
            setPlaceholder("id", fieldId);
            setPlaceholder("isArray", isArray);
        }
    }
}
