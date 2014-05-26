using DecafIde.ILCode.ClassMembers;
using System.Collections.Generic;

namespace DecafIde.ILCode
{
    class MainClass : Command
    {

        List<StructDeclaration> structDeclList = new List<StructDeclaration>();
        List<FieldDeclaration> fieldDeclList = new List<FieldDeclaration>();
        List<MethodDeclaration> methodDeclList = new List<MethodDeclaration>();
        MainMethod mainMethod = new MainMethod();
        List<ArrayDeclaration> arrayDeclList = new List<ArrayDeclaration>();

        internal MainMethod MainMethod
        {
            get { return mainMethod; }
            set { mainMethod = value; }
        }

        public override string getTemplateName()
        {
            return "Main";
        }
        public override void setGenCode()
        {
            setCommandList(structDeclList);
            setCommandList(fieldDeclList);
            setSingleCommand(mainMethod);
            setCommandList(methodDeclList);
            setCommandList(arrayDeclList);
        }

        public void addCommand(StructDeclaration theCommand)
        {
            structDeclList.Add(theCommand);
        }
        public void addCommand(FieldDeclaration theCommand)
        {
            fieldDeclList.Add(theCommand);
        }
        public void addCommand(MethodDeclaration theCommand)
        {
            methodDeclList.Add(theCommand);
        }
        public void addCommand(MainMethod theCommand)
        {
            mainMethod = theCommand;
        }

        public void addCommand(ArrayDeclaration theCommand)
        {
            arrayDeclList.Add(theCommand);
        }

        public MainClass() : base() { }

        public MainClass(List<StructDeclaration> theStructDeclList,
            List<FieldDeclaration> theFieldDeclList,
            List<MethodDeclaration> theMethodDeclList, List<ArrayDeclaration> theArrayDeclList)
            : this()
        {
            structDeclList = theStructDeclList;
            fieldDeclList = theFieldDeclList;
            methodDeclList = theMethodDeclList;
            arrayDeclList = theArrayDeclList;
        }
    }
}
