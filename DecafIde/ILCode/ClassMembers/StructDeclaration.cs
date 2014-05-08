using System.Collections.Generic;

namespace DecafIde.ILCode.ClassMembers
{
    class StructDeclaration : Command
    {
        string nameField;

        public string NameField
        {
            get { return nameField; }
            set { nameField = value; }
        }
        List<StructDeclaration> structDeclList = new List<StructDeclaration>();
        List<FieldDeclaration> fieldDeclList = new List<FieldDeclaration>();
        public override string getTemplateName()
        {
            return "StructDec";
        }
        public override void setGenCode()
        {
            setPlaceholder("name", nameField);
            setCommandList(structDeclList);
            setCommandList(fieldDeclList);
        }

        public void addCommand(StructDeclaration theCommmand)
        {
            structDeclList.Add(theCommmand);
        }
        public void addCommand(FieldDeclaration theCommand)
        {
            fieldDeclList.Add(theCommand);
        }
    }
}
